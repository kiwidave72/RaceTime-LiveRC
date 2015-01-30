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

     
    }
}
