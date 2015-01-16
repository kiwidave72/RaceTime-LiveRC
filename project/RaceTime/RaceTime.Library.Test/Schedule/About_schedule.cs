using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Test.Scoreboard;

namespace RaceTime.Library.Test.Schedule
{
    [TestClass]
    public class About_schedule : Schedule_AcceptanceTest
    {
        [TestMethod]
        public void it_works_with_debug_scoreboard()
        {
            Given_two_quick_practice_races();

            Using_a_debug_scoreboard();

            Then_run_schedule();

            Thread.Sleep(2500);
        }

        [TestMethod]
        public void it_works_with_serial_scoreboard()
        {
            Given_two_quick_practice_races();

            Using_a_serial_scoreboard();

            Then_run_schedule();

            Thread.Sleep(2500);
        }

        [TestMethod]
        public void it_works_with_a_number_of_rounds()
        {
            Given_two_quick_practice_races();

            When_set_to_two_rounds();

            Using_a_debug_scoreboard();

            Then_run_schedule();

            Thread.Sleep((2000) * 2);

            Then_current_round_should_be_two();
        }

    }
}
