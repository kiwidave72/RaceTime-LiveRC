using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RaceTime.Library.Test.RaceClock
{
    [TestClass]
    public class about_race_clock_events : RaceClockAcceptanceTest
    {
        private int twoSeconds = 2000;

        [TestInitialize]
        public void BeingWith()
        {
            Given_a_race_clock(twoSeconds);
        }

        [TestMethod]
        public void for_example_onstarted_when_the_clock_starts()
        {
            var eventRaised = false;

            RaceClock.OnStarted += (sender, e) =>
            {
                eventRaised = true;
            };

            When_I_start_the_clock();

            And_I_wait_for_three_seconds();

            Assert.IsTrue(eventRaised == true);
        }

        [TestMethod]
        public void for_example_onstopped_when_the_clock_is_stopped()
        {
            var eventRaised = false;

            RaceClock.OnStopped += (sender, e) =>
            {
                eventRaised = true;
            };

            When_I_start_the_clock();

            When_I_stop_the_clock();
            
            Assert.IsTrue(eventRaised == true);
        }

        [TestMethod]
        public void for_example_onreset_when_the_clock_is_reset()
        {
            var eventRaised = false;

            RaceClock.OnReset  += (sender, e) =>
            {
                eventRaised = true;
            };

            When_I_start_the_clock();

            When_I_stop_the_clock();

            When_I_reset_the_clock();

            Assert.IsTrue(eventRaised == true);
        }


        [TestMethod]
        public void for_example_onelapsedhasexpired_when_the_clock_runs_out()
        {
            var eventRaised = false;

            RaceClock.OnElapsedHasExpired += (sender, e) =>
            {
                eventRaised = true;

                Then_the_clock_stops_counting();
            };
            
            When_I_start_the_clock();

            And_I_wait_for_three_seconds();

            Assert.IsTrue(eventRaised==true);
        }

        private void And_I_wait_for_three_seconds()
        {
           Thread.Sleep(3000);

        }
    }
}
