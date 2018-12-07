using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Program
    {
        private static List<Step> _steps;
        private static string _correctOrder;
        private static HashSet<string> _blockedStepsCodes = new HashSet<string>();

        static void Main(string[] args)
        {
            var lines = FileHelper.GetLines("Day7");
            _steps = new List<Step>();
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var stepCode = split[1];
                var step = _steps.FirstOrDefault(s => s.LetterCode == stepCode);
                if (step == null)
                {
                    step = new Step { LetterCode = stepCode };
                    _steps.Add(step);
                }

                var prerequisiteCode = split[7];
                var prerequisiteStep = _steps.FirstOrDefault(s => s.LetterCode == prerequisiteCode);
                if (prerequisiteStep == null)
                {
                    prerequisiteStep = new Step { LetterCode = prerequisiteCode };
                    _steps.Add(prerequisiteStep);
                }

                prerequisiteStep.PrerequisiteSteps.Add(step);
                step.NextSteps.Add(prerequisiteStep);
            }

            GetCorrectOrder(_steps.First(s => s.PrerequisiteSteps.Count == 0));
            Console.WriteLine(_correctOrder);
            Console.ReadKey();
        }

        private static void GetCorrectOrder(Step step)
        {
            _correctOrder += step.LetterCode;
            step.IsDone = true;
            _blockedStepsCodes.Remove(step.LetterCode);

            var blockedSteps = step.NextSteps.Where(s => s.PrerequisiteSteps.Count(ps => ps.IsDone) != s.PrerequisiteSteps.Count);
            foreach (var blockedStep in blockedSteps)
            {
                _blockedStepsCodes.Add(blockedStep.LetterCode);
            }

            foreach (var nextStep in step.NextSteps.Where(s => s.PrerequisiteSteps.Count(ps => ps.IsDone) == s.PrerequisiteSteps.Count).OrderBy(s => s.LetterCode))
            {
                GetCorrectOrder(nextStep);
            }

            foreach (var blockedStepsCode in _blockedStepsCodes)
            {
                GetCorrectOrder(_steps.First(s => s.LetterCode == blockedStepsCode));
            }
        }
    }
}
