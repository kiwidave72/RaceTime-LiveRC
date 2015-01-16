using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RaceTime.Library.Test.Practice.Support;
using System.Threading;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Test.Practice.Support.Interactor;


namespace RaceTime.Library.Test.Practice
{
    [TestClass]
    public class About_running_practice_with_schedule : Practice_AcceptanceTest
    {
        [TestMethod]
        public void for_example()
        {
            Given_a_meeting_called("test");

            Can_add_to_the_practice_schedule(new PracticeClass("Practice 1", 3000));

            Can_add_to_the_practice_schedule(new PracticeClass("Practice 2", 3000));
                        
            Given_the_schedule_run_with_raceclock();

            Thread.Sleep(100);

            Then_current_practice_is("Practice 1");

            Then_next_practice_is("Practice 2");

            Thread.Sleep(7000);

        }


    }
}
