using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net;
using System.Runtime.Serialization.Json;

namespace RaceTime.Library.Test.LiveRC.Support.Interactor
{
    public class LiveRCInteractor
    {
        public void ConnectWithLiveRC(string httpLiveLivercComDataGeteventlistPhp)
        {
                
        }

        public string FetchEvents()
        {
            var url = "http://live.liverc.com/data/getEventList.php";

            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream dataStream= response.GetResponseStream();

            var serializer = new DataContractJsonSerializer(typeof(List<local>));

            List<local> events = (List<local>)serializer.ReadObject(dataStream);



            TextReader reader =new StreamReader( dataStream);

            String body = reader.ReadToEnd();

            return body;

        }
    }
}