using System;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var checksum = ChecksumCalculator.CalculateChecksum();
            var commonLetters = ChecksumCalculator.FindCommonLetters();
            Console.WriteLine($"Checksum is {checksum}");
            Console.WriteLine($"Common letters between the two correct box IDs are: {commonLetters}");
            Console.ReadKey();
        }
    }
}
