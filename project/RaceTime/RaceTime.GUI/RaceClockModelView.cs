using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using RaceTime.Library.Annotations;
using RaceTime.Library.Controller;
using System.Speech.Synthesis;

namespace RaceTime.GUI
{
    public class RaceClockModelView :  INotifyPropertyChanged
    {
        private SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        private RaceClock _model = new RaceClock();

        private bool _raceStarted = false;
        private bool _raceElapsed = false;

        private DelegateCommand _startRaceCommand;

        private Timer _timer;
        private string _elapsedTime;

        public event PropertyChangedEventHandler PropertyChanged;


        public RaceClockModelView()
        {
            _model.SetRaceTime(1000 * 60 * 1);
            _startRaceCommand = new DelegateCommand(StartRace);
            _model.OnStarted += new EventHandler(_model_OnStarted);
            _model.OnStopped += new EventHandler(_model_OnStopped);
            _model.OnElapsedHasExpired += new EventHandler(_model_OnElapsedHasExpired);
        }

        void _model_OnElapsedHasExpired(object sender, EventArgs e)
        {
            RaceElapsed = true;
            _speechSynthesizer.Speak("Race Finished");
        }

        void _model_OnStopped(object sender, EventArgs e)
        {
            RaceStarted = false;
        }

        void _model_OnStarted(object sender, EventArgs e)
        {
            RaceStarted = true;
            _speechSynthesizer.Speak("Race Started");
        }

        private void StartRace()
        {
            _timer = new Timer(UpdateTimer);
            _timer.Change(100, 100);

            _model.Start();
        }

        private void UpdateTimer(object state)
        {
            this.ElapsedTime = _model.ElapsedTimeString;
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

        public string RaceTime
        {
            get { return Model.RaceTimeString; }
        }

        public RaceClock Model
        {
            get { return _model; }
            
        }

        public ICommand StartRaceCommand
        {
            get { return _startRaceCommand; }
        }

        public bool RaceStarted 
        {
            get { return _raceStarted; }
            set
            {
                if (value != this._raceStarted)
                {
                    this._raceStarted = value;
                    OnPropertyChanged("RaceStarted");
                    OnPropertyChanged("RaceTime");
                }
            }
        }

        public bool RaceElapsed
        {
            get { return _raceElapsed; }
            set
            {
                if (value != _raceElapsed)
                {
                    _raceElapsed = value;
                    OnPropertyChanged("RaceElapsed");
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
