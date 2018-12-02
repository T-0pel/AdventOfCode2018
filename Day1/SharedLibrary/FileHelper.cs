using System;
using System.Collections.Generic;
using System.IO;

namespace SharedLibrary
{
    public class FileHelper
    {
        private const string FileName = "PuzzleInput.txt";

        public static IEnumerable<string> GetLines(string directory)
        {
            var inputPath = Path.Combine(Environment.CurrentDirectory, $@"Data\{directory}\", FileName);

            var lines = File.ReadLines(inputPath);

            return lines;
        }
    }
}
