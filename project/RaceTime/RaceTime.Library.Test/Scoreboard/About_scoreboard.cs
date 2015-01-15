using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Scoreboard;

namespace RaceTime.Library.Test.Scoreboard
{
    [TestClass]
    public class About_Scoreboard 
    {

        [TestMethod]
        public void for_example()
        {
            var scoreboard = new SerialScoreboard();

            scoreboard.Interval = 1000;
            
            scoreboard.WriteOutput("testing");
        }
    }
}
