using System;

namespace Day6
{
    public class Point
    {
        public int Id { get; }
        public int X { get; set; }
        public int Y { get; set; }

        private static int _idCounter;

        public Point()
        {
            Id = _idCounter++;
        }

        public int CalculateDistance(int x, int y)
        {
            var xDistance = Math.Abs(X - x);
            var yDistance = Math.Abs(Y - y);

            return xDistance + yDistance;
        }
    }
}