using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RaceTime.Library.Test.RaceClock
{
    public class RaceClockAcceptanceTest
    {
        private int fiveminutes = (1000 * 60) * 5;


        private Controller.RaceClock _raceClock;

        private Exception _error;


        public Controller.RaceClock RaceClock
        {
            get { return _raceClock; }
        }

        public int Fiveminutes
        {
            get { return fiveminutes; }
        }

        public void Given_a_race_clock(int racetime)
        {
            _raceClock = new Controller.RaceClock();

            _raceClock.SetRaceTime(racetime);
        }

        public void Then_its_race_time_is_set_to(int racetime)
        {
            Assert.IsTrue(_raceClock.RaceTime==racetime);
        }


        public void When_I_start_the_clock()
        {
            RaceClock.Start();
        }

        public void When_I_stop_the_clock()
        {
            _error = null;
            try
            {
                RaceClock.Stop();
            }
            catch (Exception ex)
            {
                _error = ex;
            }
        }
        
        public void Then_the_clock_counts_up_a_second()
        {
            var elapsed = RaceClock.Elapsed();
            Assert.IsTrue(elapsed > 1000);
        }

        public void Then_the_clock_stops_counting()
        {
            var time = RaceClock.Elapsed();

            And_wait_for_a_second();

            var currentTime = RaceClock.Elapsed();

            Assert.IsTrue(time == currentTime);
        }

        public void And_wait_for_a_second_and_stop_the_clock()
        {
            And_wait_for_a_second();

            When_I_stop_the_clock();
        }

        public void And_wait_for_a_second()
        {
            Thread.Sleep(1000);
        }

        public void Then_I_get_an_error(string messageLikeThis)
        {
            Assert.IsTrue(_error != null);
            
            Assert.IsTrue(_error.Message==messageLikeThis,string.Format("Expected <{0}> and got <{1}>",messageLikeThis,_error.Message));
        }

        public void When_I_reset_the_clock()
        {
            _error = null;
            try
            {
                RaceClock.Reset();
            }
            catch (Exception ex)
            {
                _error = ex;
            }
        }
    }
}