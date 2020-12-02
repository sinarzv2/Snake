using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using Domain.Enum;

namespace Domain.Model
{
    public class Board
    {
        public Board()
        {
            Positions = new List<Position>();
            for (var i = 0; i < 20; i++)
            {
                for (var j = 0; i < 20; i++)
                {
                    Positions.Add(new Position()
                    {
                        PositionState = PositionState.Empty,
                        X = i,
                        Y = j
                    });
                }
            }

            var first = Positions.Single(d => d.X == 0 && d.Y == 0);
            first.PositionState = PositionState.Snake;
            first.SetOrder(3);

            var second = Positions.Single(d => d.X == 1 && d.Y == 0);
            second.PositionState = PositionState.Snake;
            second.SetOrder(2);

            var third = Positions.Single(d => d.X == 2 && d.Y == 0);
            third.PositionState = PositionState.Snake;
            third.SetOrder(1);

            MakeRandomFood();
        }

        internal OperationResult MoveSnake(DirectionType currentDirection)
        {
            var head = Positions.Single(d => d.Order == 1);
            Position newPosition;
            switch (currentDirection)
            {
                case DirectionType.Right:
                    if (head.X == 19)
                        return OperationResult.BuildFailure(ErrorType.BoardPositionNotExist);
                    newPosition = Positions.Single(d => d.Y == head.Y && d.X == head.X + 1);
                    break;

                case DirectionType.Up:
                    if (head.Y == 0)
                        return OperationResult.BuildFailure(ErrorType.BoardPositionNotExist);
                    newPosition = Positions.Single(d => d.Y == head.Y - 1 && d.X == head.X);
                    break;
                case DirectionType.Down:
                    if (head.Y == 19)
                        return OperationResult.BuildFailure(ErrorType.BoardPositionNotExist);
                    newPosition = Positions.Single(d => d.Y == head.Y + 1 && d.X == head.X + 1);
                    break;
                case DirectionType.Left:
                    if (head.X == 0)
                        return OperationResult.BuildFailure(ErrorType.BoardPositionNotExist);
                    newPosition = Positions.Single(d => d.Y == head.Y && d.X == head.X - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null);
            }

            switch (newPosition.PositionState)
            {
                case PositionState.Snake:
                    return OperationResult.BuildFailure(ErrorType.CollisionWithSnake);
                case PositionState.Empty:
                    newPosition.PositionState = PositionState.Snake;
                    newPosition.SetOrder(0);
                    var maxOrder = Positions.Max(d => d.Order);
                    var last = Positions.Single(d => d.Order == maxOrder);
                    last.PositionState = PositionState.Empty;
                    last.SetOrder(null);

                    break;
                case PositionState.Food:
                    newPosition.PositionState = PositionState.Snake;
                    newPosition.SetOrder(0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var snakePosition = Positions.Where(d => d.PositionState == PositionState.Snake);
            foreach (var position in snakePosition)
            {
                position.SetOrder(position.Order + 1);
            }
            return OperationResult.BuildSuccess();
        }

        public IList<Position> Positions { get; }


        public void MakeRandomFood()
        {
            var emtyPosition = Positions.Where(d => d.PositionState == PositionState.Empty).ToList();
            var rnd = new Random();
            var target = rnd.Next(emtyPosition.Count + 1);
            Positions[target].PositionState = PositionState.Food;
        }
    }
}
