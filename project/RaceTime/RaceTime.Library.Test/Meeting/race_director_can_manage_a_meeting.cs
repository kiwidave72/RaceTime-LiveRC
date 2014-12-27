using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model.Race;
using RaceTime.Library.Test.Meeting.Support;

namespace RaceTime.Library.Test.Meeting
{
    [TestClass]
    public class race_director_can_manage_a_meeting : MeetingAcceptanceTest
    {
    
        [TestMethod]
        public void that_a_meeting_has_a_title()
        {
            Given_a_meeting_called("test");

            Then_i_get_the_meeting_title("test");
        }

        [TestMethod]
        public void can_create_a_race_class()
        {
            var raceClasses = new[]
            {
                Single("New Class")
            };

            Given_these_classes(raceClasses);

            When_i_fetch();

            Then_i_get_the_class("New Class");

        }

        [TestMethod]
        public void can_create_more_than_one_race_class()
        {
            var raceClasses = new[]
            {
                Single("New Class"),
                Single("Other Class")
            };

            Given_these_classes(raceClasses);

            When_i_fetch();

            Then_i_get_these_class_names(new[]{"New Class","Other Class"});
        }

        private static RaceClass Single(string newClass)
        {
            return new RaceClass(newClass);
        }
    }
}
