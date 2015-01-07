using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RaceTime.Library.Test.Practice.Support;
using System.Threading;


namespace RaceTime.Library.Test.Practice
{
    [TestClass]
    public class About_running_practice_with_raceclock : Practice_AcceptanceTest
    {
        [TestMethod]
        public void for_example()
        {
            Given_a_meeting_called("test");

            Can_add_to_the_practice_schedule(300);

            Can_add_to_the_practice_schedule(300);
                        
            Given_the_schedule_run_with_raceclock();
            
            Thread.Sleep(700);



        }
    }
}
