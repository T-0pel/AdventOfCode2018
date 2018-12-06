using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = FileHelper.GetLines("Day5").First();

            var distinctLetters = new HashSet<char>(line.ToLower());
            var shortestPolymerLength = int.MaxValue;

            foreach (var distinctLetter in distinctLetters)
            {
                var helperLine = line;
                helperLine = helperLine.Replace(distinctLetter.ToString(), "", StringComparison.CurrentCultureIgnoreCase);

                var index = 0;
                var previousLetter = '-';
                while (index < helperLine.Length)
                {
                    var letter = helperLine[index];

                    if (string.Equals(previousLetter.ToString(), letter.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
                        char.IsUpper(previousLetter) == char.IsLower(letter))
                    {
                        helperLine = helperLine.Remove(--index, 2);
                    }
                    else
                    {
                        index++;
                    }

                    previousLetter = index == 0 ? '-' : helperLine[index - 1];
                }

                if (shortestPolymerLength > helperLine.Length)
                {
                    shortestPolymerLength = helperLine.Length;
                }
            }

            Console.WriteLine(shortestPolymerLength);
            Console.ReadKey();
        }
    }
}
