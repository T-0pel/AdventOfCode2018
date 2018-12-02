using System;
using System.Collections.Generic;
using System.Text;
using SharedLibrary;

namespace Day2
{
    public class ChecksumCalculator
    {
        private const string DirectoryName = "Day2";

        public static int CalculateChecksum()
        {
            var lines = FileHelper.GetLines(DirectoryName);
            var characterThreeTimes = 0;
            var characterTwoTimes = 0;

            foreach (var line in lines)
            {
                var characterDictionary = new Dictionary<char, int>();

                foreach (var character in line)
                {
                    if (characterDictionary.ContainsKey(character))
                    {
                        characterDictionary[character] += 1;
                    }
                    else
                    {
                        characterDictionary.Add(character, 1);
                    }
                }

                if (characterDictionary.ContainsValue(2)) characterTwoTimes += 1;
                if (characterDictionary.ContainsValue(3)) characterThreeTimes += 1;
            }

            return characterTwoTimes * characterThreeTimes;
        }
    }
}
