using System.Runtime.InteropServices;
using System.Threading;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RaceTime.Library.Controller;


namespace RaceTime.Library.Model.Schedule
{
    public class DefaultSchedule
    {

        private Timer _scoreboardNotificationTimer;
        
        private IScoreboard _scoreboard =null;

        private List<PracticeClass> _schedule = new List<PracticeClass>();

        private RaceClock _clock = new RaceClock();

        private PracticeClass _currentPracticeClass;
        
        private PracticeClass _nextPracticeClass;

        private Timer ScoreboardNotificationTimer
        {
            get { return _scoreboardNotificationTimer; }
            set { _scoreboardNotificationTimer = value; }
        }

        public IScoreboard Scoreboard
        {
            get { return _scoreboard; }
            set { _scoreboard = value; }
        }

        public List<PracticeClass> Schedule
        {
            get { return _schedule; }
        }

        public RaceClock Clock
        {
            get { return _clock; }
        }

        public PracticeClass CurrentPracticeClass
        {
            get { return _currentPracticeClass; }
        }

        public PracticeClass NextPracticeClass
        {
            get { return _nextPracticeClass; }
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
            ScoreboardNotificationTimer = new Timer(UpdateScoreboard, null, (uint)_scoreboard.Interval, _scoreboard.Interval);

            _currentPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");

            if (_currentPracticeClass == null)
            {
                return;
            }

            CurrentPracticeClass.Status = "Running";

            _nextPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");


            Clock.SetRaceTime(CurrentPracticeClass.Time);

            Clock.Start();

            Clock.OnElapsedHasExpired += Clock_OnElapsedHasExpired;

        }

        private void UpdateScoreboard(object state)
        {
            if(_scoreboard !=null)
                _scoreboard.WriteOutput( CurrentPracticeClass.Name +" - >"+ Clock.ElapsedTimeString);
        }

        private void Clock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            Clock.Stop();
            
            CurrentPracticeClass.Status = "Finished";

            Run();

        }


    }
}
