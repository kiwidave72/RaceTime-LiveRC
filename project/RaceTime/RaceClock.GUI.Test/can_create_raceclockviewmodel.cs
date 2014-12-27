using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.GUI;

namespace RaceClock.GUI.Test
{
    [TestClass]
    public class can_create_raceclockviewmodel
    {
        [TestMethod]
        public void for_example()
        {
            var raceClock = new RaceClockModelView();

            raceClock.Model.SetRaceTime(1000);
            
            ICommand command = raceClock.StartRaceCommand;
            command.Execute(null);

            Assert.IsTrue(raceClock.Model.HasStarted);
        }
    }
}
