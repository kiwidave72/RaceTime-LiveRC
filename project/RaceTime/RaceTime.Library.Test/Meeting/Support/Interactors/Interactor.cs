using System.Collections.Generic;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Race;
using RaceTime.Library.Model.Schedule;

namespace RaceTime.Library.Test.Meeting.Support
{
    internal class Interactor
    {
        private static Model.Meeting.Meeting _meeting = new Model.Meeting.Meeting(new DefaultSchedule(null));

        public static List<RaceClass> FetchAll()
        {
            return _meeting.Classes.All();
        }

        public static void CreateClass(RaceClass newClass)
        {
            _meeting.Classes.Create(newClass);
        }

        public static void SetTitle(string title)
        {
            _meeting.Title = title;
        }

        public static string GetTitle()
        {
            return _meeting.Title;
        }

        public static void ClearClasses()
        {
            _meeting.Classes.Clear();
        }
    }
}