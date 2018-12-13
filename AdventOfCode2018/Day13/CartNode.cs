using System;

namespace Day13
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum TurnDirection
    {
        Left,
        Straight,
        Right
    }

    public class CartNode
    {
        public Node Node { get; set; }
        public Direction Direction { get; set; }
        public TurnDirection TurnDirection { get; set; } = TurnDirection.Left;
        public bool IsDestroyed { get; set; }

        public CartNode(Node node)
        {
            Node = node;
        }

        public char GetDirectionSign()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return '^';
                case Direction.Down:
                    return 'v';
                case Direction.Left:
                    return '<';
                case Direction.Right:
                    return '>';
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}