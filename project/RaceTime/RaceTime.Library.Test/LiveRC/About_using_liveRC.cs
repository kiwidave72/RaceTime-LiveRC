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
            Given_this_driver_data_test_data(@"5:::{""name"":""updateDriverData"",""args"":[""{\""p10\"":\""10|John Martin|10|5|41.970|4:57.7|8/7:56.338|41.970|10|false|1|123||United States||59.5|1:10.523|5|false|139.678\""}""]})");



        }

        
    }
}
