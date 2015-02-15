using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;
using System.Timers;

using Croaker.CamfrogWin32;
using System.Text.RegularExpressions;

namespace Croaker_Mic_Watcher
{
    public partial class MainForm : Form
    {
        private CamfrogWin32 win32;
        private MicWatcher watcher;
        //private RoomReader reader;

        #region Settings Variables
        private string roomName;
        private string sessionDataPath;
        private string safeListPath;
        #endregion

        private int topCount;

        private TimeSpan warnThreshold;
        private TimeSpan blockThreshold;
        private TimeSpan resetSessionInterval;

        private bool warnUser = true;
        private bool blockUser = true;
        private bool autoUnblock = false;
        private int blockPeriod = 30;
        private string nickOnMic;
        private string lastNickDropped;
        private int secondsOnMic;
        private bool resetSessions = true;
        private bool sendToRoom = false;

        #region Timers
        private System.Timers.Timer unblockTimer = null;
        private System.Timers.Timer resetSessionsTimer = null;
        private System.Timers.Timer onMicTimer = null;
        private System.Timers.Timer userCountTimer = null;
        #endregion

        #region Constants
        private const int SEND_MESSAGE_DELAY = 1200; //milliseconds
        #endregion

        #region Delegates and Events
        private delegate void RefreshDataSourceCallback();
        private delegate void SetNullBindingSourceCallback();
        private delegate void SetWhoTalkingTextCallback(string value);
        private delegate void ClearOnMicCounterCallback();
        private delegate void UpdateOnMicCounterCallback(int value);
        private delegate void UpdateUserCountCallback(int value);
        #endregion

        #region Helper Methods
        /// <summary>
        /// Loads UI settings from the configuration file.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (Properties.Settings.Default["RoomName"] != null)
                    roomName = Properties.Settings.Default["RoomName"].ToString();

                if (Properties.Settings.Default["DataPath"] != null)
                    sessionDataPath = Properties.Settings.Default["DataPath"].ToString();

                if (Properties.Settings.Default["SafeListPath"] != null)
                    safeListPath = Properties.Settings.Default["SafeListPath"].ToString();

                if (Properties.Settings.Default["WarnUser"] != null)
                {
                    warnUser = (bool)Properties.Settings.Default["WarnUser"];
                    this.dtpWarnThreshold.Checked = warnUser;
                }

                if (Properties.Settings.Default["WarnThreshold"] != null)
                {
                    warnThreshold = (TimeSpan)Properties.Settings.Default["WarnThreshold"];
                    this.dtpWarnThreshold.Value = DateTime.Today + warnThreshold;
                }

                if (Properties.Settings.Default["BlockUser"] != null)
                {
                    blockUser = (bool)Properties.Settings.Default["BlockUser"];
                    this.dtpBlockThreshold.Checked = blockUser;
                }

                if (Properties.Settings.Default["BlockThreshold"] != null)
                {
                    blockThreshold = (TimeSpan)Properties.Settings.Default["BlockThreshold"];
                    this.dtpBlockThreshold.Value = DateTime.Today + blockThreshold;
                }

                if (Properties.Settings.Default["BlockPeriod"] != null)
                {
                    blockPeriod = (int)Properties.Settings.Default["BlockPeriod"];
                    this.nudBlockPeriod.Value = this.blockPeriod;
                }

                if (Properties.Settings.Default["ResetInterval"] != null)
                {
                    resetSessionInterval = (TimeSpan)Properties.Settings.Default["ResetInterval"];
                    this.dtpResetInterval.Value = DateTime.Today + resetSessionInterval;
                }

                if (Properties.Settings.Default["AutoUnblock"] != null)
                {
                    autoUnblock = (bool)Properties.Settings.Default["AutoUnblock"];
                    this.chkAutoUnBlock.Checked = this.autoUnblock;
                }

                if (Properties.Settings.Default["SendToRoom"] != null)
                {
                    sendToRoom = (bool)Properties.Settings.Default["SendToRoom"];
                    this.chkSendToRoom.Checked = this.sendToRoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading application settings:\n{0}", ex.Message), Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Saves UI settings to the configuration file.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                InitFormSettings();

                Properties.Settings.Default["WarnUser"] = this.dtpWarnThreshold.Checked;
                Properties.Settings.Default["WarnThreshold"] = this.warnThreshold;
                Properties.Settings.Default["BlockUser"] = this.dtpBlockThreshold.Checked;
                Properties.Settings.Default["BlockThreshold"] = this.blockThreshold;
                Properties.Settings.Default["BlockPeriod"] = this.blockPeriod;
                Properties.Settings.Default["ResetInterval"] = this.resetSessionInterval;
                Properties.Settings.Default["AutoUnblock"] = this.autoUnblock;
                Properties.Settings.Default["SendToRoom"] = this.sendToRoom;

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error saving settings:\n{0}", ex.Message)
                    , Application.ProductName
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// This method initializes the form setting variables.
        /// </summary>
        private void InitFormSettings()
        {
            this.topCount = (int)this.nudTop.Value;
            this.warnThreshold = new TimeSpan(this.dtpWarnThreshold.Value.Hour
                , this.dtpWarnThreshold.Value.Minute, this.dtpWarnThreshold.Value.Second);
            this.blockThreshold = new TimeSpan(this.dtpBlockThreshold.Value.Hour,
                this.dtpBlockThreshold.Value.Minute, this.dtpBlockThreshold.Value.Second);
            this.blockPeriod = (int)this.nudBlockPeriod.Value;
            this.autoUnblock = this.chkAutoUnBlock.Checked;
            this.resetSessions = this.dtpResetInterval.Checked;
            this.resetSessionInterval = new TimeSpan(this.dtpResetInterval.Value.Hour, this.dtpResetInterval.Value.Minute,
                this.dtpResetInterval.Value.Second);
            this.sendToRoom = this.chkSendToRoom.Checked;
        }

        /// <summary>
        /// Gets the needed win32 window handles.
        /// </summary>
        private void GetHandles()
        {
            win32.GetChatRoomHandle();
            win32.GetChatWindowHandle(win32.ChatRoomHandle);
            win32.GetTextArea(win32.ChatRoomHandle);
            win32.GetButton(win32.ChatRoomHandle);
            win32.GetTreeViewControl(win32.ChatRoomHandle);
        }

        /// <summary>
        /// Loads the saved mic session data.
        /// </summary>
        private void LoadSessionData()
        {
            try
            {
                if (File.Exists(sessionDataPath))
                {
                    this.Cursor = Cursors.WaitCursor;

                    this.micWatcherDataSet1.Session.ReadXml(sessionDataPath);

                    RenderTop(topCount);

                    this.bindingSource1.DataSource = this.micWatcherDataSet1;
                    this.bindingSource1.DataMember = "Totals";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading session data:\n{0}", ex.Message), Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Loads the saved save list.
        /// </summary>
        private void LoadSafeList()
        {
            try
            {
                if (File.Exists(safeListPath))
                {
                    this.Cursor = Cursors.WaitCursor;

                    this.lstSafeList.Items.Clear();
                    this.micWatcherDataSet1.SafeList.Clear();
                    this.micWatcherDataSet1.SafeList.ReadXml(safeListPath);

                    foreach (MicWatcherDataSet.SafeListRow row in this.micWatcherDataSet1.SafeList)
                    {
                        this.lstSafeList.Items.Add(row.NickName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading safe list:\n{0}", ex.Message), Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Refresh the top list datagridview datasource
        /// </summary>
        private void RefreshDataGridView()
        {
            if (this.dgvMicUsage.InvokeRequired)
            {
                RefreshDataSourceCallback callback = new RefreshDataSourceCallback(RefreshDataGridView);
                this.Invoke(callback, new object[] { });

            }
            else
            {
                this.bindingSource1.DataSource = this.micWatcherDataSet1;
                this.bindingSource1.DataMember = "Totals"; ;
            }
        }

        /// <summary>
        /// Set the databinding datasource to null. 
        /// This is required because the databound control doesn't like it when you alter the underlying data while it's bound to a control.
        /// </summary>
        private void SetNullBindingSource()
        {
            if (this.dgvMicUsage.InvokeRequired)
            {
                SetNullBindingSourceCallback callback = new SetNullBindingSourceCallback(SetNullBindingSource);
                this.Invoke(callback, new object[] { });
            }
            else
            {
                this.bindingSource1.DataSource = null;
            }
        }

        /// <summary>
        /// Set the who is talking text value.
        /// </summary>
        /// <param name="value">Value to set in the label</param>
        private void SetWhoTalkingText(string value)
        {
            if (this.lblCurrentlyOnMic.InvokeRequired)
            {
                SetWhoTalkingTextCallback callback = new SetWhoTalkingTextCallback(SetWhoTalkingText);
                this.Invoke(callback, new object[] { value });
            }
            else
            {
                this.lblCurrentlyOnMic.Text = string.Format("Currntly on mic:   {0}", value);
            }
        }

        /// <summary>
        /// Updates the seconds on mic label.
        /// </summary>
        /// <param name="value">Value to set in the label</param>
        private void UpdateTimeOnMicLabel(int value)
        {
            if (this.lblTimeOnMic.InvokeRequired)
            {
                UpdateOnMicCounterCallback callback = new UpdateOnMicCounterCallback(UpdateTimeOnMicLabel);
                this.Invoke(callback, new object[] { value });
            }
            else
            {
                this.lblTimeOnMic.Text = value.ToString();
            }
        }

        private void UpdateUserCountLabel(int value)
        {
            if (this.lblUserCount.InvokeRequired)
            {
                UpdateUserCountCallback callback = new UpdateUserCountCallback(UpdateUserCountLabel);
                this.Invoke(callback, new object[] { value });
            }
            else
            {
                this.lblUserCount.Text = string.Format("Users ({0})", value);
            }
        }

        /// <summary>
        /// Clear the who is talking text value.
        /// </summary>
        private void ClearOnMicLabel()
        {
            if (this.lblTimeOnMic.InvokeRequired)
            {
                ClearOnMicCounterCallback callback = new ClearOnMicCounterCallback(ClearOnMicLabel);
                this.Invoke(callback, new object[] { });
            }
            else
            {
                this.secondsOnMic = 0;
                this.lblTimeOnMic.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sum up all mic session data and return the top x values into the datagridview.
        /// </summary>
        /// <param name="top"></param>
        private void RenderTop(int top)
        {
            var topx = (from s in this.micWatcherDataSet1.Session
                        group s by new { s.NickName } into g
                        select new { g.Key.NickName, TotalTime = g.Sum(s => s.TimeOnMic.Ticks) })
                            .OrderByDescending(s => s.TotalTime)
                            .AsEnumerable()
                            .Take(top)
                            .ToList();

            this.micWatcherDataSet1.Totals.Clear();

            foreach (var item in topx)
            {
                MicWatcherDataSet.TotalsRow newRow = this.micWatcherDataSet1.Totals.NewTotalsRow();
                TimeSpan totalTime = new TimeSpan(item.TotalTime);

                newRow.NickName = item.NickName;
                newRow.TotalTime = totalTime; //new TimeSpan(totalTime.Hours, totalTime.Minutes, totalTime.Seconds);
                this.micWatcherDataSet1.Totals.AddTotalsRow(newRow);
            }
        }

        /// <summary>
        /// Searches the safe list items, using a case insensitive method, for a match.
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        private bool IsSafe(string nickName)
        {
            for (int i = 0; i < this.lstSafeList.Items.Count; i++)
            {
                if (this.lstSafeList.Items[i].ToString().Equals(nickName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        /// <summary>
        /// MainForm constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// MainForm Load method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();

            InitFormSettings();

            win32 = new CamfrogWin32();
            watcher = new MicWatcher(roomName);
            //reader = new RoomReader();

            win32.RoomName = roomName;
            watcher.MicUserChanged += new MicWatcher.MicUserChangedEventHandler(MicWatcher_UserChanged);
            watcher.MicDropped += new MicWatcher.MicDroppedEventHandler(MicWatcher_MicDropped);

            //reader.RoomName = roomName;
            //reader.NewMessageRead += new RoomReader.MessageReadEventHandler(Reader_NewMessageRead);

            GetHandles();

            userCountTimer = new System.Timers.Timer(1000);
            userCountTimer.Elapsed += userCountTimer_Elapsed;
            userCountTimer.Start();

            this.dgvMicUsage.Columns[1].DefaultCellStyle.FormatProvider = new TimeSpanFormatter();
            this.dgvMicUsage.Columns[1].DefaultCellStyle.Format = "hh:mm:ss";

            LoadSafeList();
            LoadSessionData();
            RefreshDataGridView();
        }

        /// <summary>
        /// MainForm FormClosing method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.watcher.StopWatching();

                this.SaveSettings();

                if (this.userCountTimer != null)
                {
                    this.userCountTimer.Elapsed -= userCountTimer_Elapsed;
                    this.userCountTimer.Stop();
                    this.userCountTimer = null;
                }

                if (this.unblockTimer != null)
                {
                    this.unblockTimer.Elapsed -= unblockTimer_Elapsed;
                    this.unblockTimer.Stop();
                    this.unblockTimer = null;
                }
                if (this.resetSessionsTimer != null)
                {
                    this.resetSessionsTimer.Elapsed -= resetSessionsTimer_Elapsed;
                    this.resetSessionsTimer.Stop();
                    this.resetSessionsTimer = null;
                }

                if (!Directory.Exists(safeListPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(safeListPath));
                }

                this.micWatcherDataSet1.Session.WriteXml(sessionDataPath);
                this.micWatcherDataSet1.SafeList.WriteXml(safeListPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error saving usage data:\n{0}", ex.Message), Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //#region Reader Events
        //private void Reader_NewMessageRead(MessageReadEventArgs e)
        //{
        //    //User leonleon10, IP: 108.184.47.60 
        //    if (e.NickName.Equals(string.Format("User {0}, IP", nickOnMic), StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        Console.WriteLine(string.Format("{0} IP = {1}", nickOnMic, e.Message));
        //    }
        //}
        //#endregion

        #region Watcher Events
        /// <summary>
        /// MicWatcher MicDropped method. This event is raised when the mic is dropped.
        /// </summary>
        /// <param name="e"></param>
        private void MicWatcher_MicDropped(MicDroppedEventArgs e)
        {
            if (!IsSafe(e.LastNickOnMic) && e.TimeOnMic > new TimeSpan(0, 0, 0, 1, 500))
            {
                try
                {
                    if (e.LastNickOnMic.Equals(nickOnMic))
                    {
                        onMicTimer.Stop();

                        ClearOnMicLabel();
                        SetWhoTalkingText(string.Empty);
                    }

                    lastNickDropped = e.LastNickOnMic;
                    MicWatcherDataSet.SessionRow row = this.micWatcherDataSet1.Session.NewSessionRow();
                    row.NickName = e.LastNickOnMic;
                    row.TimeOnMic = e.TimeOnMic;
                    this.micWatcherDataSet1.Session.AddSessionRow(row);

                    if (!Directory.Exists(safeListPath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(safeListPath));
                    }

                    this.micWatcherDataSet1.Session.WriteXml(sessionDataPath);

                    SetNullBindingSource();
                    RenderTop(topCount);
                    RefreshDataGridView();

                    var totalResults = (from s in this.micWatcherDataSet1.Session
                                        where s.NickName == e.LastNickOnMic
                                        group s by new { s.NickName } into g
                                        select new { g.Key.NickName, TotalTime = g.Sum(s => s.TimeOnMic.Ticks) })
                                        .FirstOrDefault();

                    if (totalResults != null)
                    {
                        TimeSpan totalTimeSpan = new TimeSpan(totalResults.TotalTime);

                        if (warnUser)
                        {
                            var warnResult = (from w in this.micWatcherDataSet1.WarnList
                                              where w.NickName == e.LastNickOnMic
                                              select w).FirstOrDefault();

                            if (warnResult == null)
                            {
                                if (totalTimeSpan.Ticks >= this.warnThreshold.Ticks)
                                {
                                    string msg = string.Format("{0}, You have {1} minute(s) and {2} second(s) left on the mic."
                                    , nickOnMic
                                    , new TimeSpan(this.blockThreshold.Ticks - totalTimeSpan.Ticks).Minutes
                                    , new TimeSpan(this.blockThreshold.Ticks - totalTimeSpan.Ticks).Seconds);

                                    Console.WriteLine(msg);
                                    if (this.sendToRoom)
                                        win32.SendText(msg, SEND_MESSAGE_DELAY);
                                    else
                                        Thread.Sleep(SEND_MESSAGE_DELAY);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("micDropped {0}", ex.Message); }
            }

        }

        /// <summary>
        /// MicWatcher UserChanged method. This event is raised when the user on the mic changes.
        /// </summary>
        /// <param name="e"></param>
        private void MicWatcher_UserChanged(MicUserChangedEventArgs e)
        {
            nickOnMic = e.NickName;
            SetWhoTalkingText(e.NickName);

            //string ipMsg = string.Format("/ip {0}", e.NickName);
            //win32.SendText(ipMsg,0);

            if (onMicTimer == null)
            {
                onMicTimer = new System.Timers.Timer(1000);
                onMicTimer.Elapsed += new ElapsedEventHandler(onMicTimer_Elapsed);
            }
            onMicTimer.Start();

            if (!IsSafe(e.NickName))
            {
                try
                {
                    var blockResult = (from b in this.micWatcherDataSet1.BlockList
                                       where b.NickName == e.NickName
                                       select b).FirstOrDefault();

                    if (blockResult != null && blockUser && blockResult.NickName == e.NickName)
                    {
                        string blockMsg = string.Format("/blockmic {0}", e.NickName);
                        string totalMsg = string.Format("{0}, {1} minute(s) and {2} second(s) remaining until you are unblocked."
                            , e.NickName
                            , (int)((blockResult.BlockedWhen.AddMinutes(blockPeriod)) - DateTime.Now).Minutes
                            , (int)((blockResult.BlockedWhen.AddMinutes(blockPeriod)) - DateTime.Now).Seconds);

                        Console.WriteLine(blockMsg);
                        if (this.sendToRoom)
                            win32.SendText(blockMsg, 0);

                        Console.WriteLine(totalMsg);
                        if (this.sendToRoom)
                            win32.SendText(totalMsg, SEND_MESSAGE_DELAY);
                        else
                            Thread.Sleep(SEND_MESSAGE_DELAY);
                    }
                }
                catch (Exception ex) { Console.WriteLine("userChanged {0}", ex.Message); }
            }
        }
        #endregion

        #region Timer Elapsed Events
        /// <summary>
        /// onMicTimer timer elapsed method. This event is raised when the onMicTimer interval elapses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onMicTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.secondsOnMic++;
            UpdateTimeOnMicLabel(this.secondsOnMic);

            try
            {
                var blockResult = (from b in this.micWatcherDataSet1.BlockList
                                   where b.NickName == nickOnMic
                                   select b).FirstOrDefault();
                var totalResults = (from s in this.micWatcherDataSet1.Session
                                    where s.NickName == nickOnMic
                                    group s by new { s.NickName } into g
                                    select new { g.Key.NickName, TotalTime = g.Sum(s => s.TimeOnMic.Ticks) })
                                        .FirstOrDefault();
                var warnResult = (from w in this.micWatcherDataSet1.WarnList
                                  where w.NickName == nickOnMic
                                  select w).FirstOrDefault();

                if (totalResults != null)
                {
                    TimeSpan totalTimeSpan = new TimeSpan(totalResults.TotalTime).Add(new TimeSpan(0, 0, this.secondsOnMic));

                    if (warnUser && warnResult == null && (totalTimeSpan.Ticks >= this.warnThreshold.Ticks))
                    {
                        MicWatcherDataSet.WarnListRow newRow = this.micWatcherDataSet1.WarnList.NewWarnListRow();
                        newRow.NickName = nickOnMic;
                        newRow.WarnedWhen = DateTime.Now;
                        this.micWatcherDataSet1.WarnList.AddWarnListRow(newRow);

                        string msg = string.Format("{0}, You have {1} minute(s) and {2} second(s) left on the mic."
                                    , nickOnMic
                                    , new TimeSpan(this.blockThreshold.Ticks - totalTimeSpan.Ticks).Minutes
                                    , new TimeSpan(this.blockThreshold.Ticks - totalTimeSpan.Ticks).Seconds);

                        Console.WriteLine(msg);
                        if (this.sendToRoom)
                            win32.SendText(msg, SEND_MESSAGE_DELAY);
                        else
                            Thread.Sleep(SEND_MESSAGE_DELAY);
                    }

                    if (blockUser && blockResult == null && (totalTimeSpan.Ticks >= this.blockThreshold.Ticks))
                    {
                        MicWatcherDataSet.BlockListRow newRow = this.micWatcherDataSet1.BlockList.NewBlockListRow();
                        newRow.NickName = nickOnMic;
                        newRow.BlockedWhen = DateTime.Now;
                        this.micWatcherDataSet1.BlockList.AddBlockListRow(newRow);

                        string blockMsg = string.Format("/blockmic {0}", nickOnMic);
                        string totalMsg = string.Format("{0}, {1} minute(s) remaining until you are unblocked."
                            , nickOnMic
                            , blockPeriod);

                        Console.WriteLine(blockMsg);
                        if (this.sendToRoom)
                            win32.SendText(blockMsg, 0);

                        Console.WriteLine(totalMsg);
                        if (this.sendToRoom)
                            win32.SendText(totalMsg, SEND_MESSAGE_DELAY);
                        else
                            Thread.Sleep(SEND_MESSAGE_DELAY);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("onMicThread {0}", ex.Message); }
        }

        /// <summary>
        /// resetSessionsTimer elapsed method. This event is raised when the resetSessionsTimer interval elapses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetSessionsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Uncomment the lines below to enable the unblocking of blocked users 
                // when the timers reset.
                //
                //var blockedUsers = (from b in this.micWatcherDataSet1.BlockList
                //                    select b)
                //                    .OrderByDescending(b => b.BlockedWhen)
                //                    .AsEnumerable()
                //                    .ToList();

                //foreach (var user in blockedUsers)
                //{
                //    string unblockMsg = string.Format("/unblockmic {0}", user.NickName);
                //    Console.WriteLine(unblockMsg);

                //    user.Delete();
                //    if (this.sendToRoom)
                //        win32.SendText(unblockMsg, SEND_MESSAGE_DELAY);
                //    else
                //        Thread.Sleep(SEND_MESSAGE_DELAY);
                //}

                SetNullBindingSource();

                this.micWatcherDataSet1.Totals.Clear();
                this.micWatcherDataSet1.Session.Clear();
                this.micWatcherDataSet1.WarnList.Clear();

                if (!Directory.Exists(safeListPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(safeListPath));
                }

                this.micWatcherDataSet1.Session.WriteXml(sessionDataPath);

                RefreshDataGridView();

                string msg = string.Format(" Mic timers reset! You all have {0} minute(s) and {1} second(s) to use {2} minute(s) and {3} second(s) on the mic."
                    , (int)this.resetSessionInterval.Minutes
                    , (int)this.resetSessionInterval.Seconds
                    , this.dtpBlockThreshold.Value.Minute
                    , this.dtpBlockThreshold.Value.Second);

                Console.WriteLine(msg);
                if (this.sendToRoom)
                    win32.SendText(msg, SEND_MESSAGE_DELAY);
                else
                    Thread.Sleep(SEND_MESSAGE_DELAY);
            }
            catch (Exception ex) { Console.WriteLine("reset {0}", ex.Message); }
        }

        /// <summary>
        /// unBlockTimer elapsed method. This event is raised when then unblockTimer interval elapses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unblockTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var blocked = (from b in this.micWatcherDataSet1.BlockList
                               where (DateTime.Now - b.BlockedWhen).TotalMinutes > blockPeriod
                               select b)
                                .OrderByDescending(b => b.BlockedWhen);

                foreach (var user in blocked)
                {
                    string nick = user.NickName;
                    var warn = (from w in this.micWatcherDataSet1.WarnList
                                where w.NickName == user.NickName
                                select w).FirstOrDefault();
                    var sessions = (from s in this.micWatcherDataSet1.Session
                                    where s.NickName == user.NickName
                                    select s).ToList();

                    user.Delete();
                    if (warn != null) warn.Delete();

                    if (sessions != null)
                    {
                        foreach (var session in sessions)
                        {
                            session.Delete();
                        }

                        SetNullBindingSource();
                        RenderTop(topCount);
                        RefreshDataGridView();
                    }

                    string unblockMsg = string.Format("/unblockmic {0}", nick);
                    string msg = string.Format("{0}, You are now unblocked.", nick);

                    Console.WriteLine(unblockMsg);
                    if (this.sendToRoom)
                        win32.SendText(unblockMsg, 0);

                    Console.WriteLine(msg);
                    if (this.sendToRoom)
                        win32.SendText(msg, SEND_MESSAGE_DELAY);
                    else
                        Thread.Sleep(SEND_MESSAGE_DELAY);
                }
            }
            catch (Exception ex) { Console.WriteLine("unBlock {0}", ex.Message); }
        }

        private void userCountTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                int result = win32.SendMessageToWindow(win32.UserList, CamfrogWin32.TVM_GETCOUNT).ToInt32();

                UpdateUserCountLabel(result - 5);
            }
            catch (Exception ex) { Console.WriteLine("userCountTimer: {0}", ex.Message); }
        }
        #endregion

        #region Form Control Events
        private void nudTopCount_ValueChanged(object sender, EventArgs e)
        {
            topCount = (int)this.nudTop.Value;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                SetNullBindingSource();
                RenderTop(topCount);
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error rendering top list {0}", ex.Message), Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void dtpWarnThreshold_ValueChanged(object sender, EventArgs e)
        {
            this.warnThreshold = new TimeSpan(this.dtpWarnThreshold.Value.Hour
                , this.dtpWarnThreshold.Value.Minute, this.dtpWarnThreshold.Value.Second);

            this.warnUser = this.dtpWarnThreshold.Checked;
        }
        private void dtpBlockThreshold_ValueChanged(object sender, EventArgs e)
        {
            this.blockThreshold = new TimeSpan(this.dtpBlockThreshold.Value.Hour,
                this.dtpBlockThreshold.Value.Minute, this.dtpBlockThreshold.Value.Second);

            this.blockUser = this.dtpBlockThreshold.Checked;
        }
        private void chkAutoUnBlock_CheckedChanged(object sender, EventArgs e)
        {
            autoUnblock = this.chkAutoUnBlock.Checked;

            if (autoUnblock)
            {
                if (unblockTimer == null || !unblockTimer.Enabled)
                {
                    unblockTimer = new System.Timers.Timer(1000);
                    unblockTimer.Elapsed += unblockTimer_Elapsed;
                    unblockTimer.Start();
                }
            }
            else
            {
                if (unblockTimer != null)
                {
                    unblockTimer.Elapsed -= unblockTimer_Elapsed;
                    unblockTimer.Stop();
                    unblockTimer = null;
                }
            }
        }
        private void nudBlockPeriod_ValueChanged(object sender, EventArgs e)
        {
            blockPeriod = (int)nudBlockPeriod.Value;
        }
        private void chkResetSessions_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void chkSendToRoom_CheckedChanged(object sender, EventArgs e)
        {
            this.sendToRoom = chkSendToRoom.Checked;
        }
        private void btnClearSave_Click(object sender, EventArgs e)
        {
            SetNullBindingSource();
            this.micWatcherDataSet1.Totals.Clear();
            this.micWatcherDataSet1.Session.Clear();

            if (!Directory.Exists(safeListPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(safeListPath));
            }

            this.micWatcherDataSet1.Session.WriteXml(sessionDataPath);

            RefreshDataGridView();
        }
        private void btnSafeListAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtSafeListNew.Text))
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    MicWatcherDataSet.SafeListRow newRow = this.micWatcherDataSet1.SafeList.NewSafeListRow();

                    newRow.NickName = txtSafeListNew.Text.Trim();
                    this.micWatcherDataSet1.SafeList.AddSafeListRow(newRow);
                    this.lstSafeList.Items.Add(this.txtSafeListNew.Text.Trim());
                    this.txtSafeListNew.Clear();

                    if (!Directory.Exists(safeListPath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(safeListPath));
                    }

                    this.micWatcherDataSet1.SafeList.WriteXml(safeListPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error while saving safe list:\n{0}", ex.Message),
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

            }
        }
        private void lstSafeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.lstSafeList.SelectedItem != null)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;

                        int selectedIndex = this.lstSafeList.SelectedIndex;
                        this.lstSafeList.Items.RemoveAt(selectedIndex);

                        this.micWatcherDataSet1.SafeList.Rows.RemoveAt(selectedIndex);

                        if (!Directory.Exists(Path.GetDirectoryName(safeListPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(safeListPath));
                        }

                        this.micWatcherDataSet1.SafeList.WriteXml(safeListPath);

                        LoadSafeList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error deleting safe list entry:\n{0}", ex.Message)
                            , Application.ProductName
                            , MessageBoxButtons.OK
                            , MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }

                    e.Handled = true;
                }
            }
        }
        private void dtpResetInterval_ValueChanged(object sender, EventArgs e)
        {
            this.resetSessionInterval = new TimeSpan(this.dtpResetInterval.Value.Hour,
                this.dtpResetInterval.Value.Minute, this.dtpResetInterval.Value.Second);
            this.resetSessions = this.dtpResetInterval.Checked;

            if (this.resetSessionsTimer != null)
            {
                this.resetSessionsTimer.Interval = this.resetSessionInterval.TotalMilliseconds;
            }

            if (this.resetSessions)
            {
                if (this.watcher != null && this.watcher.IsWatching)
                {
                    if (this.resetSessionsTimer == null || !this.resetSessionsTimer.Enabled)
                    {
                        this.resetSessionsTimer = new System.Timers.Timer(this.resetSessionInterval.TotalMilliseconds);
                        this.resetSessionsTimer.Elapsed += resetSessionsTimer_Elapsed;
                        this.resetSessionsTimer.Start();
                    }
                }
            }
            else
            {
                if (this.resetSessionsTimer != null)
                {
                    this.resetSessionsTimer.Elapsed -= resetSessionsTimer_Elapsed;
                    this.resetSessionsTimer.Stop();
                    this.resetSessionsTimer = null;
                }
            }
        }
        private void btnStartStopWatching_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!watcher.IsWatching)
                {
                    this.btnStartStopWatching.Text = "Stop Watching";
                    this.btnClearSave.Enabled = false;
                    GetHandles();
                    InitFormSettings();

                    this.watcher.LastPersonOnMic = null;
                    watcher.StartWatching();

                    //this.reader.StartReading();

                    if (this.sendToRoom)
                    {
                        string startUpMessage = string.Format("Mic watcher started. You all have {0} minute(s) and {1} second(s) to use {2} minute(s) and {3} second(s) on the mic."
                            , (int)this.resetSessionInterval.Minutes
                        , (int)this.resetSessionInterval.Seconds
                        , this.dtpBlockThreshold.Value.Minute
                        , this.dtpBlockThreshold.Value.Second);

                        win32.SendText(startUpMessage, 10);
                    }

                    if (this.autoUnblock)
                    {
                        this.unblockTimer = new System.Timers.Timer(1000);
                        this.unblockTimer.Elapsed += unblockTimer_Elapsed;
                        this.unblockTimer.Start();
                    }
                    if (this.resetSessions)
                    {
                        this.resetSessionsTimer = new System.Timers.Timer(this.resetSessionInterval.TotalMilliseconds);
                        this.resetSessionsTimer.Elapsed += resetSessionsTimer_Elapsed;
                        this.resetSessionsTimer.Start();
                    }
                    else
                    {
                        this.resetSessionsTimer.Interval = this.resetSessionInterval.TotalMilliseconds;
                    }
                }
                else
                {
                    this.watcher.StopWatching();
                    this.watcher.LastPersonOnMic = null;

                    //this.reader.StopReading();

                    SetNullBindingSource();

                    this.micWatcherDataSet1.Totals.Clear();
                    this.micWatcherDataSet1.Session.Clear();
                    this.micWatcherDataSet1.WarnList.Clear();
                    this.micWatcherDataSet1.BlockList.Clear();

                    if (!Directory.Exists(safeListPath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(safeListPath));
                    }

                    this.micWatcherDataSet1.Session.WriteXml(sessionDataPath);

                    RefreshDataGridView();

                    if (this.sendToRoom)
                    {
                        string stopMessage = string.Format("Mic watcher stopped.");
                        win32.SendText(stopMessage, 10);
                    }

                    if (this.unblockTimer != null && this.unblockTimer.Enabled)
                    {
                        this.unblockTimer.Elapsed -= unblockTimer_Elapsed;
                        this.unblockTimer.Stop();
                        this.unblockTimer = null;
                    }
                    if (this.resetSessionsTimer != null && this.resetSessionsTimer.Enabled)
                    {
                        this.resetSessionsTimer.Elapsed -= resetSessionsTimer_Elapsed;
                        this.resetSessionsTimer.Stop();
                        this.resetSessionsTimer = null;
                    }
                    if (this.onMicTimer != null && this.onMicTimer.Enabled)
                    {
                        this.onMicTimer.Stop();
                    }

                    this.btnStartStopWatching.Text = "Start Watching";
                    this.btnClearSave.Enabled = true;
                    nickOnMic = null;
                    ClearOnMicLabel();
                    SetWhoTalkingText(string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error starting/stopping:\n{0}", ex.Message)
                    , Application.ProductName
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void dgvMicUsage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var formatter = e.CellStyle.FormatProvider as ICustomFormatter;
            if (formatter != null)
            {
                e.Value = formatter.Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider);
                e.FormattingApplied = true;
            }
        }
    }

    class TimeSpanFormatter : IFormatProvider, ICustomFormatter
    {
        private Regex _formatParser;

        public TimeSpanFormatter()
        {
            _formatParser = new Regex("d{1,2}|h{1,2}|m{1,2}|s{1,2}|f{1,7}", RegexOptions.Compiled);
        }

        #region IFormatProvider Members

        public object GetFormat(Type formatType)
        {
            if (typeof(ICustomFormatter).Equals(formatType))
            {
                return this;
            }

            return null;
        }

        #endregion

        #region ICustomFormatter Members

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg is TimeSpan)
            {
                var timeSpan = (TimeSpan)arg;
                return _formatParser.Replace(format, GetMatchEvaluator(timeSpan));
            }
            else
            {
                var formattable = arg as IFormattable;
                if (formattable != null)
                {
                    return formattable.ToString(format, formatProvider);
                }

                return arg != null ? arg.ToString() : string.Empty;
            }
        }

        #endregion

        private MatchEvaluator GetMatchEvaluator(TimeSpan timeSpan)
        {
            return m => EvaluateMatch(m, timeSpan);
        }

        private string EvaluateMatch(Match match, TimeSpan timeSpan)
        {
            switch (match.Value)
            {
                case "dd":
                    return timeSpan.Days.ToString("00");
                case "d":
                    return timeSpan.Days.ToString("0");
                case "hh":
                    return timeSpan.Hours.ToString("00");
                case "h":
                    return timeSpan.Hours.ToString("0");
                case "mm":
                    return timeSpan.Minutes.ToString("00");
                case "m":
                    return timeSpan.Minutes.ToString("0");
                case "ss":
                    return timeSpan.Seconds.ToString("00");
                case "s":
                    return timeSpan.Seconds.ToString("0");
                case "fffffff":
                    return (timeSpan.Milliseconds * 10000).ToString("0000000");
                case "ffffff":
                    return (timeSpan.Milliseconds * 1000).ToString("000000");
                case "fffff":
                    return (timeSpan.Milliseconds * 100).ToString("00000");
                case "ffff":
                    return (timeSpan.Milliseconds * 10).ToString("0000");
                case "fff":
                    return (timeSpan.Milliseconds).ToString("000");
                case "ff":
                    return (timeSpan.Milliseconds / 10).ToString("00");
                case "f":
                    return (timeSpan.Milliseconds / 100).ToString("0");
                default:
                    return match.Value;
            }
        }
    }
}
