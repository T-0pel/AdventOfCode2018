using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary;

namespace Day8
{
    class Program
    {
        private static List<Node> _nodes = new List<Node>();

        static void Main(string[] args)
        {
            var lines = FileHelper.GetLines("Day8").ToList();
            var split = lines[0].Split(' ');
            ParseNodeData(split, null);
            Console.WriteLine(_nodes.Sum(n => n.Metadata.Sum()));
            Console.WriteLine(_nodes[0].GetNodeValue());
            Console.ReadKey();
        }

        private static int ParseNodeData(IReadOnlyList<string> data, Node parentNode)
        {
            var node = new Node();
            _nodes.Add(node);

            parentNode?.ChildNodes.Add(node);

            var childNodesCount = int.Parse(data[0]);
            var metadataCount = int.Parse(data[1]);

            var metadataIndex = 2;
            for (var i = 0; i < childNodesCount; i++)
            {
                metadataIndex += ParseNodeData(data.TakeLast(data.Count - metadataIndex).ToArray(), node);
            }

            for (var i = 0; i < metadataCount; i++)
            {
                node.Metadata.Add(int.Parse(data[metadataIndex++]));
            }

            return metadataIndex;
        }
    }
}
