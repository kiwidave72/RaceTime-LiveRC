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

        protected void Given_a_set_of_3_practice_classes()
        {
            list.Add(new PracticeClass("Race1", 1));

            list.Add(new PracticeClass("Race2", 1));

            list.Add(new PracticeClass("Race3", 1));
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
            DateTime result = TimeCalculator.CalculateTime(startTime, list);

            Assert.IsTrue(result.ToShortTimeString() == "9:03 a.m.", "Expected '9:03 a.m.' got <" + result.ToShortTimeString() + ">");
        }
    }
}