using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RaceTime.Library.Test.Scoreboard
{
    [TestClass]
    public class About_scoreboard : Scoreboard_AcceptanceTest
    {

        [TestMethod]
        public void for_example()
        {
            Given_a_practice_schedule();

            Using_a_debug_scoreboard();

            Then_run_schedule();

            Thread.Sleep(7000);
        }

    }
}
