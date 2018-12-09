using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        private const int NumberOfPlayers = 448;
        private const int LastMarbleWorth = 71628;

        static void Main(string[] args)
        {
            var playerScores = new List<int>(new int[NumberOfPlayers]);
            var marbleList = new List<int> { 0 };
            var lastMarbleWorth = 0;
            var newMarbleValue = 1;
            var currentMarbleIndex = 0;
            while (LastMarbleWorth > newMarbleValue)
            {
                int newMarbleIndex;

                if (newMarbleValue % 23 == 0)
                {
                    var scoreMarblePosition = currentMarbleIndex - 7;
                    if (scoreMarblePosition < 0)
                    {
                        scoreMarblePosition = (marbleList.Count) + scoreMarblePosition;
                    }
                    lastMarbleWorth = newMarbleValue + marbleList[scoreMarblePosition];
                    marbleList.RemoveAt(scoreMarblePosition);
                    playerScores[(newMarbleValue % NumberOfPlayers)] += lastMarbleWorth;
                    newMarbleIndex = scoreMarblePosition;
                }
                else
                {
                    if (currentMarbleIndex == marbleList.Count - 1)
                    {
                        newMarbleIndex = 1;
                        marbleList.Insert(newMarbleIndex, newMarbleValue);
                    }
                    else
                    {
                        newMarbleIndex = currentMarbleIndex + 2;
                        marbleList.Insert(newMarbleIndex, newMarbleValue);
                    }
                }

                currentMarbleIndex = newMarbleIndex;
                newMarbleValue++;

                //Console.WriteLine(string.Join(' ', marbleList));
            }

            Console.WriteLine(playerScores.Max());
            Console.ReadKey();
        }
    }
}
