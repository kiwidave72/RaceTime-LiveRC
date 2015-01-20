using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using RaceTime.Library.Annotations;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Schedule;

namespace RaceTime.GUI
{
    public class ScheduleModelView : INotifyPropertyChanged
    {

        private DefaultSchedule _model = new DefaultSchedule();


        private Timer _timer;
        private string _elapsedTime;

        private DelegateCommand _stopScheduleCommand;
        private DelegateCommand _startScheduleCommand;
        private string _annoucementText;


        public ScheduleModelView()
        {
            _startScheduleCommand = new DelegateCommand(StartSchedule);
            _stopScheduleCommand = new DelegateCommand(StopSchedule);

            _model.OnAnnouncement += OnAnnouncement;

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

            _model.Run();
        }

        private void StopSchedule()
        {
            _timer.Dispose();

            _model.Stop();
        }


        private void UpdateTimer(object state)
        {
            this.ElapsedTime = _model.RaceClock.ElapsedTimeString;

        }

        private void OnAnnouncement(object sender, Announcement e)
        {
            this.AnnouncmentText = e.Text;
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
