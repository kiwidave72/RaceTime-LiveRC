using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model;
using ServiceStack;

namespace RaceTime.Library.Test.LiveRC.Support.Interactor
{
    public class PHPLiveRCInteractor
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


        public List<DriverData> Parse(string data)
        {
            var json = data.Remove(0, 4);

            var liveRcDriverData = json.FromJson<LiveRcDriverData>();

            var singlePacket = liveRcDriverData.args[0].FromJson<DriverPositionData>();

            var LiveRCPositionDataParser = Interactor.LiveRCPositionDataParser.Parse(singlePacket);


            return LiveRCPositionDataParser;

        }
    }

    public static class LiveRCPositionDataParser
    {
        public static List<DriverData> Parse(DriverPositionData data)
        {

            var result = new List<DriverData>();

            ParseSingleDriver(data.p1, result);
            ParseSingleDriver(data.p2, result);
            ParseSingleDriver(data.p3, result);
            ParseSingleDriver(data.p4, result);
            ParseSingleDriver(data.p5, result);
            ParseSingleDriver(data.p6, result);
            ParseSingleDriver(data.p7, result);
            ParseSingleDriver(data.p8, result);
            ParseSingleDriver(data.p9, result);
            ParseSingleDriver(data.p10, result);

            return result;
        }

        private static void ParseSingleDriver(string data, List<DriverData> result)
        {
            if (!string.IsNullOrEmpty(data ))
            {
                var dataArray = data.Split('|');
                var item = new DriverData();

                item.Position = dataArray[0];

                item.Name = dataArray[1];

                result.Add(item);
            }
        }
    }

    public class LiveRcDriverData
    {

        public string name { get; set; }
        public string[] args { get; set; }
    }

    public class DriverPositionData
    {
        public string p1 { get; set; }
        public string p2 { get; set; }
        public string p3 { get; set; }
        public string p4 { get; set; }
        public string p5 { get; set; }
        public string p6 { get; set; }
        public string p7 { get; set; }
        public string p8 { get; set; }
        public string p9 { get; set; }
        public string p10 { get; set; }
    }

    public class DriverData
    {
        public string Name { get; set; }
        public string Position { get; set; }

    }


    public class argsValues
    {
        public string values { get; set; }
    }


    public class EventData
    {

        public Local[] local { get; set; }
        public Premium[] premium { get; set; }
        
    }

    public class Premium
    {
        public Local __invalid_type__269 { get; set; }
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