using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {
        private const int puzzleInput = 430971;

        static void Main(string[] args)
        {
            //Day14Part2();

            var puzzleInputString = puzzleInput.ToString();
            var recipesList = new LinkedList<int>();
            recipesList.AddFirst(3);
            recipesList.AddLast(7);
            var workerNodes = new List<LinkedListNode<int>> { recipesList.First, recipesList.Last };

            var potentialOccurrences = new List<string>();

            while (!potentialOccurrences.Contains(puzzleInputString))
            {
                var total = workerNodes[0].Value + workerNodes[1].Value;
                var totalString = total.ToString();

                foreach (var number in totalString)
                {
                    recipesList.AddLast(int.Parse(number.ToString()));

                    for (var index = potentialOccurrences.Count - 1; index >= 0; index--)
                    {
                        var potentialOccurrence = potentialOccurrences[index];
                        potentialOccurrence += number;
                        if (!puzzleInputString.Contains(potentialOccurrence))
                        {
                            potentialOccurrences.RemoveAt(index);
                        }
                        else
                        {
                            potentialOccurrences[index] = potentialOccurrence;
                        }
                    }

                    if (number == puzzleInputString[0])
                    {
                        potentialOccurrences.Add(number.ToString());
                    }

                    if (potentialOccurrences.Contains(puzzleInputString))
                    {
                        break;
                    }
                }

                for (var index = 0; index < workerNodes.Count; index++)
                {
                    var workerNode = workerNodes[index];
                    var iterations = 1 + workerNode.Value;
                    for (var i = 0; i < iterations; i++)
                    {
                        if (workerNode.Next != null)
                        {
                            workerNodes[index] = workerNode = workerNode.Next;
                        }
                        else
                        {
                            workerNodes[index] = workerNode = recipesList.First;
                        }
                    }
                }

                //recipesList.ToList().ForEach(Console.Write);
                //Console.WriteLine();
            }

            Console.WriteLine(recipesList.Count() - puzzleInput.ToString().Length);
            Console.ReadKey();
        }

        public static void Day14Part2()
        {
            int[] numbersToCheck = new int[] { 4,3,0,9,7,1 };
            int index = 0;
            int positionToCheck = 0;
            bool notFound = true;
            List<int> numbers = new List<int> { 3, 7 };
            int currentRecipe1 = 0;
            int currentRecipe2 = 1;
            while (notFound)
            {
                int recipe1 = numbers[currentRecipe1];
                int recipe2 = numbers[currentRecipe2];
                int sum = recipe1 + recipe2;
                if (sum < 10)
                {
                    numbers.Add(sum);
                }
                else
                {
                    numbers.Add(1);
                    numbers.Add(sum - 10);
                }

                currentRecipe1 = (currentRecipe1 + 1 + recipe1) % numbers.Count;
                currentRecipe2 = (currentRecipe2 + 1 + recipe2) % numbers.Count;

                while (index + positionToCheck < numbers.Count)
                {
                    if (numbersToCheck[positionToCheck] == numbers[index + positionToCheck])
                    {
                        if (positionToCheck == numbersToCheck.Length - 1)
                        {
                            notFound = false;
                            break;
                        }
                        positionToCheck++;
                    }
                    else
                    {
                        positionToCheck = 0;
                        index++;
                    }
                }
            }

            Console.WriteLine(index.ToString());
            Console.ReadKey();
        }
    }
}
