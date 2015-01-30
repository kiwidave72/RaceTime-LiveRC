using System;
using System.Collections.Generic;

namespace RaceTime.Library.Controller.Scoreboard
{
    public interface    IScoreboard
    {
        List<Exception> Errors { get; }

        void ClearDisplay();
        
        void WriteRaceInfor(int round, int heat, string elapsedTime,string name);

        long Interval { get; set; }

        string FriendlyOutputText { get; set; }

        string SerialOutputText { get; set; }

        string[] SerialPortNames { get; set; }

        string PortName { get; set; }
    }

}