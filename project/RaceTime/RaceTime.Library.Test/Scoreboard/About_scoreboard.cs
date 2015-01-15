using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Scoreboard;

namespace RaceTime.Library.Test.Scoreboard
{
    [TestClass]
    public class About_scoreboard : Scoreboard_AcceptanceTest
    {

        [TestMethod]
        public void it_works_with_debug_scoreboard()
        {
            Given_a_practice_schedule();

            Using_a_debug_scoreboard();

            Then_run_schedule();

            Thread.Sleep(7000);
        }
        
        [TestMethod]
        public void it_works_with_serial_scoreboard()
        {
            Given_a_practice_schedule();

            Using_a_serial_scoreboard();

            Then_run_schedule();

            Thread.Sleep(7000);
        }
        
        [TestMethod]
        public void it_works_with_a_number_of_rounds()
        {
            Given_a_practice_schedule();

            When_set_to_three_rounds();

            Using_a_debug_scoreboard();

            Then_run_schedule();

            Thread.Sleep((1000 * 60 * 1 )* 4);

            Then_current_round_should_be_three();
        }


        [TestMethod]
        public void for_example()
        {
            var scoreboard = new SerialScoreboard();

            scoreboard.Interval = 1000;
            
            scoreboard.WriteOutput("testing");
        }
    }
}
