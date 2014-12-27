using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RaceTime.Library.Test.RaceClock
{
    [TestClass]
    public class can_reset_the_race_clock : RaceClockAcceptanceTest
    {
        [TestInitialize]
        public void BeingWith()
        {
            Given_a_race_clock(Fiveminutes);
        }

        [TestMethod]
        public void The_clock_needs_to_stop_before_we_can_reset()
        {
            When_I_start_the_clock();

            When_I_reset_the_clock();

            Then_I_get_an_error("");
        }

        [TestMethod]
        public void The_clock_can_be_reset()
        {
            When_I_start_the_clock();

            And_wait_for_a_second_and_stop_the_clock();

            When_I_reset_the_clock();

            Then_the_clocks_elapsed_gets_reset();
        }

        private void Then_the_clocks_elapsed_gets_reset()
        {
            Assert.IsTrue(RaceClock.Elapsed()==0);

        }
    }
}