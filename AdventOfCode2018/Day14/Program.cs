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
            //FasterSolution();

            var puzzleInputString = puzzleInput.ToString();
            var recipesList = new List<int> {3,7};
            var workerIndex1 = 0;
            var workerIndex2 = 1;

            var potentialOccurrences = new List<string>();

            while (!potentialOccurrences.Contains(puzzleInputString))
            {
                var workerValue1 = recipesList[workerIndex1];
                var workerValue2 = recipesList[workerIndex2];
                var total = workerValue1 + workerValue2;
                var totalString = total.ToString();

                foreach (var number in totalString)
                {
                    recipesList.Add(int.Parse(number.ToString()));

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

                workerIndex1 = (workerIndex1 + workerValue1 + 1) % recipesList.Count;
                workerIndex2 = (workerIndex2 + workerValue2 + 1) % recipesList.Count;

                //recipesList.ToList().ForEach(Console.Write);
                //Console.WriteLine();
            }

            Console.WriteLine(recipesList.Count() - puzzleInput.ToString().Length);
            Console.ReadKey();
        }

        public static void FasterSolution()
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
