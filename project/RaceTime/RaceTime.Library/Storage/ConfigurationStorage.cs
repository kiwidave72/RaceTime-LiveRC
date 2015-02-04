using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Configuration;

namespace RaceTime.Library.Storage
{
    public class ConfigurationStorage
    {
        private Configuration _configuration;
        private string _filename;

        public Configuration Configuration
        {
            get { return _configuration; }
            private set { _configuration = value; }
        }

        public ConfigurationStorage()
        {
            _filename = @"configuration.xml";
        }

        public void Load(string filename)
        {
            XmlSerializer reader = new XmlSerializer(typeof (Configuration));

            using (FileStream input = File.OpenRead(filename))
            {
                
                Configuration = (Configuration)reader.Deserialize(input);
            }
        }

        public void Save(Configuration configuration)
        {
            Configuration = configuration;

            XmlSerializer writer = new XmlSerializer(typeof(Configuration));
            
            File.Delete(_filename);
            
            using (FileStream file = File.OpenWrite(_filename))
            {
                
                writer.Serialize(file, Configuration);
            }
        }
    }
}