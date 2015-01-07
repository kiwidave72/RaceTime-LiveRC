namespace RaceTime.Library.Controller.Scoreboard
{
    public interface IScoreboard
    {

        void WriteOutput(string line);

        long Interval { get; set; }
    }
}