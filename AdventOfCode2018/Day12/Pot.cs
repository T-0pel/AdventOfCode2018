namespace Day12
{
    public class Pot
    {
        public bool HasPlant { get; set; }
        public bool HasPlantNext { get; set; }
        public int PotNumber { get; set; }

        private static int _counter;

        public Pot()
        {
            PotNumber = _counter++;
        }
    }
}