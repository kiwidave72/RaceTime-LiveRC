using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Test.Schedule
{
    [TestClass]
    public class About_TimeCalculator : TimeCalculatorAcceptanceTest
    {

        [TestMethod]
        public void for_example()
        {
            Given_a_set_of_3_practice_classes();

            When_we_have_a_start_time_of_9_am();

            Then_start_time_is_9_am();

            Then_the_end_time_should_be_9_03();

        }
    }
}
