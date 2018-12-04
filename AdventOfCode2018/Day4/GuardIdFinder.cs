using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SharedLibrary;

namespace Day4
{
    public static class GuardIdFinder
    {
        private const string DirectoryName = "Day4";
        private static Dictionary<int, GuardEntry> _guardEntries;

        private static IOrderedEnumerable<LineMessage> GetOrderedLines()
        {
            var lines = FileHelper.GetLines(DirectoryName);
            var lineMessages = lines.Select(l => new LineMessage(l)).ToList();

            return lineMessages.OrderBy(lm => lm.DateAndTime);
        }

        private static void ParseLineMessages(IOrderedEnumerable<LineMessage> orderedLineMessages)
        {
            _guardEntries = new Dictionary<int, GuardEntry>();
            GuardEntry entry = null;
            foreach (var lineMessage in orderedLineMessages)
            {
                if (lineMessage.Message.Contains("Guard"))
                {
                    var guardId = int.Parse(Regex.Match(lineMessage.Message, @"\d+").Value);
                    if (!_guardEntries.ContainsKey(guardId))
                    {
                        entry = new GuardEntry(guardId);
                        _guardEntries[guardId] = entry;
                    }
                    else
                    {
                        entry = _guardEntries[guardId];
                    }
                    
                    entry.AwokeRanges.Add(new Tuple<DateTime, DateTime>(lineMessage.DateAndTime, DateTime.Now));
                }
                else if (lineMessage.Message.Contains("falls"))
                {
                    var lastRange = entry.AwokeRanges[entry.AwokeRanges.Count - 1];
                    lastRange = new Tuple<DateTime, DateTime>(lastRange.Item1, lineMessage.DateAndTime);
                    entry.AwokeRanges[entry.AwokeRanges.Count - 1] = lastRange;

                    entry.AsleepRanges.Add(new Tuple<DateTime, DateTime>(lineMessage.DateAndTime, DateTime.Now));
                }
                else
                {
                    var lastRange = entry.AsleepRanges[entry.AsleepRanges.Count - 1];
                    lastRange = new Tuple<DateTime, DateTime>(lastRange.Item1, lineMessage.DateAndTime);
                    entry.AsleepRanges[entry.AsleepRanges.Count - 1] = lastRange;

                    entry.AwokeRanges.Add(new Tuple<DateTime, DateTime>(lineMessage.DateAndTime, DateTime.Now));
                }
            }
        }

        public static GuardEntry GetHighestSleepTimeGuard()
        {
            var lines = GetOrderedLines();
            ParseLineMessages(lines);
            return _guardEntries.Aggregate((l, r) => l.Value.TotalTimeAsleep > r.Value.TotalTimeAsleep ? l : r).Value;
        }

        public static int HighestSleepTimeAnswer()
        {
            var guard = GetHighestSleepTimeGuard();
            var mostAsleepMinute = guard.GetHighestSleepTimeMinute(true);

            return guard.Id * mostAsleepMinute;
        }

        public static int HighestSleepMinuteAnswer()
        {
            var lines = GetOrderedLines();
            ParseLineMessages(lines);
            var highestMinutes = 0;
            var guardId = 0;
            foreach (var guardEntry in _guardEntries.Values)
            {
                var mostAsleepMinutes = guardEntry.GetHighestSleepTimeMinute(false);
                if (highestMinutes < mostAsleepMinutes)
                {
                    highestMinutes = mostAsleepMinutes;
                    guardId = guardEntry.Id;
                }
            }

            var mostAsleepMinute = _guardEntries[guardId].GetHighestSleepTimeMinute(true);

            return guardId * mostAsleepMinute;
        }
    }
}