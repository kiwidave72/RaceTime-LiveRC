using System;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Model
{
     [Serializable()]
    public enum ScheduleEventType
    {
        Started,
        Stopped,
        Finished,
        IntervalStarted,
        TenSecondCountedStarted,
        Next,
        Repeatable
    }

     [Serializable()]
    public class Announcement
    {
        public ScheduleEventType EventType { get; set; }
        
        public string Text { get; set; }
        
        public PracticeClass CurrentPracticeClass { get; set; }
        
        public long Time { get; set; }

         public Announcement()
         {
             
         }

        public Announcement(ScheduleEventType eventType, string text)
        {
            EventType = eventType;
            Text = text;
        }

        public Announcement(ScheduleEventType eventType, string text,long time)
        {
            EventType = eventType;
            Text = text;
            Time = time;
        }

        public Announcement(ScheduleEventType eventType, string text,PracticeClass currentPracticeClass)
        {
            EventType = eventType;
            Text = text;
            CurrentPracticeClass = currentPracticeClass;
            Time = 0;
        }

    }

    
}