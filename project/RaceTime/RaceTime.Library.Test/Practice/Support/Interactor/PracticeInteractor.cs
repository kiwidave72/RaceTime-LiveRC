using System.Collections.Generic;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Test.Practice.Support.Interactor
{
    public class PracticeInteractor
    {
        private static Model.Meeting.Meeting _meeting = new Model.Meeting.Meeting();

        public static void SetTitle(string title)
        {
            _meeting.Title = title;
        }

        public static void SetNumberOfRounds(int number)
        {
            _meeting.Schedule.NumberOfRound = number;
        }

        public static void AddToSchedule(PracticeClass practice)
        {
            _meeting.Schedule.Add(practice);
        }

        public static List<PracticeClass> All()
        {
            return _meeting.Schedule.Fetch();
        }

        public static int CurrentRound()
        {
            return _meeting.Schedule.CurrentRound;
        }

        public static PracticeClass CurrentPractice()
        {
            return _meeting.Schedule.CurrentPracticeClass;
        }

        public static PracticeClass NextPractice()
        {
            return _meeting.Schedule.NextPracticeClass;
        }


        public static void RunSchedule(IScoreboard _scoreboard)
        {
            _meeting.Schedule.Scoreboard = _scoreboard;
            _meeting.Schedule.Run();
        }
    }
}
