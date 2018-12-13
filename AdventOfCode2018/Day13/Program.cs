using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = FileHelper.GetLines("Day13").ToList();
            var gridWidth = lines[0].Length;
            var gridHeight = lines.Count;
            var cartCharacters = new HashSet<char> {'>', '<', 'v', '^'};
            var cartNodes = new List<CartNode>();
            var grid = new Node[gridWidth, gridHeight];

            for (var x = 0; x < gridWidth; x++)
            {
                for (var y = 0; y < gridHeight; y++)
                {
                    var character = lines[y][x];
                    var nodeCharacter = character;
                    if (character == '>' || character == '<')
                    {
                        nodeCharacter = '-';
                    }
                    else if (character == '^' || character == 'v')
                    {
                        nodeCharacter = '|';
                    }
                    var node = new Node {PositionX = x, PositionY = y, TrackType = nodeCharacter};
                    grid[x, y] = node;

                    if (cartCharacters.Contains(character))
                    {
                        var cartNode = new CartNode(node);
                        node.CartNode = cartNode;
                        cartNodes.Add(cartNode);
                        switch (character)
                        {
                            case '>':
                                cartNode.Direction = Direction.Right;
                                break;
                            case '<':
                                cartNode.Direction = Direction.Left;
                                break;
                            case 'v':
                                cartNode.Direction = Direction.Down;
                                break;
                            case '^':
                                cartNode.Direction = Direction.Up;
                                break;
                        }
                    }
                }
            }

            Node crashNode = null;
            while (cartNodes.Count > 1)
            {
                var orderedCarts = cartNodes.OrderBy(n => n.Node.PositionY).ThenBy(n => n.Node.PositionX);
                foreach (var cart in orderedCarts)
                {
                    if (cart.IsDestroyed) continue;

                    Node nextNode;
                    switch (cart.Direction)
                    {
                        case Direction.Up:
                            nextNode = grid[cart.Node.PositionX, cart.Node.PositionY - 1];
                            break;
                        case Direction.Down:
                            nextNode = grid[cart.Node.PositionX, cart.Node.PositionY + 1];
                            break;
                        case Direction.Left:
                            nextNode = grid[cart.Node.PositionX - 1, cart.Node.PositionY];
                            break;
                        case Direction.Right:
                            nextNode = grid[cart.Node.PositionX + 1, cart.Node.PositionY];
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (nextNode.CartNode != null)
                    {
                        //crashNode = nextNode;
                        cart.Node.CartNode = null;
                        cart.IsDestroyed = true;
                        cartNodes.Remove(cart);

                        cartNodes.Remove(nextNode.CartNode);
                        nextNode.CartNode.IsDestroyed = true;
                        nextNode.CartNode = null;
                    }
                    else
                    {
                        cart.Node.CartNode = null;
                        cart.Node = nextNode;
                        nextNode.SetNewCartDirection(cart);
                    }
                    //PrintGrid(grid, gridWidth, gridHeight);
                }
            }

            Console.WriteLine($"{cartNodes[0].Node.PositionX},{cartNodes[0].Node.PositionY}");
            Console.ReadKey();
        }

        private static void PrintGrid(Node[,] grid, int gridWidth, int gridHeight)
        {
            for (var y = 0; y < gridHeight; y++)
            {
                for (var x = 0; x < gridWidth; x++)
                {
                    var node = grid[x, y];
                    Console.Write(node.CartNode?.GetDirectionSign() ?? node.TrackType);
                }
                Console.WriteLine();
            }
        }
    }
}
