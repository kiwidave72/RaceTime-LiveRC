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

        protected void Given_a_practice_schedule()
        {
            PracticeInteractor.SetTitle("test");

            PracticeInteractor.AddToSchedule(new PracticeClass("Practice 1", 1000 * 3 ));

            PracticeInteractor.AddToSchedule(new PracticeClass("Practice 2", 1000 * 3));
        }

        protected void Given_two_quick_practice_races()
        {
            PracticeInteractor.SetTitle("test");

            PracticeInteractor.AddToSchedule(new PracticeClass("Practice 1", 1000));

            PracticeInteractor.AddToSchedule(new PracticeClass("Practice 2", 1000));
        }



        protected void When_set_to_two_rounds()
        {
            PracticeInteractor.SetNumberOfRounds(2);
        }

        protected void Then_current_round_should_be_two()
        {

            Debug.WriteLine(PracticeInteractor.CurrentRound());
            Assert.IsTrue( PracticeInteractor.CurrentRound() == 2);
        }


        protected void Then_run_schedule()
        {
            PracticeInteractor.RunSchedule(_scoreboard);
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