using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Test.Practice.Support.Interactor;
using System;

namespace RaceTime.Library.Test.Scoreboard
{
    public class Scoreboard_AcceptanceTest
    {
        private IScoreboard _scoreboard;

        protected void Given_a_practice_schedule()
        {
            PracticeInteractor.SetTitle("test");

            PracticeInteractor.AddSchedule(new PracticeClass("Practice 1", 3000));

            PracticeInteractor.AddSchedule(new PracticeClass("Practice 2", 3000));

        }

        protected void Then_run_schedule()
        {
            PracticeInteractor.RunSchedule(_scoreboard);
        }

        protected void Using_a_debug_scoreboard()
        {
            _scoreboard = new DebugScoreboard();

            
        }
    }
}