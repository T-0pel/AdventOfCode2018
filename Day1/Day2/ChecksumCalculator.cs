using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string FindCommonLetters()
        {
            var lines = FileHelper.GetLines(DirectoryName).ToList();

            foreach (var line in lines)
            {
                foreach (var lineToCompare in lines)
                {
                    var differentCharacterIndexes = new HashSet<int>();

                    for (var i = 0; i < line.Length; i++)
                    {
                        if (line[i] != lineToCompare[i])
                        {
                            differentCharacterIndexes.Add(i);
                        }

                        if (differentCharacterIndexes.Count > 1) break;
                    }

                    if (differentCharacterIndexes.Count == 1)
                    {
                        return line.Remove(differentCharacterIndexes.First(), 1);
                    }
                }
            }

            return "No two entries differ by just one character!";
        }
    }
}
