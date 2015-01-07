using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;


namespace RaceTime.Library.Model.Meeting
{

    [Serializable()]
    public class Meeting
    {
        private MeetingClasses _classes = new MeetingClasses();

        private List<long> _schedule = new List<long>();

        Controller.RaceClock _clock = new Controller.RaceClock();

        private String _title;

        public Controller.RaceClock Clock
        {
            get { return _clock; }
        }

        public MeetingClasses Classes
        {
            get { return _classes; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public void AddToSchedule(long time)
        {
            _schedule.Add(time);
        }

        public List<long> FetchAllSchedule()
        {
            return _schedule;
        }

        public void RunSchedule()
        {
            var time = _schedule.First();

            _schedule.Remove(time);

            Clock.SetRaceTime(time);

            Clock.Start();

            Clock.OnElapsedHasExpired += Clock_OnElapsedHasExpired;

        }

        void Clock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            Clock.Stop();

            RunSchedule();

        }
    }
}