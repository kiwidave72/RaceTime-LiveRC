using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RaceTime.Library.Controller;
using RaceTime.Library.Scoreboard;


namespace RaceTime.Library.Model.Schedule
{
     [Serializable()]
    public class DefaultSchedule
    {

        public event AnnouncementHandler OnAnnouncement;

        [NonSerialized()]
        private Timer _scoreboardNotificationTimer;

        [NonSerialized()]
        private IScoreboard _scoreboard = null;

        private List<PracticeClass> _schedule = new List<PracticeClass>();

        private List<Announcement> _announcements = new List<Announcement>();

        public long Interval {
            get { return _interval; }
            set
            {
                _interval = value ;
                CalculateScheduleTimes();
            } 
        }
         
        [NonSerialized()]
        private RaceClock _raceClock = new RaceClock();

        [NonSerialized()]
        private RaceClock _intervalClock = new RaceClock();

        [NonSerialized()]
        private RaceClock _nextPracticeAnnoucementClock = new RaceClock();

         [NonSerialized()]
         private RaceClock _repeatableAnnoncementClock  = new RaceClock();

         [NonSerialized()]
        private PracticeClass _currentPracticeClass;

         [NonSerialized()]
        private PracticeClass _nextPracticeClass;

         [NonSerialized()]
        private bool _isScheduleRunning;

         [NonSerialized()]
        private int _currentRound = 1;

         [NonSerialized()]
        private int _numberOfRoundses;

         private string[] _serialPortNames;
         private string _serialPortName;
         private long _interval;
         private int _currentHeat;

         public int CurrentRound
        {
            get { return _currentRound; }
            set { _currentRound = value; }
        }

        public int NumberOfRounds
        {
            get { return _numberOfRoundses; }
            set { _numberOfRoundses = value; }
        }

        private Timer ScoreboardNotificationTimer
        {
            get { return _scoreboardNotificationTimer; }
            set { _scoreboardNotificationTimer = value; }
        }
         
        public List<PracticeClass> Schedule
        {
            get { return _schedule; }
        }

        public List<Announcement> Announcements
        {
            get { return _announcements; }
        }

        public RaceClock RaceClock
        {
            get { return _raceClock; }
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
            set { _isScheduleRunning = value; }
        }

        public RaceClock IntervalClock
        {
            get { return _intervalClock; }
            set { _intervalClock = value; }
        }

        public RaceClock RepeatableAnnoncementClock
        {
            get { return _repeatableAnnoncementClock; }
            set { _repeatableAnnoncementClock = value; }
        }


        public RaceClock NextPracticeAnnoucementClock
        {
            get { return _nextPracticeAnnoucementClock; }
            set { _nextPracticeAnnoucementClock = value; }
        }

         public string[] SerialPortNames
         {
             get { return _serialPortNames; } 
             set { _serialPortNames = value; }
         }

         public string SerialPortName
         {
             get { return _serialPortName; }
             set { _serialPortName = value; }
         }

         public int CurrentHeat {
             get { return _currentHeat; }
             set { _currentHeat = value; } 
         }


         public DefaultSchedule()
        {
            _scoreboard = new SerialScoreboard();
            SerialPortNames = _scoreboard.SerialPortNames;
           // _scoreboard.PortName = SerialPortName;// "COM2";// SerialPortNames[0];

        }


         public DefaultSchedule(IScoreboard scoreboard)
         {
             _scoreboard = scoreboard;
         }

        public void Add(PracticeClass practice)
        {
            practice.HeatNumber = _schedule.Count() +1 ;
            _schedule.Add(practice);
        }

        public void Change(int heatNumber, string name)
        {
            var practiceClass = Schedule.SingleOrDefault(i => i.HeatNumber == heatNumber);
            if (practiceClass != null)
            {
                practiceClass.Name = name;
            }
            else
            {
                throw new Exception(string.Format("Unable to find heat {0}",heatNumber));
            }
        }

        public List<PracticeClass> Fetch()
        {
            return _schedule;
        }

        protected virtual void OnAnnoucementEvent(Announcement announcement,PracticeClass currentPracticeClass )
        {
            if (announcement == null)
                return;

            AnnouncementHandler handler = OnAnnouncement;

            Debug.WriteLine(announcement.Text);
            if (handler != null) handler(this, announcement,currentPracticeClass);
        }


        public void Run()
        {
            CalculateScheduleTimes();

            if (Interval > 0)
            {
                StartInterval();
            }
            else
            {
                StartSchedule();
            }
            
        }

         private void CalculateScheduleTimes()
         {
             DateTime? startTime =null ;
             PracticeClass lastClass = null;

             foreach (var practiceClass in Schedule)
             {

                 DateTime? scheduledTime;


                 if (startTime == null)
                 {
                     startTime = DateTime.Now;
                     
                     scheduledTime = startTime.Value.AddSeconds(Interval / 1000);
                 }
                 else
                 {
                     startTime = lastClass.ScheduledTime;
                     scheduledTime = startTime.Value.AddSeconds((lastClass.Time * 60) + (Interval / 1000));
                 }
                 
                 practiceClass.ScheduledTime = scheduledTime.Value;
                 lastClass = practiceClass;
             }


         }

         private void StartInterval()
        {
            if (Interval > 0)
            {
                if (HaveFinishRound())
                {
                    return;
                }

                ClearScoreboard();

                IntervalClock.SetRaceTime(Interval);

                IntervalClock.OnElapsedHasExpired += IntervalClock_OnElapsedHasExpired;

                IntervalClock.Start();

                var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.IntervalStarted);

                OnAnnoucementEvent(announcement,null);
            }
        }

         void IntervalClock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            IntervalClock.Stop();

            StartSchedule();

        }

         public void Initialization()
         {
             CalculateScheduleTimes();

             SetCurrentPracticeClassFromSchedule();

             SetNextPracticeClass();


             RaceClock.SetRaceTime(CurrentPracticeClass.Time * 60 * 1000);

             SetRepeatableAnnoucementClock();

             ClearScoreboard();
         }

         private void ClearScoreboard()
         {
             _scoreboard.ClearDisplay();
         }

         private void SetRepeatableAnnoucementClock()
         {
             var repeatableAnnouncement = Announcements.First(i => i.EventType == ScheduleEventType.Repeatable);

             RepeatableAnnoncementClock.SetRaceTime(repeatableAnnouncement.Time*60*1000);
         }

         private void StartSchedule()
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


             SetRepeatableAnnoucementClock();

            RepeatableAnnoncementClock.OnElapsedHasExpired += RepeatableAnnoncementClock_OnElapsedHasExpired;

            RepeatableAnnoncementClock.Start();

            SetCurrentPracticeClassFromSchedule();
             
            CurrentPracticeClass.Status = "Running";

            SetNextPracticeClass();

            IsScheduleRunning = true;

            var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Started);

            OnAnnoucementEvent(announcement,CurrentPracticeClass);

            RaceClock.SetRaceTime(CurrentPracticeClass.Time*60*1000);

            RaceClock.Start();

            RaceClock.OnElapsedHasExpired += Clock_OnElapsedHasExpired;

            SetUpTimedAnnoucement();
        }

         void RepeatableAnnoncementClock_OnElapsedHasExpired(object sender, EventArgs e)
         {
             var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Repeatable);

             OnAnnoucementEvent(announcement, CurrentPracticeClass);
             
             RepeatableAnnoncementClock.Start();
         }

         private void SetNextPracticeClass()
         {
             _nextPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready" && i.HeatNumber > CurrentHeat);
         }

         private void SetUpTimedAnnoucement()
        {

            var annoucment = Announcements.Single(i => i.EventType == ScheduleEventType.Next);
            
            NextPracticeAnnoucementClock = new RaceClock();

            NextPracticeAnnoucementClock.SetRaceTime(CurrentPracticeClass.Time * 60 * 1000 - (annoucment.Time * 60 * 1000));

            NextPracticeAnnoucementClock.Start();

            NextPracticeAnnoucementClock.OnElapsedHasExpired += _annoucementClock_OnElapsedHasExpired;
        }


         private void ScoreboardNotificationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateScoreboard();
        }

         private bool HaveFinishRound()
        {
            IsScheduleRunning = false;

            if (NumberOfRounds == CurrentRound &&  Schedule.FirstOrDefault(i => i.Status == "Ready")== null)
            {
                CalculateScheduleTimes();
                return true;
            }
            
            return false;

        }

        public void SetCurrentPracticeClassTo(Int32 heatNumber)
        {
            foreach (PracticeClass classes in Schedule)
            {
                classes.Status = "Ready";
            }

            var practiceClass = Schedule.FirstOrDefault(i => i.HeatNumber == heatNumber);

            if (practiceClass == null)
                return;

            foreach (PracticeClass classes in Schedule.Where(i=>i.HeatNumber < heatNumber))
            {
                classes.Status = "Finished";
            }

            SetCurrentPracticeClassFromSchedule();

            SetNextPracticeClass();
        }

         private void SetCurrentPracticeClassFromSchedule()
         {
            
             _currentPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");

             SetCurrentPracticeClass();

         }

         private void SetCurrentPracticeClass()
         {


             if (_currentPracticeClass == null)
             {
                 CurrentRound++;
                 foreach (var practiceClass in Schedule)
                 {
                     practiceClass.Status = "Ready";
                 }

                 _currentPracticeClass = Schedule.FirstOrDefault(i => i.Status == "Ready");
             }

            

             CurrentHeat = _currentPracticeClass.HeatNumber;

         }


         private void UpdateScoreboard()
        {
            if(_scoreboard !=null && RaceClock.HasStarted)
                _scoreboard.WriteRaceInfor(CurrentRound, CurrentPracticeClass.HeatNumber, RaceClock.ElapsedTimeMinutesSecondsString, CurrentPracticeClass.Name);
        }

         private void Clock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            RaceClock.Stop();
           
            RepeatableAnnoncementClock.Stop();

            CurrentPracticeClass.Status = "Finished";

            var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Finished);

            OnAnnoucementEvent(announcement,CurrentPracticeClass);

            Run();

        }

         private void _annoucementClock_OnElapsedHasExpired(object sender, EventArgs e)
        {
            NextPracticeAnnoucementClock.Stop();

            var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Next);

            // check for end of round ...ect
            
            if (NextPracticeClass != null)
            {
                OnAnnoucementEvent(announcement, NextPracticeClass);
            }

        }


         public void Stop()
        {
            RaceClock.Stop();

            var announcement = Announcements.FirstOrDefault(i => i.EventType == ScheduleEventType.Stopped);

            OnAnnoucementEvent(announcement,CurrentPracticeClass);
            
            IsScheduleRunning = false;

            CurrentPracticeClass.Status = "Stopped";

            ScoreboardNotificationTimer = null;
        }

         public void AddAnnoucement(Announcement announcement)
        {
            var matchingAccoucement = Announcements.SingleOrDefault(i => i.EventType == announcement.EventType);

            if (matchingAccoucement != null)
            {
                Announcements.Remove(matchingAccoucement);
            }

            Announcements.Add(announcement);
        }

         public string GetScoreboardText()
         {
             return _scoreboard.SerialOutputText;

         }

         public IEnumerable<Exception> GetScoreboardErrors()
         {
             return _scoreboard.Errors;
         }

         public void SetScoreboardPort(string serialPortName)
         {
             _scoreboard.PortName = serialPortName;
         }
    }

    

    public delegate void AnnouncementHandler(object sender, Announcement announcement,PracticeClass currentPracticeClass);
}
