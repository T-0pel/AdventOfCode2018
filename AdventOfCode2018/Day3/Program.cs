using System;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var overlap = ClaimCalculator.OverlappingFabricInches();
            Console.WriteLine($"Square inches of fabric that are within two or more claims: {overlap}");
            Console.ReadKey();
        }
    }
}
