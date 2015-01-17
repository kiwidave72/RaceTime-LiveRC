using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Schedule;
using RaceTime.Library.Test.Meeting.Support;
using RaceTime.Library.Test.Scoreboard;

namespace RaceTime.Library.Test.Schedule
{
    [TestClass]
    public class About_annoucements : Schedule_AcceptanceTest
    {
        [TestMethod]
        public void for_example()
        {
            
            Given_two_quick_practice_races();

            And_these_announcements();
            
            When_the_schedule_is_run();

            And_we_wait_until_the_end_of_the_schedule();

            Then_the_schedules_has_finished();

            And_we_have_all_the_annoucements_returned();
            
        }

       
    }
}
