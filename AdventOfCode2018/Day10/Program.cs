using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        private static List<Point> _points = new List<Point>();

        static void Main(string[] args)
        {
            var lines = FileHelper.GetLines("Day10");
            _points = lines.Select(l => new Point(l)).ToList();

            var seconds = 0;
            while (true)
            {
                const int yLimitPoint = 10;

                var yDifference = _points.Max(p => p.PositionY) - _points.Min(p => p.PositionY);

                if (yDifference < yLimitPoint)
                {
                    // Shift to take care of potential negative values
                    var centre = 500;

                    var grid = new char[_points.Max(p => p.PositionX) + centre + 1, _points.Max(p => p.PositionY) + centre + 1];
                    foreach (var point in _points)
                    {
                        grid[point.PositionX + centre, point.PositionY + centre] = '#';
                    }

                    Console.WriteLine();
                    for (var y = _points.Min(p => p.PositionY); y < _points.Max(p => p.PositionY) + 1; y++)
                    {
                        for (var x = _points.Min(p => p.PositionX); x < _points.Max(p => p.PositionX) + 1; x++)
                        {
                            Console.Write(grid[x + centre, y + centre] == '#' ? '#' : '.');
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine($"Elapsed seconds: {seconds}");
                    break;
                }

                foreach (var point in _points)
                {
                    point.Move();
                }

                seconds++;

                //for (int x = 0; x < gridSize; x++)
                //{
                //    for (int y = 0; y < gridSize; y++)
                //    {
                //        Console.Write(grid[x, y] == '#' ? '#' : '.');
                //    }
                //    Console.WriteLine();
                //}
            }

            Console.ReadKey();
        }
    }
}
