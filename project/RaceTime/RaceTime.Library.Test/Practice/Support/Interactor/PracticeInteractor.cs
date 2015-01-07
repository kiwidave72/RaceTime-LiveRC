using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceTime.Library.Test.Practice.Support.Interactor
{
    public class PracticeInteractor
    {
        private static Model.Meeting.Meeting _meeting = new Model.Meeting.Meeting();

        public static void SetTitle(string title)
        {
            _meeting.Title = title;
        }

        public static void AddSchedule(long time)
        {
            _meeting.AddToSchedule(time);
        }

        public static List<long> All()
        {
            return _meeting.FetchAllSchedule();
        }

        public static void RunSchedule()
        {
            _meeting.RunSchedule();
        }
    }
}
