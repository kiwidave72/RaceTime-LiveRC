using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceTime.Library.Test.Practice.Support.Interactor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Test.Scoreboard;

namespace RaceTime.Library.Test.Practice.Support
{
    public class Practice_AcceptanceTest
    {
        private PracticeInteractor interactor;

        public Practice_AcceptanceTest()
        {
            interactor = new PracticeInteractor();
        }

        public void Given_a_meeting_called(string test)
        {
            interactor.SetTitle(test);
        }

        public void Can_add_to_the_practice_schedule(PracticeClass practice)
        {
            interactor.AddToSchedule(practice);
        }

        public void Then_I_have_this_many(int expected)
        {
            Assert.IsTrue(interactor.All().Count() == expected);

        }

        public void Given_the_schedule_run_with_raceclock()
        {
            interactor.RunSchedule(new DebugScoreboard(),0);
        }

        public void Then_current_practice_is(string name)
        {
            Assert.IsTrue(interactor.CurrentPractice().Name == name);
        }

        public void Then_next_practice_is(string name)
        {
            Assert.IsTrue(interactor.NextPractice().Name == name);
        }

    }
}
