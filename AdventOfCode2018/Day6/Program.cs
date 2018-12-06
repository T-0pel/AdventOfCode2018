using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        private static void MySolution()
        {
            var lines = FileHelper.GetLines("Day6");
            var points = new List<Point>();
            var gridExtension = 2;
            var safeArea = 0;

            foreach (var line in lines)
            {
                var split = line.Split(',');
                var y = int.Parse(split[0].Trim()) + gridExtension;
                var x = int.Parse(split[1].Trim()) + gridExtension;

                points.Add(new Point { X = x, Y = y });
            }

            var grid = new int[points.Max(p => p.X) + gridExtension + 1, points.Max(p => p.Y) + gridExtension + 1];

            var disqualifiedCharacters = new HashSet<int>();
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                for (var y = 0; y < grid.GetLength(1); y++)
                {
                    var shortestDistance = int.MaxValue;
                    var shortestDistanceIds = new List<int>();
                    var distanceTotal = 0;

                    foreach (var point in points)
                    {
                        var distance = point.CalculateDistance(x, y);
                        distanceTotal += distance;

                        if (shortestDistance > distance)
                        {
                            shortestDistance = distance;
                            shortestDistanceIds.Clear();
                            shortestDistanceIds.Add(point.Id);
                        }
                        else if (shortestDistance == distance)
                        {
                            shortestDistanceIds.Add(point.Id);
                        }
                    }

                    var id = shortestDistanceIds.Count > 1 ? -1 : shortestDistanceIds[0];
                    grid[x, y] = id;

                    if (x == 0 || y == 0 || x == grid.GetLength(0) - 1 || y == grid.GetLength(1) - 1)
                    {
                        disqualifiedCharacters.Add(id);
                    }

                    if (distanceTotal < 10000) safeArea++;

                    var pointPlace = points.FirstOrDefault(p => p.X == x && p.Y == y);
                    if (pointPlace != null)
                    {
                        grid[x, y] = pointPlace.Id;

                        //Console.Write(char.ToUpper(pointPlace.Id));
                    }
                }
                //Console.WriteLine();
            }

            var results = grid.Cast<int>().GroupBy(n => n);

            foreach (var result in results.Where(r => !disqualifiedCharacters.Contains(r.Key)).OrderByDescending(r => r.Count()))
            {
                Console.WriteLine($"[{result.Key}, {result.Count()}]");
            }

            Console.WriteLine(safeArea);

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            MySolution();
        }
    }
}
