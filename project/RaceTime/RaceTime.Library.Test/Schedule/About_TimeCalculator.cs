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
        public void for_example_with_no_rounds()
        {
            Given_a_set_of_3_practice_classes();

            Given_no_rounds();

            When_we_have_a_start_time_of_9_am();

            When_we_calculate_end_time();

            Then_start_time_is_9_am();

            Then_the_end_time_should_be_9_03();
        }


        [TestMethod]
        public void for_example_with_multiple_rounds()
        {
            Given_a_set_of_3_practice_classes();

            Given_8_rounds();

            When_we_have_a_start_time_of_9_am();

            When_we_calculate_end_time();
            
            Then_start_time_is_9_am();
            
            Then_the_end_time_should_be_9_24();
        }

    
    }
}
