namespace RaceTime.Library.Model
{

    public enum ScheduleEventType
    {
        Started,
        Stopped,
        Finished,
        IntervalStarted,
        TenSecondCountedStarted

    }


    public class Announcement
    {
        public ScheduleEventType EventType { get; private set; }
        public string Text { get; private set; }

        public Announcement(ScheduleEventType eventType, string text)
        {
            EventType = eventType;
            Text = text;
        }

    }

    
}