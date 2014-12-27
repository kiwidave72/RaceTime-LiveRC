using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Model;
using RaceTime.Library.Model.Application;

namespace RaceTime.Library.Test.Application
{
    [TestClass]
    public class can_create_the_application : ApplicationAcceptanceTest
    {
        [TestMethod]
        public void for_example()
        {
            Given_a_application();

            Then_the_other_classes_are_created();
        }
    }
}
