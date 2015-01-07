using RaceTime.Library.Model.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RaceTime.Library.Controller;


namespace RaceTime.Library.Model.Schedule
{
    public class DefualtSchedule
    {
        private List<PracticeClass> _schedule = new List<PracticeClass>();

        private RaceClock _clock = new RaceClock();

        private PracticeClass _currentPracticeClass;

        public List<PracticeClass> Schedule
        {
            get { return _schedule; }
        }

        public RaceClock Clock
        {
            get { return _clock; }
        }

        public void Add(PracticeClass practice)
        {
            _schedule.Add(practice);
        }

        public List<PracticeClass> Fetch()
        {
            return _schedule;
        }
        
        public void Run()
        {
            _currentPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");

            _currentPracticeClass.Status = "Running";

            Clock.SetRaceTime(_currentPracticeClass.Time);

            Clock.Start();

            Clock.OnElapsedHasExpired += Clock_OnElapsedHasExpired;

        }

        private void Clock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            Clock.Stop();

            _currentPracticeClass.Status = "Finished";

            Run();

        }


    }
}
