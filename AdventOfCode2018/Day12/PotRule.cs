namespace Day12
{
    public class PotRule
    {
        public bool[] Rules { get; set; }
        public bool Result { get; set; }

        public PotRule(string line, bool reverse)
        {
            var first = line[0] == '#' ? !reverse : reverse;
            var second = line[1] == '#' ? !reverse : reverse;
            var third = line[2] == '#' ? !reverse : reverse;
            var fourth = line[3] == '#' ? !reverse : reverse;
            var fifth = line[4] == '#' ? !reverse : reverse;
            Rules = new[] { first, second, third, fourth, fifth };

            Result = line[9] == '#' ? !reverse : reverse; ;
        }
    }
}