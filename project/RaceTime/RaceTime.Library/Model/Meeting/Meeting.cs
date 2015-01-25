using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Controller;
using RaceTime.Library.Model.Schedule;

namespace RaceTime.Library.Model.Meeting
{

    [Serializable()]
    public class Meeting
    {
        private MeetingClasses _classes = new MeetingClasses();

        private DefaultSchedule _schedule ;
        
        private String _title;

        public Meeting(DefaultSchedule schedule)
        {
            _schedule = schedule;
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public MeetingClasses Classes
        {
            get { return _classes; }
        }

        public DefaultSchedule Schedule
        {
            get { return _schedule; }
        }


    }
}