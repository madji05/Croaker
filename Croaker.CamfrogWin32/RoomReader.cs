using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Croaker.CamfrogWin32
{
    public class RoomReader
    {
        CamfrogWin32 win32 = null;
        private string roomName = null;
        private Thread readingThread = null;
        private string lastLine = null;
        private bool isReading = false;
        public delegate void MessageReadEventHandler(MessageReadEventArgs e);
        public MessageReadEventHandler NewMessageRead;

        public bool IsReading
        {
            get { return isReading; }
        }

        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        public RoomReader()
        {
            win32 = new CamfrogWin32();
        }

        public void StartReading()
        {
            this.isReading = true;
            win32.RoomName = roomName;
            win32.GetChatRoomHandle();
            win32.GetChatWindowHandle(win32.ChatRoomHandle);

            readingThread = new Thread(new ThreadStart(ReadingThreadTarget));
            readingThread.Start();
        }

        public void StopReading()
        {
            isReading = false;
            readingThread.Abort();
        }

        private void ReadingThreadTarget()
        {
            while (this.isReading)
            {
                string chatText = win32.GetChatText(win32.ChatWindowHandle);

                if (!string.IsNullOrEmpty(chatText))
                {
                    string[] lines = chatText.Split(new string[] { "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
                    string last = lines[lines.Length - 1];

                    if (string.IsNullOrWhiteSpace(last))
                    {
                        last = lines[lines.Length - 2];
                    }

                    if (!last.Equals(this.lastLine))
                    {
                        string nick = null;
                        string msg = null;

                        this.lastLine = last;

                        if (!string.IsNullOrEmpty(last) && last.IndexOf(':') > 0)
                        {
                            nick = last.Substring(0, last.IndexOf(':')).ToLower();
                            if (!string.IsNullOrEmpty(nick))
                            {
                                msg = last.Substring(last.IndexOf(':') + 2, last.Length - (last.IndexOf(':') + 2));
                                if (NewMessageRead != null)
                                {
                                    NewMessageRead(new MessageReadEventArgs(nick, msg));
                                }
                            }
                        }                        
                    }
                }
            }
        }
    }

    public class MessageReadEventArgs : EventArgs
    {
        private string _nickName;
        private string _message;

        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public MessageReadEventArgs(string nick, string message)
        {
            _nickName = nick;
            _message = message;
        }
    }
}
