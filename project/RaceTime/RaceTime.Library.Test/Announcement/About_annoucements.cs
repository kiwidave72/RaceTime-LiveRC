using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model;
using RaceTime.Library.Test.Scoreboard;

namespace RaceTime.Library.Test.Announcement
{
    [TestClass]
    public class About_annoucements : Schedule_AcceptanceTest
    {
        [TestMethod]
        public void announcements_are_raised_during_the_running_of_the_schedule()
        {
            
            Given_two_quick_practice_races();

            And_these_announcements();
            
            When_the_schedule_is_run_with_an_interval();

            And_wait_for_the_interval();

            And_we_wait_until_the_end_of_the_schedule();

            Then_the_schedules_has_finished();

            And_we_have_all_the_annoucements_returned();
            
        }


        [TestMethod]
        public void when_schedule_is_stopped_the_annoucement_is_stopped()
        {

            Given_two_quick_practice_races();

            And_interval_and_stopped_announcements();

            When_the_schedule_is_run_with_an_interval();

            And_wait_for_the_interval();

            Then_stop_schedule();
            
            Then_the_schedule_has_stopped();

            And_we_just_get_interval_and_stopped_annoucement_returned();

        }

        [TestMethod]
        public void you_can_change_the_annoucements_during_the_running()
        {
            Given_two_quick_practice_races();

            And_these_announcements();

            When_the_schedule_is_run();

            When_a_annoucement_can_be_change_to(ScheduleEventType.Started, "Has Changed to this Practice Started");

            And_we_wait_until_the_end_of_the_schedule();

            Then_the_schedules_has_finished();

            Then_the_annoucement_are_like_this(ScheduleEventType.Started, "Has Changed to this Practice Started");

            And_we_have_all_the_annoucements_returned_event_the_old_and_new();

        }

       

    }
}
