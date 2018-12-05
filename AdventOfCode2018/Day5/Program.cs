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

            var isDone = false;
            var previousLetter = '-';
            var resultString = line;
            var indexesToDelete = new HashSet<int>();
            while (!isDone)
            {
                indexesToDelete.Clear();
                var changeInCycle = false;
                var index = 0;
                foreach (var letter in line)
                {
                    if (string.Equals(previousLetter.ToString(), letter.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
                        char.IsUpper(previousLetter) == char.IsLower(letter))
                    {
                        changeInCycle = true;
                        indexesToDelete.Add(index);
                    }

                    previousLetter = letter;
                    index++;
                }

                foreach (var i in indexesToDelete.OrderByDescending(ii => ii))
                {
                    resultString = resultString.Remove(i, 2);
                }

                line = resultString;
                isDone = !changeInCycle;
            }

            Console.WriteLine(resultString);
            Console.WriteLine(resultString.Length);
            Console.ReadKey();
        }
    }
}
