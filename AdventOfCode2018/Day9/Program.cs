using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        private const long NumberOfPlayers = 448;
        private const long LastMarbleWorth = 71628 * 100;

        static void Main(string[] args)
        {
            var playerScores = new List<long>(new long[NumberOfPlayers]);
            var marbleList = new LinkedList<long>();
            marbleList.AddFirst(0);
            long newMarbleValue = 1;
            var currentNode = marbleList.First;
            while (LastMarbleWorth > newMarbleValue)
            {
                if (newMarbleValue % 23 == 0)
                {
                    var positionLeft = 7;
                    var scoreNode = currentNode;
                    while(positionLeft > 0)
                    {
                        scoreNode = scoreNode?.Previous ?? marbleList.Last;
                        positionLeft--;
                    }

                    currentNode = scoreNode.Next;
                    var lastMarbleWorth = newMarbleValue + scoreNode.Value;
                    marbleList.Remove(scoreNode);
                    playerScores[(int)(newMarbleValue % NumberOfPlayers)] += lastMarbleWorth;
                }
                else
                {
                    currentNode = marbleList.AddAfter(currentNode.Next ?? marbleList.First, newMarbleValue);
                }

                newMarbleValue++;

                //Console.WriteLine(string.Join(' ', marbleList));
            }

            Console.WriteLine(playerScores.Max());
            Console.ReadKey();
        }
    }
}
