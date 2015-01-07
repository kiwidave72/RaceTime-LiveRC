using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static void AddSchedule(PracticeClass practice)
        {
            _meeting.Schedule.Add(practice);
        }

        public static List<PracticeClass> All()
        {
            return _meeting.Schedule.Fetch();
        }

        public static void RunSchedule()
        {
            _meeting.Schedule.Run();
        }
    }
}
