using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Test.Schedule
{
    public class TimeCalculatorAcceptanceTest
    {
        protected readonly List<PracticeClass> list = new List<PracticeClass>();

        protected DateTime startTime;

        protected int roundCount;
        
        private DateTime endDateTime;


        protected void Given_a_set_of_3_practice_classes()
        {
            list.Add(new PracticeClass("Race1", 1));

            list.Add(new PracticeClass("Race2", 1));

            list.Add(new PracticeClass("Race3", 1));
        }

        protected void Given_no_rounds()
        {
            roundCount = 1;
        }


        protected void Then_start_time_is_9_am()
        {
            Assert.IsTrue(startTime.ToShortTimeString() == "9:00 a.m.", "was expecting 9am but got <" + startTime.ToShortTimeString() + ">");
        }

        protected void When_we_have_a_start_time_of_9_am()
        {
            startTime = DateTime.Parse("9:00");
        }

        protected void Then_the_end_time_should_be_9_03()
        {

            Assert.IsTrue(endDateTime.ToShortTimeString() == "9:03 a.m.", "Expected '9:03 a.m.' got <" + endDateTime.ToShortTimeString() + ">");
        }

        protected void When_we_calculate_end_time()
        {
            endDateTime = TimeCalculator.CalculateTime(startTime, list,roundCount);
        }

        protected void Then_the_end_time_should_be_9_24()
        {

            Assert.IsTrue(endDateTime.ToShortTimeString() == "9:24 a.m.", "Expected '9:24 a.m.', for <" + endDateTime.ToShortTimeString() + ">");
        }

        protected void Given_8_rounds()
        {
            roundCount = 8;
        }
    }
}