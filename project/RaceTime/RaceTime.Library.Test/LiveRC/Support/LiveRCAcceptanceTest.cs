using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using RaceTime.Library.Test.LiveRC.Support.Interactor;

namespace RaceTime.Library.Test.LiveRC.Support
{
    public class LiveRCAcceptanceTest
    {
        private readonly LiveRCInteractor _liveRcInteractor = new LiveRCInteractor();

        private void Given_a_connection()
        {
        }


        protected void When_I_fetch_the_events()
        {
            string eventJson = _liveRcInteractor.FetchEvents();



        }
    }


    [DataContract]
    public class local
    {
        [DataMember] public int track_id;

        [DataMember] public String track_name;

        [DataMember] public String status;

        [DataMember] public String aap_event;

        [DataMember] public String data_directory;

        [DataMember] public String description;

        [DataMember]public String end_date;

        [DataMember] public String event_id;

        [DataMember] public String event_title;

        [DataMember] public String frc_event;

        [DataMember] public String local_time_offset;

        [DataMember] public String start_date;
    }
}
