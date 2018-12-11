using System;
using System.Linq;

namespace Day11
{
    class Program
    {
        private static int _gridSerialNumber = 3031;

        static void Main(string[] args)
        {
            const int gridSize = 300;
            var fuelGrid = new Node[gridSize, gridSize];

            for (var x = 0; x < gridSize; x++)
            {
                for (var y = 0; y < gridSize; y++)
                {
                    var realX = x + 1;
                    var realY = y + 1;

                    var rackId = realX + 10;
                    var powerLevel = rackId * realY;
                    powerLevel += _gridSerialNumber;
                    powerLevel *= rackId;
                    var powerLevelString = powerLevel.ToString();
                    powerLevel = powerLevelString.Length >= 3 ? int.Parse(powerLevelString[powerLevelString.Length - 3].ToString()) : 0;
                    powerLevel -= 5;

                    fuelGrid[x, y] = new Node { PositionX = realX, PositionY = realY, Value = powerLevel };
                }
            }

            for (var x = gridSize - 1; x >= 0; x--)
            {
                for (var y = gridSize - 1; y >= 0; y--)
                {
                    var higherPosition = x > y ? x : y;
                    for (var i = gridSize - higherPosition; i > 1; i--)
                    {
                        var total = 0;
                        for (var xCount = 0; xCount < i; xCount++)
                        {
                            total += fuelGrid[xCount + x, y].Value;
                        }
                        for (var yCount = 1; yCount < i; yCount++)
                        {
                            total += fuelGrid[x, y + yCount].Value;
                        }

                        if (!(x == gridSize - 1 || y == gridSize - 1) && i > 0)
                        {
                            if (i - 1 == 1)
                            {
                                total += fuelGrid[x + 1, y + 1].Value;
                            }
                            else
                            {
                                total += fuelGrid[x + 1, y + 1].SubGridValues[i - 1];
                            }
                        }
                        
                        fuelGrid[x, y].SubGridValues.Add(i, total);

                    }

                    var value = gridSize - higherPosition < 2 ? fuelGrid[x, y].Value : fuelGrid[x, y].SubGridValues[gridSize - higherPosition];
                    //Console.Write(value + "    ");
                }
                //Console.WriteLine();
            }


            var maxValueNode = fuelGrid.Cast<Node>().Aggregate((i1, i2) => i1.GetMaxSubGridValue() > i2.GetMaxSubGridValue() ? i1 : i2);

            Console.WriteLine(maxValueNode.GetMaxSubGridValue());
            Console.WriteLine($"{maxValueNode.PositionX},{maxValueNode.PositionY},{maxValueNode.GetMaxSubGridSize()}");

            Console.ReadKey();
        }
    }
}
