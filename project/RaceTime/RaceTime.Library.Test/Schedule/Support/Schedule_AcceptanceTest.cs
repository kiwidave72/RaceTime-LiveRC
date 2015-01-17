using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model.Announcement;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Model.Schedule;
using RaceTime.Library.Scoreboard;
using RaceTime.Library.Test.Practice.Support.Interactor;
using System;
using System.Collections.Generic;

namespace RaceTime.Library.Test.Scoreboard
{
    public class Schedule_AcceptanceTest
    {
        private IScoreboard _scoreboard;

        private List<Announcement> raisedAnnouncments = new List<Announcement>();
        
        public PracticeInteractor interactor;

        public Schedule_AcceptanceTest()
        {
            interactor = new PracticeInteractor();
        }


        public void And_interval_and_stopped_announcements()
        {

            interactor.AddAnnouncmentToSchedule(new Announcement(ScheduleEventType.IntervalStarted, "Interval Started"));

            interactor.AddAnnouncmentToSchedule(new Announcement(ScheduleEventType.Stopped, "Practice Stopped"));

            interactor.Schedule.OnAnnouncement += (sender, e) => raisedAnnouncments.Add(e);
            
        }

        public void And_these_announcements()
        {
            interactor.AddAnnouncmentToSchedule(new Announcement(ScheduleEventType.IntervalStarted, "Interval Started"));

            interactor.AddAnnouncmentToSchedule(new Announcement(ScheduleEventType.Started, "Practice Started"));

            interactor.AddAnnouncmentToSchedule(new Announcement(ScheduleEventType.Finished, "Practice Finished"));

            interactor.Schedule.OnAnnouncement += (sender, e) => raisedAnnouncments.Add(e);
            
        }

        protected void Given_a_practice_schedule()
        {

            interactor.All().Clear();

            interactor.SetTitle("test");

            interactor.AddToSchedule(new PracticeClass("Practice 1", 1000 * 3));

            interactor.AddToSchedule(new PracticeClass("Practice 2", 1000 * 3));
        }

        protected void Given_two_quick_practice_races()
        {
            interactor.All().Clear();

            interactor.SetTitle("test");

            interactor.AddToSchedule(new PracticeClass("Practice 1", 2000));

            interactor.AddToSchedule(new PracticeClass("Practice 2", 2000));

            interactor.SetNumberOfRounds(1);

        }

        public void And_we_wait_until_the_end_of_the_schedule()
        {
            Thread.Sleep(With_the_length_of_the_schedule() + 1000);
        }


        public void Then_the_schedules_has_finished()
        {
            Assert.IsTrue(interactor.CurrentPractice().Status == "Finished");

            Assert.IsTrue(interactor.IsScheduleRunning() == false);
        }

        public void Then_the_schedules_current_practice_status_is_stopped()
        {
            Assert.IsTrue(interactor.CurrentPractice().Status=="Stopped");
        }

        public void Then_the_schedule_has_stopped()
        {
            Assert.IsTrue(interactor.IsScheduleRunning() == false); 
        }


        public void Then_stop_schedule()
        {
            interactor.StopSchedule();
        }

        protected void When_set_to_two_rounds()
        {
            interactor.SetNumberOfRounds(2);
        }

        protected void Then_current_round_should_be_two()
        {

            Debug.WriteLine(interactor.CurrentRound());
            Assert.IsTrue(interactor.CurrentRound() == 2);
        }

        public int With_the_length_of_the_schedule()
        {
            int total_time = 0;

            foreach (var practiceClass in interactor.All())
            {
                total_time = total_time + (int)practiceClass.Time + (int)interactor.Schedule.Interval;
            }

            total_time = total_time * interactor.GetNumberOfRounds();

            return total_time ;
        }


        protected void When_the_schedule_is_run()
        {
            interactor.RunSchedule(_scoreboard,1000);
        }

        protected void Then_the_schedule_is_running()
        {
            Assert.IsTrue(interactor.IsScheduleRunning()); 
        }

        protected void Using_a_debug_scoreboard()
        {
            _scoreboard = new DebugScoreboard();
        }
        
        protected void Using_a_serial_scoreboard()
        {
            _scoreboard = new SerialScoreboard();
        }

        protected void And_we_have_all_the_annoucements_returned()
        {
            Assert.IsTrue(raisedAnnouncments.Count == 6);

            Assert.IsTrue(raisedAnnouncments[0].Text == "Interval Started");

            Assert.IsTrue(raisedAnnouncments[1].Text == "Practice Started");

            Assert.IsTrue(raisedAnnouncments[2].Text == "Practice Finished");

            Assert.IsTrue(raisedAnnouncments[3].Text == "Interval Started");

            Assert.IsTrue(raisedAnnouncments[4].Text == "Practice Started");

            Assert.IsTrue(raisedAnnouncments[5].Text == "Practice Finished");
        }

        protected void And_we_just_get_interval_and_stopped_annoucement_returned()
        {
            Assert.IsTrue(raisedAnnouncments.Count == 2);
            Assert.IsTrue(raisedAnnouncments[0].Text == "Interval Started");
            Assert.IsTrue(raisedAnnouncments[1].Text == "Practice Stopped");
        }

        protected static void And_wait_for_the_interval()
        {
            Thread.Sleep(1500);
        }
    }
}