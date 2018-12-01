using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    public class FrequencyCalculator
    {
        private const string FileName = "PuzzleInput.txt";

        public static int CalculateFrequency()
        {
            var calculatedFrequency = 0;

            var lines = GetLines();
            foreach (var line in lines)
            {
                calculatedFrequency += GetNumber(line);
            }

            return calculatedFrequency;
        }

        public static int CalculateFrequencyReachedTwice()
        {
            var reachedFrequencies = new HashSet<int> { 0 };
            var frequencyReachedTwice = false;
            var lines = GetLines().ToList();
            var calculatedFrequency = 0;

            while (!frequencyReachedTwice)
            {
                foreach (var line in lines)
                {
                    calculatedFrequency += GetNumber(line);

                    if (reachedFrequencies.Contains(calculatedFrequency))
                    {
                        frequencyReachedTwice = true;
                        break;
                    }
                    else
                    {
                        reachedFrequencies.Add(calculatedFrequency);
                    }
                }
            }

            return calculatedFrequency;
        }

        private static int GetNumber(string line)
        {
            var number = int.Parse(line.Substring(1));

            return line[0] == '+' ? number : -number;
        }

        private static IEnumerable<string> GetLines()
        {
            var inputPath = Path.Combine(Environment.CurrentDirectory, @"Data\", FileName);

            var lines = File.ReadLines(inputPath);

            return lines;
        }
    }
}
