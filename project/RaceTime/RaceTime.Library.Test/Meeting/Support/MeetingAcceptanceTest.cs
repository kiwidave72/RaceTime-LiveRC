using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Race;

namespace RaceTime.Library.Test.Meeting.Support
{
    public class MeetingAcceptanceTest
    {
        private List<RaceClass> _classCollection;

        public void Then_i_get_the_meeting_title(string test)
        {
            Assert.IsTrue(Interactor.GetTitle() == test);
        }

        public void Given_a_meeting_called(string test)
        {
            Interactor.SetTitle(test);
        }


        public void Then_i_get_the_class(string className)
        {
            Assert.IsTrue(_classCollection.SingleOrDefault(i=>i.Name == className)!=null );
        }

        public void Then_i_get_these_class_names(string[] strings)
        {
            foreach (var s in strings)
            {
                 Then_i_get_the_class(s);
            }
        }


        public void When_i_fetch()
        {
            _classCollection=  Interactor.FetchAll();

        }
        public void Given_these_classes(RaceClass[] raceClass)
        {
            Interactor.ClearClasses();
            foreach (var a in raceClass)
            {
                Interactor.CreateClass(a);
            }

        }

    }
}