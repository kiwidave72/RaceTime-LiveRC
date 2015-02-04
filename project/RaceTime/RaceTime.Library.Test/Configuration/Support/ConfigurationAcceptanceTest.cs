using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Schedule;
using RaceTime.Library.Storage;

namespace RaceTime.Library.Test.Configuration.Support
{
    public class ConfigurationAcceptanceTest
    {
        private Model.Configuration.Configuration _config;

        private Exception _error;
        private readonly ConfigurationStorage _configurationStorage;

        public ConfigurationAcceptanceTest()
        {
            _configurationStorage = new ConfigurationStorage();
        }

        public Model.Configuration.Configuration Configuration
        {
            get { return _config; }
            set { _config = value; }
        }

        protected void Then_the_name_is(string theName)
        {
            Assert.IsTrue(Configuration.Name == theName);
        }

        protected void When_I_create_a_default_configuration()
        {
            Given_a_configuration();

            When_I_save();

            Then_there_are_no_errors();

            And_clear_the_configuration();
        }

        private void And_clear_the_configuration()
        {
            this.Configuration = null;
        }


        protected void Given_a_configuration()
        {
            Configuration = new Model.Configuration.Configuration();

            Configuration.Name = "This is a test Configuration";
        }

        protected void Given_a_configuration_with_a_schedule()
        {
            Configuration = new Model.Configuration.Configuration();

            Configuration.Name = "This is a test Configuration";

            Configuration.Schedule = new DefaultSchedule(null);
            
        }


        protected void When_I_read()
        {
            _error = null;
            try
            {
                _configurationStorage.Load("Configuration.xml");
                Configuration = _configurationStorage.Configuration;
            }
            catch (Exception ex)
            {

                _error = ex;
            }
        }


        protected void When_I_save()
        {
            _error = null;
            try
            {
                _configurationStorage.Save(Configuration);

            }
            catch (Exception ex)
            {
                _error = ex;
            }
        }

        protected void Then_there_are_no_errors()
        {
            Assert.IsNull(_error);
        }
    }
}