using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using RaceTime.Library.Model.Practice;


namespace RaceTime.Library.Model.Meeting
{

    [Serializable()]
    public class Meeting
    {
        private MeetingClasses _classes = new MeetingClasses();

        private List<PracticeClass> _schedule = new List<PracticeClass>();

        private Controller.RaceClock _clock = new Controller.RaceClock();

        private PracticeClass _currentPracticeClass;

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

        public void AddToSchedule(PracticeClass practice)
        {
            _schedule.Add(practice);
        }

        public List<PracticeClass> FetchAllSchedule()
        {
            return _schedule;
        }

        
        public void RunSchedule()
        {
            _currentPracticeClass = _schedule.FirstOrDefault(i => i.Status == "Ready");

            _currentPracticeClass.Status = "Running";

            Clock.SetRaceTime(_currentPracticeClass.Time);

            Clock.Start();

            Clock.OnElapsedHasExpired += Clock_OnElapsedHasExpired;

        }

        void Clock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            Clock.Stop();

            _currentPracticeClass.Status = "Finished";

            RunSchedule();

        }
    }
}