using System;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var overlap = ClaimCalculator.OverlappingFabricInches();
            var withoutOverlapId = ClaimCalculator.GetClaimWithoutOverlap();
            Console.WriteLine($"Square inches of fabric that are within two or more claims: {overlap}");
            Console.WriteLine($"ID of the only claim that doesn't overlap: {withoutOverlapId}");
            Console.ReadKey();
        }
    }
}
