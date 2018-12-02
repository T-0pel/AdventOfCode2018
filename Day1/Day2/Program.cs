using System;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var checksum = ChecksumCalculator.CalculateChecksum();
            Console.WriteLine($"Checksum is {checksum}");
            Console.ReadKey();
        }
    }
}
