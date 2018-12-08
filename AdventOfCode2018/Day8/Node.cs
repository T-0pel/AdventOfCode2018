using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    public class Node
    {
        public int Id { get; }
        public List<Node> ChildNodes { get; set; }
        public List<int> Metadata { get; set; }

        private static int _idCounter;

        public Node()
        {
            Id = ++_idCounter;
            ChildNodes = new List<Node>();
            Metadata = new List<int>();
        }

        public int GetNodeValue()
        {
            var value = 0;
            if (ChildNodes.Count == 0)
            {
                value = Metadata.Sum();
            }
            else
            {
                foreach (var meta in Metadata)
                {
                    var index = meta - 1;
                    var node = ChildNodes.ElementAtOrDefault(index);
                    if (node != null)
                    {
                        value += node.GetNodeValue();
                    }
                }
            }

            return value;
        }
    }
}