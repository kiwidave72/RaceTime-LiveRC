using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RaceTime.Library.Test.RaceClock
{
    [TestClass]
    public class it_keeps_counting_until_finished : RaceClockAcceptanceTest
    {
        private int twoSeconds = 2000;

        [TestInitialize]
        public void BeingWith()
        {
            Given_a_race_clock(twoSeconds);

            When_I_start_the_clock();

            When_we_wait_for_three_seconds();

            Then_the_clock_is_still_counting();
        }


        [TestMethod]
        public void it_keeps_going()
        {
            And_the_elapsed_time_has_passed_the_time_I_set();
        }

        [TestMethod]
        public void once_its_finished_it_stops()
        {
            When_I_stop_and_finish_the_race();

            Then_the_clock_stops_counting();
        }

        private void When_I_stop_and_finish_the_race()
        {
            RaceClock.StoppedAndFinished();
        }


        private static void When_we_wait_for_three_seconds()
        {
            Thread.Sleep(3000);
        }

        private void And_the_elapsed_time_has_passed_the_time_I_set()
        {
            Assert.IsTrue(RaceClock.Elapsed()>RaceClock.RaceTime);
        }

        private void Then_the_clock_is_still_counting()
        {
            Assert.IsTrue(RaceClock.HasElapsed==true && RaceClock.HasStarted==true);
        }
    }
}
