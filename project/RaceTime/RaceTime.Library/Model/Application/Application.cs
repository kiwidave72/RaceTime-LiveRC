using System;
using System.Configuration;
using RaceTime.Library.Storage;

namespace RaceTime.Library.Model.Application
{
    [Serializable]
    public class Application
    {
        private readonly Configuration.Configuration _configuration;

        private bool _applicationIsCreated = false;

        public Application()
        {
            _configuration = new Configuration.Configuration();
        }

        public Configuration.Configuration Configuration
        {
            get { return _configuration; }
        }

        public bool ApplicationIsCreated
        {
            get { return _applicationIsCreated; }
        }

        public void Save()
        {
            SaveConfiguration();

            SaveApplication();
        }

        private void SaveApplication()
        {
            ApplicationStorage storage = new ApplicationStorage();

            storage.Save(this);

            _applicationIsCreated = true;
        }

        private void SaveConfiguration()
        {
            ConfigurationStorage storage = new ConfigurationStorage();

            storage.Save(Configuration);
        }
    }
}