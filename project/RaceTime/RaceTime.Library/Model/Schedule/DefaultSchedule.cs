using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
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

        public event AnnouncementHandler OnAnnouncement;
        
        private Timer _scoreboardNotificationTimer;

        private IScoreboard _scoreboard = null;

        private List<PracticeClass> _schedule = new List<PracticeClass>();

        private List<Announcement> _announcements = new List<Announcement>();

        private RaceClock _clock = new RaceClock();

        private PracticeClass _currentPracticeClass;

        private PracticeClass _nextPracticeClass;

        private bool _isScheduleRunning;
        
        private int _currentRound=1;

        public int CurrentRound
        {
            get { return _currentRound; }
            private set { _currentRound = value; }
        }

        private int _numberOfRounds;

        public int NumberOfRound
        {
            get { return _numberOfRounds; }
            set { _numberOfRounds = value; }
        }

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

        public List<Announcement> Announcements
        {
            get { return _announcements; }
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

        public bool IsScheduleRunning
        {
            get { return _isScheduleRunning; }
            private set { _isScheduleRunning = value; }
        }

        public void Add(PracticeClass practice)
        {
            practice.HeatNumber = _schedule.Count() +1 ;
            _schedule.Add(practice);
        }

        public List<PracticeClass> Fetch()
        {
            return _schedule;
        }


        protected virtual void OnAnnoucementEvent(Announcement announcement )
        {
            AnnouncementHandler handler = OnAnnouncement;

            Debug.WriteLine(announcement.Text);

            if (handler != null) handler(this, announcement);
        }


        public void Run()
        {

            if (ScoreboardNotificationTimer == null && _scoreboard != null)
            {
                ScoreboardNotificationTimer = new Timer(_scoreboard.Interval);
                ScoreboardNotificationTimer.Elapsed += ScoreboardNotificationTimer_Elapsed;
                ScoreboardNotificationTimer.Enabled = true;
            }

            if (HaveFinishRound())
            {
                IsScheduleRunning = false;

                return;
            }

            CurrentPracticeClass.Status = "Running";

            _nextPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");

            IsScheduleRunning = true;
            
            var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Started);
            
            OnAnnoucementEvent(announcement);

            Clock.SetRaceTime(CurrentPracticeClass.Time);

            Clock.Start();

            Clock.OnElapsedHasExpired += Clock_OnElapsedHasExpired;
        
    }

        private void ScoreboardNotificationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateScoreboard();
        }

        private bool HaveFinishRound()
        {
            IsScheduleRunning = false;

            if (NumberOfRound == CurrentRound &&  Schedule.FirstOrDefault(i => i.Status == "Ready")== null)
            {
                return true;
            }
            
             _currentPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");

            if (_currentPracticeClass == null)
            {
                CurrentRound++;
                foreach (var practiceClass in Schedule)
                {
                    practiceClass.Status = "Ready";
                }

                _currentPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");

            }

            return false;

        }


        private void UpdateScoreboard()
        {
            if(_scoreboard !=null)
                _scoreboard.WriteOutput(CurrentRound + " - > " + CurrentPracticeClass.HeatNumber + " - >" + CurrentPracticeClass.Name + " - >" + Clock.ElapsedTimeString);
        }

        private void Clock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            Clock.Stop();
           
            CurrentPracticeClass.Status = "Finished";

            var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Finished);

            OnAnnoucementEvent(announcement);

            Run();

        }

        public void Stop()
        {
            Clock.Stop();

            var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Stopped);

            OnAnnoucementEvent(announcement);
            
            IsScheduleRunning = false;

            CurrentPracticeClass.Status = "Stopped";

            ScoreboardNotificationTimer = null;
        }
    }

    public delegate void AnnouncementHandler(object sender, Announcement announcement);
}
