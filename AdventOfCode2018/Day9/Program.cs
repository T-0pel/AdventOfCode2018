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
            long lastMarbleWorth = 0;
            long newMarbleValue = 1;
            LinkedListNode<long> currentNode = marbleList.First;
            while (LastMarbleWorth > newMarbleValue)
            {
                if (newMarbleValue % 23 == 0)
                {
                    var positionLeft = 7;
                    LinkedListNode<long> scoreNode = currentNode;
                    while(positionLeft > 0)
                    {
                        if (scoreNode?.Previous != null)
                        {
                            scoreNode = scoreNode.Previous;
                        }
                        else
                        {
                            scoreNode = marbleList.Last;
                        }
                        positionLeft--;
                    }

                    currentNode = scoreNode.Next;
                    lastMarbleWorth = newMarbleValue + scoreNode.Value;
                    marbleList.Remove(scoreNode);
                    playerScores[(int)(newMarbleValue % NumberOfPlayers)] += lastMarbleWorth;
                }
                else
                {
                    if (currentNode.Next == null)
                    {
                        currentNode = marbleList.AddAfter(marbleList.First, newMarbleValue);
                    }
                    else
                    {
                        currentNode = marbleList.AddAfter(currentNode.Next, newMarbleValue);
                    }
                }

                newMarbleValue++;

                //Console.WriteLine(string.Join(' ', marbleList));
            }

            Console.WriteLine(playerScores.Max());
            Console.ReadKey();
        }
    }
}
