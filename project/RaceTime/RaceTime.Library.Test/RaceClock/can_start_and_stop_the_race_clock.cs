using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RaceTime.Library.Test.RaceClock
{
    [TestClass]
    public class can_start_and_stop_the_race_clock : RaceClockAcceptanceTest
    {
        [TestInitialize]
        public void BeingWith()
        {
            Given_a_race_clock(Fiveminutes);
        }

        [TestMethod]
        public void the_clock_it_stores_the_racetime()
        {
            Then_its_race_time_is_set_to(Fiveminutes);
        }

        [TestMethod]
        public void when_we_start_the_clock_it_starts_counting()
        {
            When_I_start_the_clock();

            And_wait_for_a_second();

            Then_the_clock_counts_up_a_second();
        }

        [TestMethod]
        public void the_clock_needs_to_start_before_we_can_stop()
        {
            When_I_stop_the_clock();

            Then_I_get_an_error("Clock cannot be stopped.");
            
        }

        [TestMethod]
        public void when_we_stop_the_clock_it_stops_counting()
        {
            When_I_start_the_clock();

            And_wait_for_a_second_and_stop_the_clock();

            Then_the_clock_stops_counting();
        }
    }
}
