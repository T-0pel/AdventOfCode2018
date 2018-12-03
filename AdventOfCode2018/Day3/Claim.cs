namespace Day3
{
    public class Claim
    {
        public int Id { get; private set; }
        public int LeftEdge { get; private set; }
        public int TopEdge { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

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