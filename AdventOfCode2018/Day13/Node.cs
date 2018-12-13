using System;

namespace Day13
{
    public class Node
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char TrackType { get; set; }
        public CartNode CartNode { get; set; }

        public void SetNewCartDirection(CartNode cartNode)
        {
            CartNode = cartNode;
            if (TrackType == '|' || TrackType == '-')
            {
                return;
            }
            else if (TrackType == '/')
            {
                switch (cartNode.Direction)
                {
                    case Direction.Up:
                        cartNode.Direction = Direction.Right;
                        break;
                    case Direction.Down:
                        cartNode.Direction = Direction.Left;
                        break;
                    case Direction.Left:
                        cartNode.Direction = Direction.Down;
                        break;
                    case Direction.Right:
                        cartNode.Direction = Direction.Up;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (TrackType == '\\')
            {
                switch (cartNode.Direction)
                {
                    case Direction.Up:
                        cartNode.Direction = Direction.Left;
                        break;
                    case Direction.Down:
                        cartNode.Direction = Direction.Right;
                        break;
                    case Direction.Left:
                        cartNode.Direction = Direction.Up;
                        break;
                    case Direction.Right:
                        cartNode.Direction = Direction.Down;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (TrackType == '+')
            {
                switch (cartNode.Direction)
                {
                    case Direction.Up:
                        switch (cartNode.TurnDirection)
                        {
                            case TurnDirection.Left:
                                cartNode.Direction = Direction.Left;
                                break;
                            case TurnDirection.Straight:
                                break;
                            case TurnDirection.Right:
                                cartNode.Direction = Direction.Right;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case Direction.Down:
                        switch (cartNode.TurnDirection)
                        {
                            case TurnDirection.Left:
                                cartNode.Direction = Direction.Right;
                                break;
                            case TurnDirection.Straight:
                                break;
                            case TurnDirection.Right:
                                cartNode.Direction = Direction.Left;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case Direction.Left:
                        switch (cartNode.TurnDirection)
                        {
                            case TurnDirection.Left:
                                cartNode.Direction = Direction.Down;
                                break;
                            case TurnDirection.Straight:
                                break;
                            case TurnDirection.Right:
                                cartNode.Direction = Direction.Up;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case Direction.Right:
                        switch (cartNode.TurnDirection)
                        {
                            case TurnDirection.Left:
                                cartNode.Direction = Direction.Up;
                                break;
                            case TurnDirection.Straight:
                                break;
                            case TurnDirection.Right:
                                cartNode.Direction = Direction.Down;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if ((int)cartNode.TurnDirection + 1 > 2)
                {
                    cartNode.TurnDirection = TurnDirection.Left;
                }
                else
                {
                    cartNode.TurnDirection++;
                }
            }
        }
    }
}