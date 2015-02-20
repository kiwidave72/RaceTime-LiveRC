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
        private readonly LiveRCInteractor _liveRcInteractor = new LiveRCInteractor();

        private EventData _eventJson;

        private DriverData _resultJson;
        
        protected void When_I_fetch_the_events()
        {
            _eventJson = _liveRcInteractor.FetchEvents();
        }

        protected void Then_I_get_the_list_of_events()
        {
            Assert.IsTrue(_eventJson.local.Count() >0);
        }

        public void Given_this_driver_data_test_data(string data)
        {
            _resultJson = _liveRcInteractor.Parse(data);

        }
    }


}
