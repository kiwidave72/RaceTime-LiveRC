using System;
using System.ComponentModel;
using System.Threading;
using RaceTime.Library.Annotations;

namespace RaceTime.Library.Controller
{
    public class RaceClock 
    {
        private DateTime _raceclock;
        private DateTime _raceclockStopped;
        private long _raceTime;
        private Timer _notificationTimer;

        public event EventHandler OnStarted;
        public event EventHandler OnElapsedHasExpired;
        public event EventHandler OnStopped;
        public event EventHandler OnReset;

        protected virtual void OnResetEvent()
        {
            EventHandler handler = OnReset;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnStoppedEvent()
        {
            EventHandler handler = OnStopped;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public bool HasStarted { get; set; }

        public bool HasElapsed { get; set; }


        protected virtual void OnRaceElapsedEvent()
        {
            EventHandler handler = OnElapsedHasExpired;
            
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnStartedEvent()
        {
            EventHandler handler = OnStarted;
            
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public long RaceTime
        {
            get { return _raceTime; }
        }

        private Timer NotificationTimer
        {
            get { return _notificationTimer; }
            set { _notificationTimer = value; }
        }

        public long Elapsed()
        {
            long elapsed = 0;
            
            if (HasStarted)
            {
                elapsed = DateTime.Now.Ticks - _raceclock.Ticks;
            }
            else
            {
                elapsed = _raceclockStopped.Ticks - _raceclock.Ticks;
            }
            
            return elapsed;
        }
        
        public string ElapsedTimeMinutesSecondsMillisecondsString
        {

            get
            {
                var result = new TimeSpan(Elapsed());
                return string.Format("{0}:{1}.{2}", result.Minutes, result.Seconds,
                    result.Milliseconds);
            }
        }

        public string ElapsedTimeMinutesSecondsString
        {

            get
            {
                var result = new TimeSpan(Elapsed());
                return string.Format("{0}:{1}", result.Minutes, result.Seconds);
            }
        }

        public long Remaining()
        {
            long remaining = 0;

            if (HasStarted)
            {
                remaining = (RaceTime * 1000 * 10)- (Elapsed() );
            }
            else
            {
                remaining = 0;
            }

            return remaining ;
        }

        public long RemainingMinutes()
        {
            var result = new TimeSpan(Remaining());

            long minutes = result.Minutes;
            long seconds = result.Seconds;

            if (seconds > 50)
            {
                minutes++;
            }

            return minutes;
        }

        public string RemainingTimeMinutesSecondsString
        {

            get
            {
                var result = new TimeSpan(Remaining());
                return string.Format("{0}:{1}", result.Minutes, result.Seconds);
            }
        }

        public string RemainingTimeMinutesSecondsMillisecondsString
        {

            get
            {
                var result = new TimeSpan(Remaining());
                return string.Format("{0}:{1}.{3}", result.Minutes, result.Seconds,
                    result.Milliseconds);
            }
        }


        public string RaceTimeString
        {
            get
            {
                var result = new TimeSpan(RaceTime*10000);
                return string.Format("{0}:{1}.{2}", result.Minutes, result.Seconds,
                    result.Milliseconds);
            }
        }

        public void Start()
        {
            if (RaceTime == 0)
            {
                throw  new Exception("Unable to start race with out Elapsed time set");
            }
            HasStarted = true;

            HasElapsed = false;

            OnStartedEvent();

            NotificationTimer = new Timer(RaceElapsed, null, (uint) _raceTime,0);
            
            _raceclock = DateTime.Now;

        }

        private void RaceElapsed(object state)
        {
            HasElapsed = true;
            
            OnRaceElapsedEvent();
        }

        public void Stop()
        {
           // if (HasStarted==false && HasElapsed==false)
           // {
           //     throw new Exception("RaceClock cannot be stopped.");    
           // }
            OnElapsedHasExpired = null;

            OnStoppedEvent();

            HasStarted = false;
            
            _raceclockStopped = DateTime.Now;
        }

        public void Reset()
        {
            if (HasStarted)
            {
                throw new Exception("RaceClock cannot be reset while it is running.");
            }
            OnResetEvent();

            HasElapsed = false;
            
            _raceclock = DateTime.MinValue;
            
            _raceclockStopped = DateTime.MinValue;
        }

        public void SetRaceTime(long fiveminutes)
        {
            _raceTime = fiveminutes;
        }

        public void StoppedAndFinished()
        {
            Stop();
            
            Reset();
        }
     }
}