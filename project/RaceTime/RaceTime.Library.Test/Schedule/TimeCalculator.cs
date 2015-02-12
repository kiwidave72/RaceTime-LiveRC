using System;
using System.Collections.Generic;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Test.Schedule
{
    public static class TimeCalculator
    {
        public static DateTime CalculateTime(DateTime startTime, List<PracticeClass> practiceClasses)
        {
            DateTime endTime = startTime;

            foreach (var race in practiceClasses)
            {
                endTime = endTime.AddMinutes(race.Time);
            }

            return endTime;
        }
    }
}