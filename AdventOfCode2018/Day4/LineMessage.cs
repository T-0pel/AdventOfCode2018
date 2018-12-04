using System;

namespace Day4
{
    public class LineMessage
    {
        public DateTime DateAndTime { get; }
        public string Message { get; }

        public LineMessage(string line)
        {
            var split = line.Split(']');
            DateAndTime = DateTime.ParseExact(split[0].Substring(1), "yyyy-MM-dd HH:mm", null);
            Message = split[1].Trim();
        }
    }
}