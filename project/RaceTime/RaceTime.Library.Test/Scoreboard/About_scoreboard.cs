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
        public void  scoreboard_text_is_outputed()
        {
            var scoreboard = new DebugScoreboard();

            scoreboard.Interval = 1000;

            scoreboard.WriteRaceInfor(1,1,"1","testing");

            Assert.IsTrue(scoreboard.FriendlyOutputText =="testing");
        }
    }
}
