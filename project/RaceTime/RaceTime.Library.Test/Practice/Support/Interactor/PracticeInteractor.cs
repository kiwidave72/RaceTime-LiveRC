using System.Collections.Generic;
using System.Linq;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Model.Schedule;
using RaceTime.Library.Test.Meeting;

namespace RaceTime.Library.Test.Practice.Support.Interactor
{
    public class PracticeInteractor
    {
        private  Model.Meeting.Meeting _meeting = new Model.Meeting.Meeting(new DefaultSchedule(null));

        public IScoreboard Scoreboard
        {
            get { return _scoreboard; }
            set { _scoreboard = value; }
        }

        public DefaultSchedule Schedule { get; private set; }


        public void SetTitle(string title)
        {
            _meeting.Title = title;
        }

        private IScoreboard _scoreboard;


        public void SetNumberOfRounds(int number)
        {
            _meeting.Schedule.NumberOfRounds = number;
        }

        public int GetNumberOfRounds()
        {
            return _meeting.Schedule.NumberOfRounds ;
        }
        
        public void AddToSchedule(PracticeClass practice)
        {
            _meeting.Schedule.Add(practice);
        }

        public void ChangeHeatName(int heatNumber, string name)
        {
            _meeting.Schedule.Change(heatNumber, name);
        }
        
        public List<PracticeClass> All()
        {
            return _meeting.Schedule.Fetch();
        }

        public int CurrentRound()
        {
            return _meeting.Schedule.CurrentRound;
        }

        public PracticeClass CurrentPractice()
        {
            return _meeting.Schedule.CurrentPracticeClass;
        }

        public bool IsScheduleRunning()
        {
            return _meeting.Schedule.IsScheduleRunning;
        }
        
        public PracticeClass NextPractice()
        {
            return _meeting.Schedule.NextPracticeClass;
        }


        public void RunSchedule( IScoreboard scoreboard,long interval)
        {
            _meeting = new Model.Meeting.Meeting(new DefaultSchedule(scoreboard));
            _meeting.Schedule.Interval = interval;
            
            _meeting.Schedule.Run();
        }


        public void StopSchedule()
        {
            _meeting.Schedule.Stop();
        }

        public void AddAnnouncmentToSchedule(Model.Announcement announcement)
        {
            _meeting.Schedule.AddAnnoucement(announcement);
        }
    }
}
