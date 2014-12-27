using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTime.Library.Test.Configuration.Support;

namespace RaceTime.Library.Test.Configuration
{
    [TestClass]
    public class can_save_and_load_configuration : ConfigurationAcceptanceTest
    {
        [TestMethod]
        public void For_When_I_can_save_the_configuration_to_a_file()
        {
            Given_a_configuration();

            When_I_save();

            Then_there_are_no_errors();
        }

        [TestMethod]
        public void For_when_i_load_the_configuration_from_a_file()
        {
            When_I_create_a_default_configuration();

            When_I_read();

            Then_there_are_no_errors();

            Then_the_name_is("This is a test Configuration");
        }


    }
}
