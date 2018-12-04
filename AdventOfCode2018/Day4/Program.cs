using System;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = GuardIdFinder.HighestSleepMinuteAnswer();
            Console.WriteLine("What is the ID of the guard you chose multiplied by the minute you chose?");
            Console.WriteLine(answer);
            Console.ReadKey();
        }
    }
}
