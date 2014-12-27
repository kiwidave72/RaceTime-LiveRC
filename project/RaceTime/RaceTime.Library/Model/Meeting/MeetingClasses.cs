using System;
using System.Collections.Generic;
using RaceTime.Library.Model.Race;

namespace RaceTime.Library.Model.Meeting
{
    [Serializable()]
    public class MeetingClasses
    {
        private List<RaceClass> _classes =new List<RaceClass>();

        public List<RaceClass> All()
        {
            return _classes;
        }

        public void Create(RaceClass newClass)
        {
            _classes.Add(newClass);
        }

        public void Clear()
        {
            _classes.Clear();
        }
    }
}