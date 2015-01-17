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

            When_the_schedule_is_run();

            Then_the_schedule_has_stopped();

            Thread.Sleep(1500);

            Then_the_schedule_is_running();

            And_we_wait_until_the_end_of_the_schedule();

            Then_the_schedules_has_finished();
        }

        
        [TestMethod]
        public void it_works_with_serial_scoreboard()
        {
            Given_two_quick_practice_races();

            Using_a_serial_scoreboard();

            When_the_schedule_is_run();

            And_wait_for_the_interval();

            Then_the_schedule_is_running();

            Thread.Sleep(With_the_length_of_the_schedule()+1500);

            Then_the_schedules_has_finished();

        }


        [TestMethod]
        public void it_works_with_a_number_of_rounds()
        {
            Given_two_quick_practice_races();

            When_set_to_two_rounds();

            Using_a_debug_scoreboard();

            When_the_schedule_is_run();

            And_wait_for_the_interval();

            Then_the_schedule_is_running();

            Thread.Sleep(With_the_length_of_the_schedule() + 1000);

            Then_current_round_should_be_two();
        }

        [TestMethod]
        public void you_can_start_and_stop_the_schedule()
        {

            Given_two_quick_practice_races();

            Using_a_debug_scoreboard();

            When_the_schedule_is_run();

            And_wait_for_the_interval();

            Then_the_schedule_is_running();

            Thread.Sleep(100);

            Then_stop_schedule();

            Then_the_schedule_has_stopped();

            Then_the_schedules_current_practice_status_is_stopped();

        }

    }
}
