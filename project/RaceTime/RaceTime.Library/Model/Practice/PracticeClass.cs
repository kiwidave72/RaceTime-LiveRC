using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceTime.Library.Model.Practice
{
    [Serializable()]
    public class PracticeClass
    {

        private string _name;
        private long _time;
        private string _status;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public long Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value;}
        }

        public int HeatNumber { get; set; }

        public PracticeClass(string name, long time)
        {
            Name = name;
            Time = time;
            Status = "Ready";

        }

    }
}
