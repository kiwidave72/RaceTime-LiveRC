using System;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.GUI;
using RaceTime.Library.Model.Practice;

namespace RaceClock.GUI.Test
{
    [TestClass]
    public class can_create_ScheduleModelView
    {
        ScheduleModelView schedule ;

        public can_create_ScheduleModelView()
        {
            schedule = new ScheduleModelView();

            schedule.Model.Add(new PracticeClass("Super Stock Touring", 1000 * 10));

            schedule.Model.Add(new PracticeClass("Modified Touring", 1000 * 10));

            schedule.Model.Add(new PracticeClass("Pro10 and Pro12", 1000 * 10));

            schedule.Model.Add(new PracticeClass("Formula One", 1000 * 10));

            schedule.Model.Add(new PracticeClass("Mini's", 1000 * 10));

            schedule.Model.Add(new PracticeClass("Under 12's", 1000 * 10));            
        }

        [TestMethod]
        public void it_runs_like_this()
        {
            ICommand command = schedule.StartScheduleCommand;
            command.Execute(null);

            Assert.IsTrue(schedule.Model.IsScheduleRunning);
        }

        [TestMethod]
        public void it_stops_like_this()
        {
            ICommand command = schedule.StartScheduleCommand;
            command.Execute(null);


            ICommand stopcommand = schedule.StopScheduleCommand;
            stopcommand.Execute(null);

            Assert.IsTrue(schedule.Model.IsScheduleRunning==false);
        }
    }
}
