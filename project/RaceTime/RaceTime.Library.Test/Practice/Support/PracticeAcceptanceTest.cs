using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceTime.Library.Test.Practice.Support.Interactor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Test.Practice.Support
{
    public class Practice_AcceptanceTest
    {
       
        public void Given_a_meeting_called(string test)
        {
            PracticeInteractor.SetTitle(test);
        }

        public void Can_add_to_the_practice_schedule(PracticeClass practice)
        {
            PracticeInteractor.AddToSchedule(practice);
        }

        public void Then_I_have_this_many(int expected)
        {
            Assert.IsTrue(PracticeInteractor.All().Count() == expected );

        }

        public void Given_the_schedule_run_with_raceclock()
        {
            PracticeInteractor.RunSchedule(null);
        }

        public void Then_current_practice_is(string name)
        {
            Assert.IsTrue(PracticeInteractor.CurrentPractice().Name == name);
        }

        public void Then_next_practice_is(string name)
        {
            Assert.IsTrue(PracticeInteractor.NextPractice().Name == name);
        }

    }
}
