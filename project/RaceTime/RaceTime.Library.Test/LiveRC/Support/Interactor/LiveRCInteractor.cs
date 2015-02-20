using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;

namespace RaceTime.Library.Test.LiveRC.Support.Interactor
{
    public class LiveRCInteractor
    {
       

        public EventData FetchEvents()
        {
            var url = "http://live.liverc.com/data/getEventList.php";

            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);


            var data = reader.ReadToEnd();

            var stuff = data.FromJson<EventData>();

            return stuff;
        }


        public DriverData Parse(string data)
        {
            var json = data.Remove(0, 4);
            var stuff = json.FromJson<DriverData>();

            return stuff;

        }
    }

    public class DriverData
    {

        public string name { get; set; }
        public string args { get; set; }
    }

    public class argsValues
    {
        public string values { get; set; }
    }


    public class EventData
    {

        public Local[] local { get; set; }
        public Local[] premium { get; set; }
        
    }

    public class Local
    {
        public string track_type { get; set; }

        public string track_id { get; set; }

        public string track_name { get; set; }

        public string Description { get; set; }

        public string data_directory { get; set; }

        public string Status { get; set; }

        public string event_id { get; set; }

        public string event_title { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }

        public string local_time_offset { get; set; }

        public string aap_event { get; set; }

        public string frc_event { get; set; }
    }
}