using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Practice;

namespace RaceTime.GUI
{
    /// <summary>
    /// Interaction logic for PracticeMain.xaml
    /// </summary>
    public partial class PracticeMain : Window
    {
        public PracticeMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var model = new ScheduleModelView();

            model.Model.Add(new PracticeClass("Super Stock Touring", 1000 * 10));

            model.Model.Add(new PracticeClass("Modified Touring", 1000 * 10));

            model.Model.Add(new PracticeClass("Pro10 and Pro12", 1000 * 10));

            model.Model.Add(new PracticeClass("Formula One", 1000 * 10));

            model.Model.Add(new PracticeClass("Mini's", 1000 * 10));

            model.Model.Add(new PracticeClass("Under 12's", 1000 * 10));

            model.Model.AddAnnoucement(new Announcement(ScheduleEventType.Started,"Practice Started"));

            model.Model.AddAnnoucement(new Announcement(ScheduleEventType.Finished, "Practice Finshed"));
            
            ListPractice.DataContext = model.Model;

            StartButton.DataContext = model;
            StopButton.DataContext = model;
            ElapesedTime.DataContext = model;
            Announcement.DataContext = model;
            NumberOfRounds.DataContext = model;
            CurrentRound.DataContext = model;
            CurrentClass.DataContext = model;
            CurrentClassName.DataContext = model;
        }
    }
}
