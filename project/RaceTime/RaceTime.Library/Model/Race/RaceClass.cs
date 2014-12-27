using System;

namespace RaceTime.Library.Model.Race
{
    [Serializable()]
    public class RaceClass
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public RaceClass(string name)
        {
            Name = name;
        }
    }
}