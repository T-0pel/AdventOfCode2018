namespace Day3
{
    public class Claim
    {
        public int Id { get; }
        public int LeftEdge { get; }
        public int TopEdge { get; }
        public int Width { get; }
        public int Height { get; }

        public Claim(string line)
        {
            var spaceSplit = line.Split(' ');
            Id = int.Parse(spaceSplit[0].Substring(1));

            var dashSplit = spaceSplit[2].Split(',');
            LeftEdge = int.Parse(dashSplit[0]);
            TopEdge = int.Parse(dashSplit[1].Substring(0, dashSplit[1].Length - 1));

            var xSplit = spaceSplit[3].Split('x');
            Width = int.Parse(xSplit[0]);
            Height = int.Parse(xSplit[1]);
        }
    }
}