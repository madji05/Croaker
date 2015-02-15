using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Croaker.CamfrogWin32;
using NCalc;

namespace Croaker
{
    public partial class MainForm : Form
    {
        private CamfrogWin32.CamfrogWin32 win32 = null;
        private RoomReader gameReader;
        private RoomReader modWatchReader;
        private RoomReader repeaterReader;
        private MicWatcher micWatcher;
        private string gameType = "1";
        private string gameResponseInterval = null;
        private string namesToWatch = null;
        private List<MicUserStats> micUsageList;
        private Thread usageTotalThread = null;

        private delegate void AddListViewItemCallback(string[] values);
        private delegate void ClearListViewItemCallback();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            win32 = new CamfrogWin32.CamfrogWin32();

            GetHandles();

            gameReader = new RoomReader();
            gameReader.NewMessageRead += new RoomReader.MessageReadEventHandler(GameReader_NewMessageRead);

            try
            {
                LoadSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadSettings()
        {
            gameType = Properties.Settings.Default["GameType"].ToString();
            gameResponseInterval = Properties.Settings.Default["ResponseInterval"].ToString();
            namesToWatch = Properties.Settings.Default["NamesToWatch"].ToString();

            if (gameType == "1") rdoMath.Checked = true;
            if (gameType == "2") rdoScramble.Checked = true;
            this.nudResponseInterval.Text = gameResponseInterval.ToString();

            foreach (string name in namesToWatch.Split(new char[] { ',' }))
            {
                this.lstNamesToWatch.Items.Add(name);
            }
        }

        private void SaveSettings()
        {
            string[] names = new string[this.lstNamesToWatch.Items.Count];
            string namesToWatch = null;

            this.lstNamesToWatch.Items.CopyTo(names, 0);
            namesToWatch = string.Join(",", names);

            Properties.Settings.Default["GameType"] = rdoMath.Checked ? "1" : "2";
            Properties.Settings.Default["ResponseInterval"] = nudResponseInterval.Text;
            Properties.Settings.Default["NamesToWatch"] = namesToWatch;

            Properties.Settings.Default.Save();
        }

        private void AddListViewItem(string[] values)
        {
            if (this.lvMicUsage.InvokeRequired)
            {
                AddListViewItemCallback callback = new AddListViewItemCallback(AddListViewItem);
                this.Invoke(callback, new object[] {values});
            }
            else{
                this.lvMicUsage.Items.Add(new ListViewItem(values));   
            }            
        }

        private void ClearListViewItem()
        {
            if (this.lvMicUsage.InvokeRequired)
            {
                ClearListViewItemCallback callback = new ClearListViewItemCallback(ClearListViewItem);
                this.Invoke(callback, new object[] { });
            }
            else
            {
                this.lvMicUsage.Items.Clear();
            }
        }

        private void GetHandles()
        {
            win32.GetChatRoomHandle();
            win32.GetChatWindowHandle(win32.ChatRoomHandle);
            win32.GetTextArea(win32.ChatRoomHandle);
            win32.GetButton(win32.ChatRoomHandle);
        }

        private void GameReader_NewMessageRead(MessageReadEventArgs e)
        {
            try
            {
                if (lstNamesToWatch.Items.Contains(e.NickName.ToLower()))
                {
                    string justNumbers = e.Message.Replace("+", "").Replace("-", "").Replace("*", "").Replace("÷", "").Replace("=", "").Replace("?", "");
                    int tryParseResult = 0;
                    if (int.TryParse(justNumbers, out tryParseResult))
                    {
                        string expString = e.Message.Replace("÷", "/").Replace("=", "").Replace("?", "");
                        Expression exp = new Expression(expString);
                        int result = (int)exp.Evaluate();

                        win32.SendText(result.ToString(), Convert.ToInt32(this.nudResponseInterval.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddNameToWatch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNameToWatch.Text))
            {
                try
                {
                    this.lstNamesToWatch.Items.Add(txtNameToWatch.Text.ToLower().Trim());
                    this.txtNameToWatch.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!gameReader.IsReading)
                {
                    this.btnPlay.Text = "Stop";
                    this.rdoMath.Enabled = false;
                    this.rdoScramble.Enabled = false;

                    GetHandles();

                    gameReader.StartReading();
                }
                else
                {
                    this.btnPlay.Text = "Start";
                    this.rdoMath.Enabled = true;
                    this.rdoScramble.Enabled = true;

                    gameReader.StopReading();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (gameReader != null && gameReader.IsReading)
                    gameReader.StopReading();
                if (modWatchReader != null && modWatchReader.IsReading)
                    modWatchReader.StopReading();

                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstNamesToWatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    int selectedIndex = this.lstNamesToWatch.SelectedIndex;
                    this.lstNamesToWatch.Items.RemoveAt(selectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.Handled = true;
            }
        }

        private void txtTextToSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                try
                {
                    win32.SendText(this.txtTextToSend.Text, 0);
                    this.txtTextToSend.Clear();
                    this.txtTextToSend.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.Handled = true;
            }
        }

        private void btnModWatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (modWatchReader == null)
                {
                    modWatchReader = new RoomReader();
                    modWatchReader.NewMessageRead += new RoomReader.MessageReadEventHandler(ModWatchReader_NewMessageRead);
                }

                if (!this.modWatchReader.IsReading)
                {
                    this.btnModWatch.Text = "Stop Mod Watch";
                    GetHandles();
                    this.modWatchReader.StartReading();
                }
                else
                {
                    this.btnModWatch.Text = "Start Mod Watch";
                    this.modWatchReader.StopReading();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModWatchReader_NewMessageRead(MessageReadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NickName) && e.NickName.ToLower().Equals("join"))
            {
                Console.WriteLine(e.Message);
                if (!string.IsNullOrEmpty(e.Message) && e.Message.ToLower().Contains("camfrogmoderator"))
                {
                    win32.SendText("Hi CamfrogModerator!", 0);
                }
            }
        }

        private void btnStartStopRepeat_Click(object sender, EventArgs e)
        {
            try
            {
                if (repeaterReader == null)
                {
                    repeaterReader = new RoomReader();
                    repeaterReader.NewMessageRead += new RoomReader.MessageReadEventHandler(RepeaterReader_NewMessageRead);
                }

                if (!this.repeaterReader.IsReading)
                {
                    this.btnStartStopRepeat.Text = "Stop Repeating";
                    GetHandles();
                    this.repeaterReader.StartReading();
                }
                else
                {
                    this.btnStartStopRepeat.Text = "Start Repeating";
                    this.repeaterReader.StopReading();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RepeaterReader_NewMessageRead(MessageReadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NickName) && e.NickName.ToLower().Equals(txtNameToRepeat.Text))
            {
                if (chkBackwards.Checked)
                    win32.SendText(Reverse(e.Message), 500);
                else
                    win32.SendText(e.Message, 500);
            }
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void btnMicGuard_Click(object sender, EventArgs e)
        {
            try
            {
                if (micWatcher == null)
                {
                    micWatcher = new MicWatcher();
                    micWatcher.MicUserChanged += new MicWatcher.MicUserChangedEventHandler(MicWatcher_UserChanged);
                    micWatcher.MicDropped += new MicWatcher.MicDroppedEventHandler(MicWatcher_MicDropped);
                }

                if (micWatcher != null && !micWatcher.IsWatching)
                {
                    GetHandles();                   

                    if (micUsageList == null)
                    {
                        micUsageList = new List<MicUserStats>();
                    }

                    micWatcher.StartWatching();

                    usageTotalThread = new Thread(new ThreadStart(UsageTotaler));
                    usageTotalThread.Start();
                }
                else if (micWatcher != null && micWatcher.IsWatching)
                {
                    micWatcher.StopWatching();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UsageTotaler()
        {
            while (this.micWatcher != null && this.micWatcher.IsWatching)
            {
                var userStats = (from x in this.micUsageList
                                group x by new { x.NickName } into g
                                select new { g.Key.NickName, TotalTime = g.Sum(x => x.TotalTime.Ticks) })
                                .OrderByDescending(y => y.TotalTime);

                this.ClearListViewItem();

                for (int i = 0; i < userStats.Count(); i++)
                {
                    var user = userStats.ElementAt(i);

                    AddListViewItem(new string[] { user.NickName, new DateTime(user.TotalTime).ToString("HH:mm:ss") });
                }

                Thread.Sleep(10000);
            }
            
        }

        private void MicWatcher_MicDropped(MicDroppedEventArgs e)
        {
            this.micUsageList.Add(new MicUserStats(e.LastNickOnMic, e.TimeOnMic));

            Console.WriteLine("Mic Dropped by {0};Time={1}", e.LastNickOnMic, e.TimeOnMic);
        }

        private void MicWatcher_UserChanged(MicUserChangedEventArgs e)
        {
            Console.WriteLine("{0} has the mic", e.NickName);
        }
    }
}
