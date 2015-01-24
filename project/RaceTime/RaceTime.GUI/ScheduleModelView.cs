using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using RaceTime.Library.Annotations;
using RaceTime.Library.Controller.Scoreboard;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Configuration;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Model.Schedule;
using RaceTime.Library.Scoreboard;
using RaceTime.Library.Storage;

namespace RaceTime.GUI
{
    public class ScheduleModelView : INotifyPropertyChanged
    {

        private DefaultSchedule _model ;

        private IScoreboard _scoreboard;

        private SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        private Timer _timer;
        private string _elapsedTimeString;

        private DelegateCommand _stopScheduleCommand;
        private DelegateCommand _startScheduleCommand;
        private string _annoucementText;
        private int _currentRound;
        private int _numberOfRounds;
        private PracticeClass _currentClass;
        private string _scoreboardOutput;
        private string _intervalElapsedTime;
        private ObservableCollection<PracticeClass> _schedule;
        private ObservableCollection<Exception> _scoreboardErrors;
        private long _elapsedTime;
        private long _practiceTime;
        private string _raceTimeRemainingTimeString;
        private PracticeClass _nextClass;


        public ScheduleModelView()
        {
            _scoreboard = new SerialScoreboard();

            ConfigurationStorage store = new ConfigurationStorage();

            store.Load();

            _model = store.Configuration.Schedule;
            
            //_model = new DefaultSchedule(_scoreboard);
            
            _startScheduleCommand = new DelegateCommand(StartSchedule);
            _stopScheduleCommand = new DelegateCommand(StopSchedule);

            _speechSynthesizer.SpeakCompleted += _speechSynthesizer_SpeakCompleted;

            _timer = new Timer(UpdateTimer);
            _timer.Change(100, 100);


            //Configuration config = new Configuration();

            //config.Name = "This is a test Configuration";

            //config.Schedule = Model;

           


            //Model.NumberOfRound = 2;
            //Model.Interval = 10000;

            //Model.Add(new PracticeClass("Super Stock Touring", 1000 * 20));

            //Model.Add(new PracticeClass("Modified Touring", 1000 * 20));

            //Model.Add(new PracticeClass("Pro10 and Pro12", 1000 * 20));

            //Model.Add(new PracticeClass("Formula One", 1000 * 20));

            //Model.Add(new PracticeClass("Mini's", 1000 * 20));

            //Model.Add(new PracticeClass("Under Twelves", 1000 * 20));

            //Model.AddAnnoucement(new Announcement(ScheduleEventType.Started, "Practice Started for [PracticeClass.Name]"));

            //Model.AddAnnoucement(new Announcement(ScheduleEventType.Finished, "Practice Finshed for [PracticeClass.Name]"));

            //Model.AddAnnoucement(new Announcement(ScheduleEventType.Next, "Next up [PracticeClass.Name]",1000*10));

            Model.OnAnnouncement += OnAnnouncement;


            Model.Initialization();

            UpdateGUI();

        }

        public void Save()
        {
            ConfigurationStorage store = new ConfigurationStorage();

            var config = new Configuration();

            config.Schedule = Model;
            store.Save(config);
        }

        public DefaultSchedule Model
        {
            get { return _model; }

        }

        public string IntervalElapsedTime
        {
            get { return _intervalElapsedTime; }
            set
            {
                _intervalElapsedTime = value;
                OnPropertyChanged("IntervalElapsedTime");
            }
        }

        public string ElapsedTimeString
        {
            get { return _elapsedTimeString; }
            set
            {
                _elapsedTimeString = value;
                OnPropertyChanged("ElapsedTimeString");
            }
        }

        public String AnnouncmentText
        {
            get { return _annoucementText; }
            set
            {
                _annoucementText = value;
                OnPropertyChanged("AnnouncmentText");

            }
        }

        public int CurrentRound
        {
            get { return  _currentRound; }
            set
            {
                _currentRound = value;
                OnPropertyChanged("CurrentRound");

            }
        }

        public int NumberOfRounds
        {
            get { return _numberOfRounds; }
            set
            {
                _numberOfRounds = value;
                OnPropertyChanged("NumberOfRounds");

            }
        }

        public PracticeClass CurrentClass
        {
            get { return _currentClass; }
            set
            {
                _currentClass = value;
                OnPropertyChanged("CurrentClass");

            }
        }

        public ObservableCollection<Exception> ScoreboardErrors
        {
            get { return _scoreboardErrors; }
            set
            {
                _scoreboardErrors = value;
                OnPropertyChanged("ScoreboardErrors");

            }
        }

        public ObservableCollection<PracticeClass> Schedule
        {
            get { return _schedule; }
            set
            {
                _schedule = value;
                OnPropertyChanged("Schedule");

            }
        }

        public string ScoreboardOutput
        {
            get { return _scoreboardOutput; }
            set
            {
                _scoreboardOutput = value;
                OnPropertyChanged("ScoreboardOutput");

            }
        }


        public ICommand StartScheduleCommand
        {
            get { return _startScheduleCommand; }
        }

        public ICommand StopScheduleCommand
        {
            get { return _stopScheduleCommand; }
        }

        

        private void StartSchedule()
        {
          
            _model.Run();
        }

        private void StopSchedule()
        {
            _timer.Dispose();

            _model.Stop();
        }


        private void UpdateTimer(object state)
        {
            UpdateGUI();
        }

        private void UpdateGUI()
        {

            ElapsedTimeString = Model.RaceClock.ElapsedTimeMinutesSecondsMillisecondsString;

            PracticeTime = Model.RaceClock.RaceTime  ;

            ElapsedTime = Model.RaceClock.Elapsed() /1000 /10;

            RaceTimeRemainingTimeString = Model.RaceClock.RemainingTimeMinutesSecondsString;

            IntervalElapsedTime = Model.IntervalClock.RemainingTimeMinutesSecondsString;
                
            ScoreboardOutput = Model.GetScoreboardText();

            CurrentRound = Model.CurrentRound;

            NumberOfRounds = Model.NumberOfRound;

            CurrentClass = Model.CurrentPracticeClass;

            NextClass = Model.NextPracticeClass;


            Schedule = new ObservableCollection<PracticeClass>(Model.Schedule);

            ScoreboardErrors = new ObservableCollection<Exception>(Model.GetScoreboardErrors());
        }

        public PracticeClass NextClass
        {
            get { return _nextClass; }
            set
            {
                _nextClass = value;
                OnPropertyChanged("NextClass");

            }
        }

        public string RaceTimeRemainingTimeString
        {
            get { return _raceTimeRemainingTimeString; }
            set
            {
                _raceTimeRemainingTimeString  = value;
                OnPropertyChanged("RaceTimeRemainingTimeString");
            }
        }

        public long ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged("ElapsedTime");
            }
        }

        public long PracticeTime
        {
            get { return _practiceTime; }
            set
            {
                _practiceTime = value;
                OnPropertyChanged("PracticeTime");
            }
        }

        private void OnAnnouncement(object sender, Announcement e,PracticeClass CurrentClass)
        {
            var text = e.Text.Replace("[PracticeClass.Name]", CurrentClass.Name);

            text = text.Replace("[PracticeClass.HeatNumber]", CurrentClass.HeatNumber.ToString());

            AnnouncmentText = text.Replace("[Schedule.Remaining]", (Model.RaceClock.RemainingMinutes()).ToString());
            
            _speechSynthesizer.SpeakAsync(AnnouncmentText);
        }

        void _speechSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            AnnouncmentText = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}
