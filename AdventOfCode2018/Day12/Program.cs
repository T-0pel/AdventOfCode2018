using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        private const long Generations = 50000000000;

        static void Main(string[] args)
        {
            var lines = FileHelper.GetLines("Day12").ToList();

            var pots = new LinkedList<Pot>();

            var potsList = lines[0].Split(' ')[2].Select(ch => new Pot { HasPlant = ch == '#' });

            foreach (var pot in potsList)
            {
                pots.AddLast(pot);
            }
            CheckIfNeedAdditionalNodes(pots);

            lines.RemoveRange(0, 2);
            var rules = lines.Select(l => new PotRule(l, false)).ToList();
            //rules.AddRange(lines.Select(l => new PotRule(l, true)).ToList());

            var totalPotNumbersWithPlants = 0;
            var additions = new[] { 0, 0, 0 };
            var previousTotal = 0;
            var generationsDone = 0;
            for (long generation = 0; generation <= Generations; generation++)
            {
                var potsCount = pots.Count;
                var currentPot = pots.First;
                while (potsCount > 0)
                {
                    foreach (var potRule in rules)
                    {
                        if (currentPot.Value.HasPlant == potRule.Rules[2] &&
                            (currentPot.Previous?.Value.HasPlant ?? false) == potRule.Rules[1] &&
                            (currentPot.Previous?.Previous?.Value.HasPlant ?? false) == potRule.Rules[0] &&
                            (currentPot.Next?.Value.HasPlant ?? false) == potRule.Rules[3] &&
                            (currentPot.Next?.Next?.Value.HasPlant ?? false) == potRule.Rules[4])
                        {
                            currentPot.Value.HasPlantNext = potRule.Result;
                            break;
                        }
                        else
                        {
                            currentPot.Value.HasPlantNext = false;
                        }
                    }

                    currentPot = currentPot.Next;
                    potsCount--;
                }

                var newTotal = pots.Where(p => p.HasPlant).Sum(p => p.PotNumber);
                var addition = newTotal - previousTotal;
                additions[generation % 3] = addition;
                if (additions.Distinct().Count() == 1) break;
                totalPotNumbersWithPlants = previousTotal = newTotal;
                //Console.Write($"{generation}: ");
                //pots.ToImmutableList().Select(p => p.HasPlant ? '#' : '.').ToList().ForEach(i => Console.Write("{0}", i));
                //Console.WriteLine();
                foreach (var pot in pots)
                {
                    pot.HasPlant = pot.HasPlantNext;
                }
                CheckIfNeedAdditionalNodes(pots);
                generationsDone++;
            }

            var result = totalPotNumbersWithPlants + (Generations - generationsDone + 1) * additions[0];

            //totalPlants = pots.Where(p => p.HasPlant).Sum(p => p.PotNumber);
            Console.WriteLine(result);
            Console.ReadKey();
        }


        private static void CheckIfNeedAdditionalNodes(LinkedList<Pot> pots)
        {
            if (pots.First.Value.HasPlant || pots.First.Next.Value.HasPlant)
            {
                pots.AddFirst(new Pot { PotNumber = pots.First.Value.PotNumber - 1 });
                pots.AddFirst(new Pot { PotNumber = pots.First.Value.PotNumber - 1 });
            }

            if (pots.Last.Value.HasPlant || pots.Last.Previous.Value.HasPlant)
            {
                pots.AddLast(new Pot { PotNumber = pots.Last.Value.PotNumber + 1 });
                pots.AddLast(new Pot { PotNumber = pots.Last.Value.PotNumber + 1 });
            }
        }
    }
}
