using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
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

       // private IScoreboard _scoreboard;

        private SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        private Timer _timer;
        private string _elapsedTimeString;

        private DelegateCommand _saveCommand;
        private DelegateCommand _openCommand;
        
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
        private string[] _serialPortNames;
        private string _serialPortName;
        private long _interval;
        private int _currentHeat;
        private ObservableCollection<string> _announcementCollection;
        private bool _hasRaceStarted ;
        private bool _hasLoadedConfiguration=false;

        public ScheduleModelView()
        {

            ScoreboardOutputList = new ObservableCollection<string>();

            AnnoucementObservableCollection= new ObservableCollection<string>();
    

            _startScheduleCommand = new DelegateCommand(StartSchedule);

            _stopScheduleCommand = new DelegateCommand(StopSchedule);
            
            _saveCommand = new DelegateCommand(Save);
            
            _openCommand = new DelegateCommand(Open);
            
            _speechSynthesizer.SpeakCompleted += _speechSynthesizer_SpeakCompleted;

            SerialPortNames = SerialPort.GetPortNames();
        }

        private void Open()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.FileName = "Configuration.xml"; 
            
            openFileDialog.DefaultExt = ".xml"; 
            
            openFileDialog.Filter = "Configuration documents (.xml)|*.xml"; 

            Nullable<bool> result = openFileDialog.ShowDialog();

            
            if (result == true)
            {
                ConfigurationStorage store = new ConfigurationStorage();

                store.Load(openFileDialog.FileName);

                _model = store.Configuration.Schedule;

                Model.Initialization();

                Model.OnAnnouncement += OnAnnouncement;

                UpdateGUI();

                StartUpdateTimer();

                _hasLoadedConfiguration = true;
            }


        }

        private void StartUpdateTimer()
        {
            _timer = new Timer(UpdateTimer);

            _timer.Change(100, 100);
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

        public long Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                
                OnPropertyChanged("Interval");
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

        public ObservableCollection<string> ScoreboardOutputList { get; set; }
     
        public ObservableCollection<string> AnnoucementObservableCollection 
        {
            get { return _announcementCollection; }
            set
            {
                _announcementCollection = value;
                OnPropertyChanged("AnnoucementObservableCollection");

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

        public string[] SerialPortNames
        {
            get { return _serialPortNames; }
            set
            {
                _serialPortNames = value;
                OnPropertyChanged("SerialPortNames");

            }
        }

        public string SerialPortName
        {
            get { return _serialPortName; }
            set
            {
                _serialPortName = value;
                OnPropertyChanged("SerialPortName");
               Model.SetScoreboardPort(_serialPortName);

            }
        }

        public bool HasRaceStarted
        {
            get { return _hasRaceStarted; }
            set
            {
                _hasRaceStarted = value;
                OnPropertyChanged("HasRaceStarted");
             }
        }

        public bool HasLoadedConfiguration
        {
            get { return _hasLoadedConfiguration; }
            set
            {
                _hasLoadedConfiguration = value;
                OnPropertyChanged("HasLoadedConfiguration");
            }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
        }

        public ICommand OpenCommand
        {
            get { return _openCommand; }
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
            StartUpdateTimer();
          
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

            Interval = Model.Interval;

            CurrentHeat = Model.CurrentHeat;
            
            ElapsedTimeString = Model.RaceClock.ElapsedTimeMinutesSecondsMillisecondsString;

            PracticeTime = Model.RaceClock.RaceTime  ;

            ElapsedTime = Model.RaceClock.Elapsed() /1000 /10;

            RaceTimeRemainingTimeString = Model.RaceClock.RemainingTimeMinutesSecondsString;

            IntervalElapsedTime = Model.IntervalClock.RemainingTimeMinutesSecondsString;
                
            ScoreboardOutput = Model.GetScoreboardText();
           
            CurrentRound = Model.CurrentRound;

            NumberOfRounds = Model.NumberOfRounds;

            CurrentClass = Model.CurrentPracticeClass;

            NextClass = Model.NextPracticeClass;
            
            Schedule = new ObservableCollection<PracticeClass>(Model.Schedule);

            ScoreboardErrors = new ObservableCollection<Exception>(Model.GetScoreboardErrors());

            HasRaceStarted = Model.RaceClock.HasStarted;

           

        }

        public int CurrentHeat
        {
            get { return _currentHeat; }
            set
            {
                _currentHeat = value;
                OnPropertyChanged("CurrentHeat");

            }
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

            if (e.EventType == ScheduleEventType.Finished || e.EventType == ScheduleEventType.Stopped)
            {
                
                var player = new SoundPlayer(@"c:\buzzer_x.wav");
                
                player.PlaySync();

                _speechSynthesizer.SpeakAsync(AnnouncmentText);


            }
            else
            {
                _speechSynthesizer.SpeakAsync(AnnouncmentText);
            }

            Application.Current.Dispatcher.BeginInvoke(
                  DispatcherPriority.Background,
                    new Action(() => AnnoucementObservableCollection.Add(DateTime.Now.ToShortTimeString() + " "+Model.RaceClock.ElapsedTimeMinutesSecondsString  +" -> '"+  AnnouncmentText+"'")));

            
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
