using System;

namespace RaceTime.Library.Model.Meeting
{

    [Serializable()]
    public class Meeting
    {
        private MeetingClasses _classes = new MeetingClasses();

        private String _title;

        public MeetingClasses Classes
        {
            get { return _classes; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
    }
}