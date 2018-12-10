namespace Day10
{
    public class Point
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int VelocityX { get; }
        public int VelocityY { get; }

        public Point(string line)
        {
            var spaceSplit = line.Split(',');
            PositionX = int.Parse(spaceSplit[0].Substring(spaceSplit[0].Length - 6, 6).Trim());
            PositionY = int.Parse(spaceSplit[1].Substring(1, 6).Trim());
            VelocityX = int.Parse(spaceSplit[1].Substring(spaceSplit[1].Length - 2, 2).Trim());
            VelocityY = int.Parse(spaceSplit[2].Substring(1, 2).Trim());
        }

        public void Move()
        {
            PositionX += VelocityX;
            PositionY += VelocityY;
        }
    }
}