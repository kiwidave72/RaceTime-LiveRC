using System;
using RaceTime.Library.Model.Schedule;

namespace RaceTime.Library.Model.Configuration
{
    [Serializable()]
    public class Configuration
    {
        public string Name { get; set; }

        public DefaultSchedule Schedule { get; set; }
    }
}
