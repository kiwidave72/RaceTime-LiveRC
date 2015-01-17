using System.Collections.Generic;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Test.Practice.Support.Interactor
{
    public class PracticeInteractor
    {
        private  Model.Meeting.Meeting _meeting = new Model.Meeting.Meeting();

        public void SetTitle(string title)
        {
            _meeting.Title = title;
        }

        public void SetNumberOfRounds(int number)
        {
            _meeting.Schedule.NumberOfRound = number;
        }

        public int GetNumberOfRounds()
        {
            return _meeting.Schedule.NumberOfRound ;
        }


        public void AddToSchedule(PracticeClass practice)
        {
            _meeting.Schedule.Add(practice);
        }

        public List<PracticeClass> All()
        {
            return _meeting.Schedule.Fetch();
        }

        public int CurrentRound()
        {
            return _meeting.Schedule.CurrentRound;
        }

        public PracticeClass CurrentPractice()
        {
            return _meeting.Schedule.CurrentPracticeClass;
        }

        public bool IsScheduleRunning()
        {
            return _meeting.Schedule.IsScheduleRunning;
        }
        
        public PracticeClass NextPractice()
        {
            return _meeting.Schedule.NextPracticeClass;
        }


        public void RunSchedule(IScoreboard _scoreboard)
        {
            _meeting.Schedule.Scoreboard = _scoreboard;
            _meeting.Schedule.Run();
        }

        public void StopSchedule()
        {
            _meeting.Schedule.Stop();
        }
    }
}
