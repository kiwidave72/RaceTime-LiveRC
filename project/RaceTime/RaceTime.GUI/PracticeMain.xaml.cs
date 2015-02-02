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
        private ScheduleModelView model;

        public PracticeMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            model = new ScheduleModelView();
            
            ((Window) sender).DataContext = model;

         
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.SerialPortName = model.SerialPortNames[((ListBox) sender).SelectedIndex];

        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                return;
            }
            model.Model.Interval = Convert.ToInt32(((TextBox)sender).Text);
        }

        private void TextBox_SelectionChanged_NumberOfRounds(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                return;
            }

            model.Model.NumberOfRounds = Convert.ToInt32(((TextBox)sender).Text);
        }

        private void TextBox_SelectionChanged_CurrentRound(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                return;
            }

            model.Model.CurrentRound= Convert.ToInt32(((TextBox)sender).Text);
        }


        private void TextBox_SelectionChanged_CurrentHeat(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                return;
            }

            model.Model.SetCurrentPracticeClassTo(Convert.ToInt32(((TextBox)sender).Text));
            
        }
    }
}
