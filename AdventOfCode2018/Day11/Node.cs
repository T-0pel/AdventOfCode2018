using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    public class Node
    {
        public int Value { get; set; }
        public Dictionary<int, int> SubGridValues { get; set; } = new Dictionary<int, int>();
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public int GetMaxSubGridValue()
        {
            switch (SubGridValues.Count)
            {
                case 0:
                    return 0;
                case 1:
                    return SubGridValues[2];
                default:
                    return SubGridValues.Aggregate((v1, v2) => v1.Value > v2.Value ? v1 : v2).Value;
            }
        }

        public int GetMaxSubGridSize()
        {
            switch (SubGridValues.Count)
            {
                case 0:
                    return 0;
                case 1:
                    return 2;
                default:
                    return SubGridValues.Aggregate((v1, v2) => v1.Value > v2.Value ? v1 : v2).Key;
            }
        }
    }
}