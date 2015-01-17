using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Scoreboard;
using RaceTime.Library.Test.Practice.Support.Interactor;
using System;

namespace RaceTime.Library.Test.Scoreboard
{
    public class Schedule_AcceptanceTest
    {
        private IScoreboard _scoreboard;

        private PracticeInteractor interactor;

        public Schedule_AcceptanceTest()
        {
            interactor = new PracticeInteractor();
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
                total_time = total_time + (int)practiceClass.Time;
            }

            total_time = total_time * interactor.GetNumberOfRounds();

            return total_time ;
        }


        protected void Then_run_schedule()
        {
            interactor.RunSchedule(_scoreboard);
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

    }
}