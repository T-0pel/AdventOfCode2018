using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharedLibrary;

namespace Day1
{
    public class FrequencyCalculator
    {
        private const string DirectoryName = "Day1";

        public static int CalculateFrequency()
        {
            var lines = FileHelper.GetLines(DirectoryName);

            return lines.Sum(GetNumber);
        }

        public static int CalculateFrequencyReachedTwice()
        {
            var reachedFrequencies = new HashSet<int> { 0 };
            var frequencyReachedTwice = false;
            var lines = FileHelper.GetLines(DirectoryName).ToList();
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
    }
}
