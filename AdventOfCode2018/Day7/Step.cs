using System.Collections.Generic;

namespace Day7
{
    public class Step
    {
        public string LetterCode { get; set; }
        public List<Step> PrerequisiteSteps { get; set; }
        public List<Step> NextSteps { get; set; }
        public bool IsDone { get; set; }

        public Step()
        {
            PrerequisiteSteps = new List<Step>();
            NextSteps = new List<Step>();
        }
    }
}