using System.Net.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Application;

namespace RaceTime.Library.Test.Application
{
    public class ApplicationAcceptanceTest
    {
        private Model.Application.Application _app;

        public void Then_the_other_classes_are_created()
        {
            Assert.IsNull(_app.Configuration.Name);

            Assert.IsTrue(_app.ApplicationIsCreated);
        }

        public void Given_a_application()
        {
            _app = new Model.Application.Application();

            _app.Save();
        }
    }
}