using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Croaker.CamfrogWin32
{
    public class MicUserStats
    {
        private string _nickName;
        private TimeSpan _totalTime;

        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }

        public TimeSpan TotalTime
        {
            get { return _totalTime; }
            set { _totalTime = value; }
        }

        public MicUserStats(string nick, TimeSpan time)
        {
            _nickName = nick;
            _totalTime = time;
        }

    }
}
