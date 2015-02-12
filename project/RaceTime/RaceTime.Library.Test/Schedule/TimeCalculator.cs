using System;
using System.Collections.Generic;
using RaceTime.Library.Model.Practice;

namespace RaceTime.Library.Test.Schedule
{
    public static class TimeCalculator
    {
        public static DateTime CalculateTime(DateTime startTime, List<PracticeClass> practiceClasses, int roundNo)
        {
            DateTime endTime = startTime;


            foreach (var race in practiceClasses)
            {
                endTime = endTime.AddMinutes(race.Time * roundNo);
            }

            return endTime;
        }

        public static DateTime CalculateTime(DateTime startTime, List<PracticeClass> practiceClasses)
        {
            return CalculateTime(startTime, practiceClasses, 1);
        }
    }
}