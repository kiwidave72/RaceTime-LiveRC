using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using RaceTime.Library.Annotations;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Practice;
using RaceTime.Library.Model.Schedule;

namespace RaceTime.GUI
{
    public class ScheduleModelView : INotifyPropertyChanged
    {

        private DefaultSchedule _model = new DefaultSchedule();

        private SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        private Timer _timer;
        private string _elapsedTime;

        private DelegateCommand _stopScheduleCommand;
        private DelegateCommand _startScheduleCommand;
        private string _annoucementText;
        private int _currentRound;
        private int _numberOfRounds;
        private PracticeClass _currentClass;


        public ScheduleModelView()
        {
            _startScheduleCommand = new DelegateCommand(StartSchedule);
            _stopScheduleCommand = new DelegateCommand(StopSchedule);

            _model.OnAnnouncement += OnAnnouncement;
            _speechSynthesizer.SpeakCompleted += _speechSynthesizer_SpeakCompleted;

        }

        public DefaultSchedule Model
        {
            get { return _model; }

        }


        public string ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged("ElapsedTime");
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
            _timer = new Timer(UpdateTimer);
            _timer.Change(100, 100);
            _model.NumberOfRound = 1;
            _model.Interval = 10000;
            _model.Run();
        }

        private void StopSchedule()
        {
            _timer.Dispose();

            _model.Stop();
        }


        private void UpdateTimer(object state)
        {
            ElapsedTime = Model.RaceClock.ElapsedTimeString;
            CurrentRound = Model.CurrentRound;
            NumberOfRounds = Model.NumberOfRound;
            CurrentClass = Model.CurrentPracticeClass;
        }

        private void OnAnnouncement(object sender, Announcement e)
        {
            AnnouncmentText = e.Text;
            
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
