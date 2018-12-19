using System;
using System.Collections.Generic;

namespace Day15
{
    public class Pathfiniding
    {
        public static int FindPathLength(Vector2d avatarPosition, Vector2d targetPosition, Node[,] grid)
        {

            List<GridNode> openNodes = new List<GridNode>();
            List<GridNode> closedNodes = new List<GridNode>();

            GridNode startNode = new GridNode(avatarPosition);
            GridNode targetNode = new GridNode(targetPosition);
            openNodes.Add(startNode);

            while (openNodes.Count != 0)
            {
                // Old search version, priority queue performs much better, almost twice as fast
                GridNode lowestCostNode = openNodes[0];
                for (int i = 1; i < openNodes.Count; i++)
                {
                    GridNode compareNode = openNodes[i];
                    if (compareNode.GetFCost() < lowestCostNode.GetFCost() || (compareNode.GetFCost() == lowestCostNode.GetFCost() && compareNode.hCost < lowestCostNode.hCost))
                    {
                        lowestCostNode = compareNode;
                    }
                }

                openNodes.Remove(lowestCostNode);
                closedNodes.Add(lowestCostNode);

                if (lowestCostNode.position == targetPosition)
                {
                    return calculatePathLength(lowestCostNode);
                }

                for (GridNode neighbour : grid.getNeighbours(lowestCostNode))
                {
                    if (!neighbour.isTraversable || closedNodes.contains(neighbour))
                    {
                        //if (!neighbour.isTraversable){
                        //    System.out.println("You shall not pass!");
                        //}
                        continue;
                    }
                    int newDistance = lowestCostNode.gCost + calculateDistance(lowestCostNode, neighbour);
                    if (neighbour.gCost > newDistance || !openNodes.contains(neighbour))
                    {
                        neighbour.gCost = newDistance;
                        neighbour.hCost = calculateDistance(neighbour, targetNode);
                        neighbour.parent = lowestCostNode;
                        if (!openNodes.contains(neighbour))
                        {
                            openNodes.add(neighbour);
                        }
                    }
                }
            }

            //System.out.println("Target unreachable");
            return 0;
        }

        private static int calculatePathLength(GridNode endNode)
        {
            int pathLength = 0;
            GridNode currentNode = endNode;
            while (currentNode.parent != null)
            {
                pathLength++;
                currentNode = currentNode.parent;
            }
            return pathLength;
        }

        //assuming it is a 4 move grid
        private static int calculateDistance(GridNode node1, GridNode node2)
        {
            int distanceX = Math.Abs(node1.xPosition - node2.xPosition);
            int distanceY = Math.Abs(node1.yPosition - node2.yPosition);

            return distanceX + distanceY;
        }

    }

    public class Vector2d
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        protected bool Equals(Vector2d other)
        {
            return PositionX == other.PositionX && PositionY == other.PositionY;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (PositionX * 397) ^ PositionY;
            }
        }
    }

    public class Node
    {
        public Vector2d Vector { get; set; }
    }

    public class GridNode
    {
        public GridNode parent;
        public int gCost; //distance from starting node
        public int hCost; //distance from end node
        public Vector2d position;
        public bool isTraversable = true;
        public int xPosition;
        public int yPosition;

        public GridNode(Vector2d position)
        {
            this.position = position;
            xPosition = (int)position.PositionX;
            yPosition = (int)position.PositionY;
        }

        public int GetFCost()
        {
            return gCost + hCost;
        }
    }
}