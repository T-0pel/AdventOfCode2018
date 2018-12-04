using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    public class GuardEntry
    {
        public int Id { get; set; }
        public List<Tuple<DateTime, DateTime>> AwokeRanges { get; set; }
        public List<Tuple<DateTime, DateTime>> AsleepRanges { get; set; }
        public int TotalTimeAsleep
        {
            get
            {
                return AsleepRanges.Sum(ar => (ar.Item2 - ar.Item1).Minutes);
            }
        }

        public GuardEntry(int id)
        {
            Id = id;
            AwokeRanges = new List<Tuple<DateTime, DateTime>>();
            AsleepRanges = new List<Tuple<DateTime, DateTime>>();
        }

        public int GetHighestSleepTimeMinute(bool getMinute)
        {
            var asleepMinutes = new Dictionary<int, int>();
            for (var minute = 0; minute < 60; minute++)
            {
                foreach (var asleepRange in AsleepRanges)
                {
                    if (minute >= asleepRange.Item1.Minute && minute < asleepRange.Item2.Minute)
                    {
                        if (asleepMinutes.ContainsKey(minute))
                        {
                            asleepMinutes[minute] = asleepMinutes[minute] + 1;
                        }
                        else
                        {
                            asleepMinutes[minute] = 1;
                        }
                    }
                }
            }

            if (asleepMinutes.Count == 0) return 0;
            var highestMinute = asleepMinutes.Aggregate((l, r) => l.Value > r.Value ? l : r);
            return getMinute ? highestMinute.Key : highestMinute.Value;
        }
    }
}