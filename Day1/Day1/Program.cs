using System;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var frequency = FrequencyCalculator.CalculateFrequency();
            var frequencyReachedTwice = FrequencyCalculator.CalculateFrequencyReachedTwice();
            Console.WriteLine($"Resulting frequency is: {frequency}");
            Console.WriteLine($"First frequency reached twice is: {frequencyReachedTwice}");
            Console.ReadKey();
        }
    }
}
