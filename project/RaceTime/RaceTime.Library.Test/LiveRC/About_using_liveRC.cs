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
            //Given_a_connection();

            When_I_fetch_the_events();

           // Then_I_get_the_list_of_events();
        }


    }
}
