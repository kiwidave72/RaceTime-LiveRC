using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Test.Practice.Support;

namespace RaceTime.Library.Test.Practice
{
    [TestClass]
    public class About_practice_schedule : Practice_AcceptanceTest
    {
        [TestMethod]
        public void A_race_director_can_create_a_practice_schedule()
        {
           Given_a_meeting_called("test");

           Can_add_to_the_practice_schedule(300);

           Then_I_have_this_many(1);

        }




    }

    
    

}
