using System.IO;
using System.Xml.Serialization;
using RaceTime.Library.Model.Application;

namespace RaceTime.Library.Storage
{
    internal class ApplicationStorage
    {
        public void Save(Application application)
        {
            string _filename="test_app.xml";

            XmlSerializer writer = new XmlSerializer(typeof(Application));

            File.Delete(_filename);

            using (FileStream file = File.OpenWrite(_filename))
            {

                writer.Serialize(file, application);
            }
        }
    }
}