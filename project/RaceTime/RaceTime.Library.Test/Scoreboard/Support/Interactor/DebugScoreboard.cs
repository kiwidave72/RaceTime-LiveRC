using System;
using System.Collections.Generic;
using System.Diagnostics;
using RaceTime.Library.Controller.Scoreboard;

namespace RaceTime.Library.Test.Scoreboard
{
    public class DebugScoreboard :  IScoreboard
    {
        private long _interval = 1000;

        public string FriendlyOutputText { get; set; }
        public string SerialOutputText { get; set; }
        public string[] SerialPortNames { get; set; }
        public string PortName { get; set; }

        public List<Exception> Errors { get; private set; }

        public DebugScoreboard()
        {
            Errors=new List<Exception>();
        }

        public long Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public void ClearDisplay()
        {
            throw new NotImplementedException();
        }

        public void WriteRaceInfor(int round, int heat, string elapsedTime, string name)
        {
            var serialOutputText = string.Format("{0}:{1}:{2}:{3}:{4}", round, heat, elapsedTime);

            FriendlyOutputText = string.Format("Round:{0} Heat:{1} Time:{2} Name:{3}", round, heat, elapsedTime, name); 

            WriteOutput(serialOutputText);
        }


        private void WriteOutput(string line)
        {
            SerialOutputText = line;
            Debug.WriteLine(line);
        }
    }
}