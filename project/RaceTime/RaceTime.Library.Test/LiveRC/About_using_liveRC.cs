using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Test.LiveRC.Support;

namespace RaceTime.Library.Test.LiveRC
{
    [TestClass]
    public class About_using_liveRC : LiveRCAcceptanceTest
    {
        [TestMethod]
        public void Can_get_the_list_of_events()
        {
           
            When_I_fetch_the_events();

            Then_I_get_the_list_of_events();
        }

        [TestMethod]
        public void Can_read_the_result_from_updateDriverData()
        {
            Given_this_driver_data_test_data(@"5:::{""name"":""updateDriverData"",""args"":[""{\""p8\"":\""8|Matt Schreffler|5|5|39.181|3:17.5|11/7:14.588|38.043|25|false|1|862||United States||39.5|0.203|2|false|119.016\"",\""p9\"":\""9|Paul Ciccarello|15|4|39.931|2:38.2|11/7:15.149|38.482|25|false|1|879||United States||39.5|0.255|1|false|118.305\"",\""p10\"":\""10|Mike Moralez|7|4|39.381|2:38.9|11/7:17.102|39.381|25|false|1|872||||39.7|0.710|4|false|119.332\""}""]}");



        }

        
    }
}
