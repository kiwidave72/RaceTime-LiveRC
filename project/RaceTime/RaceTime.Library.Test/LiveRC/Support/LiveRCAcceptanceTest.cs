using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Test.LiveRC.Support.Interactor;

namespace RaceTime.Library.Test.LiveRC.Support
{
    public class LiveRCAcceptanceTest
    {
        private readonly PHPLiveRCInteractor _phpLiveRcInteractor = new PHPLiveRCInteractor();

        public EventData _eventJson;

        protected List<DriverData> _resultJson;
        
        protected void When_I_fetch_the_events()
        {
            _eventJson = _phpLiveRcInteractor.FetchEvents();
        }

        protected void Then_I_get_the_list_of_events()
        {
            Assert.IsTrue(_eventJson.local.Count() >0);
        }

        public void Given_this_raw_driver_data(string data)
        {
            _resultJson = _phpLiveRcInteractor.Parse(data);

        }
    }


}
