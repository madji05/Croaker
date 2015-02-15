using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Croaker.CamfrogWin32
{
    public class MicWatcher
    {
        CamfrogWin32 win32 = null;
        private Thread watcher;
        private bool _isWatching = false;
        private string lastPersonOnMic = null;
        public delegate void MicUserChangedEventHandler(MicUserChangedEventArgs e);
        public delegate void MicDroppedEventHandler(MicDroppedEventArgs e);
        public MicUserChangedEventHandler MicUserChanged;
        public MicDroppedEventHandler MicDropped;
        private bool someoneHasMic = true;
        int michWnd = 0;
        private DateTime tempStartedTalkingWhen;
        private string _roomName;

        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }

        public string LastPersonOnMic
        {
            get { return lastPersonOnMic; }
            set { lastPersonOnMic = value; }
        }

        public bool IsWatching
        {
            get { return _isWatching; }
            set { _isWatching = value; }
        }

        public MicWatcher(string roomName)
        {
            win32 = new CamfrogWin32();
            _roomName = roomName;
            win32.RoomName = _roomName;
        }

        public void StartWatching()
        {
            _isWatching = true;
            win32.GetChatRoomHandle();

            win32.RegisterControlforMessages();

            michWnd = win32.GetWhoOnMicDialg(win32.ChatRoomHandle);

            watcher = new Thread(new ThreadStart(WatchingThreadTarget));
            watcher.Start();
        }

        public void StopWatching()
        {
            _isWatching = false;
            if (watcher != null)
                watcher.Abort();
        }

        public void WatchingThreadTarget()
        {
            while (_isWatching)
            {                
                bool update = win32.RefreshUpdateWindow(new IntPtr(michWnd));
                //int len = win32.SendMessageToWindow(michWnd, CamfrogWin32.WM_GETTEXTLENGTH).ToInt32();
                string text = win32.GetWindowText(michWnd);
                bool visible = win32.GetIsWindowVisible(new IntPtr(michWnd));

                if (!visible && !string.IsNullOrEmpty(lastPersonOnMic))
                {
                    someoneHasMic = false;
                    if (MicDropped != null)
                    {
                        MicDropped(new MicDroppedEventArgs(lastPersonOnMic,
                            (TimeSpan)DateTime.Now.Subtract(tempStartedTalkingWhen)));
                    }
                    lastPersonOnMic = null;
                }                

                if (text != lastPersonOnMic && visible)
                {
                    if (lastPersonOnMic != null)
                    {
                        if (MicDropped != null)
                        {
                            MicDropped(new MicDroppedEventArgs(lastPersonOnMic, 
                                (TimeSpan)DateTime.Now.Subtract(tempStartedTalkingWhen)));
                        }
                    }

                    lastPersonOnMic = text;
                    tempStartedTalkingWhen = DateTime.Now;
                    someoneHasMic = visible;

                    if (MicUserChanged != null)
                    {
                        MicUserChanged(new MicUserChangedEventArgs(text));
                    }
                }                
            }
        }
    }

    public class MicUserChangedEventArgs : EventArgs
    {
        private string _nickName;

        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }

        public MicUserChangedEventArgs(string nick)
        {
            _nickName = nick;
        }
    }

    public class MicDroppedEventArgs : EventArgs
    {
        private string _lastNickOnMic;
        private TimeSpan _timeOnMic;

        public TimeSpan TimeOnMic
        {
            get { return _timeOnMic; }
            set { _timeOnMic = value; }
        }

        public string LastNickOnMic
        {
            get { return _lastNickOnMic; }
            set { _lastNickOnMic = value; }
        }

        public MicDroppedEventArgs(string nick, TimeSpan time)
        {
            _lastNickOnMic = nick;
            _timeOnMic = time;
        }
    }
}
