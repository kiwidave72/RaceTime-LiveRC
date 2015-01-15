using System.Diagnostics;
using RaceTime.Library.Controller.Scoreboard;

namespace RaceTime.Library.Test.Scoreboard
{
    public class DebugScoreboard :  IScoreboard
    {
        private long _interval = 1000;

        public long Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public void WriteOutput(string line)
        {
            Debug.WriteLine(line);
        }
    }
}