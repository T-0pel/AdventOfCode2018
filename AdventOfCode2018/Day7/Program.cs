using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Day7
{
    class Program
    {
        private static List<Step> _steps;
        private static string _correctOrder;
        private static int _freeWorkers = 5;

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
                    step = new Step(stepCode);
                    _steps.Add(step);
                }

                var prerequisiteCode = split[7];
                var prerequisiteStep = _steps.FirstOrDefault(s => s.LetterCode == prerequisiteCode);
                if (prerequisiteStep == null)
                {
                    prerequisiteStep = new Step(prerequisiteCode);
                    _steps.Add(prerequisiteStep);
                }

                prerequisiteStep.PrerequisiteSteps.Add(step);
                step.NextSteps.Add(prerequisiteStep);
            }

            var isDone = false;
            var timeElapsed = -1;
            while (!isDone)
            {
                timeElapsed++;
                if (_steps.Count(s => s.IsDone) != _steps.Count)
                {
                    var workers = _freeWorkers;
                    for (var i = 0; i < workers; i++)
                    {
                        var stepToDo = _steps.Where(s => !s.IsDone && !s.IsWorkedOn && s.PrerequisiteSteps.Count(ps => ps.IsDone) == s.PrerequisiteSteps.Count).OrderBy(s => s.LetterCode)
                            .FirstOrDefault();

                        if (stepToDo != null)
                        {
                            stepToDo.IsWorkedOn = true;
                            _freeWorkers--;
                        }
                    }

                    var stepsInProgress = _steps.Where(s => !s.IsDone && s.IsWorkedOn);
                    foreach (var step in stepsInProgress)
                    {
                        step.TimeRemaining--;
                        if (step.TimeRemaining == 0)
                        {
                            step.IsDone = true;
                            step.IsWorkedOn = false;
                            _freeWorkers++;
                            _correctOrder += step.LetterCode;
                        }
                    }
                }
                else
                {
                    isDone = true;
                }
            }

            Console.WriteLine(_correctOrder);
            Console.WriteLine(timeElapsed);
            Console.ReadKey();
        }

        private static void ExecuteTask(string stepToDoCode)
        {
            var stepToDo = _steps.First(s => s.LetterCode == stepToDoCode);

            Thread.Sleep(stepToDo.Length);

            _correctOrder += stepToDo.LetterCode;
            stepToDo.IsDone = true;
            _freeWorkers--;
        }

        private static void SomeonesSolution()
        {
            var dependencies = new List<(string pre, string post)>();

            var lines = FileHelper.GetLines("Day7");
            lines.ToList().ForEach(x => dependencies.Add((x.Split(' ').ElementAt(1), x.Split(' ').ElementAt(7))));

            var allSteps = dependencies.Select(x => x.pre).Concat(dependencies.Select(x => x.post)).Distinct().OrderBy(x => x).ToList();
            var result = string.Empty;

            while (allSteps.Any())
            {
                var valid = allSteps.First(s => !dependencies.Any(d => d.post == s));

                _correctOrder += valid;

                allSteps.Remove(valid);
                dependencies.RemoveAll(d => d.pre == valid);
            }
        }

        private static void GetCorrectOrder(Step step)
        {
            if (!step.IsDone)
            {
                _correctOrder += step.LetterCode;
                step.IsDone = true;
            }

            var possibleNextSteps = step.NextSteps.Where(s => !s.IsDone && s.PrerequisiteSteps.Count(ps => ps.IsDone) == s.PrerequisiteSteps.Count)
                .OrderBy(s => s.LetterCode).ToList();

            if (possibleNextSteps.Any())
            {
                GetCorrectOrder(possibleNextSteps.First());
            }
        }
    }
}
