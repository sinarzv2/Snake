using System.Collections.Generic;
using Domain.Core;
using Domain.Enum;

namespace Domain.Model
{
    public class Game
    {
        public Game()
        {
            Board = new Board();
            CurrentDirection = DirectionType.Right;
            Moves = new HashSet<Move>();
        }
        public Board Board { get; set; }
        public HashSet<Move> Moves { get; private set; }
        public DirectionType CurrentDirection { get; private set; }
        public OperationResult AddMove(Move move)
        {
            var fork = Board.MoveSnake(CurrentDirection);
            if (fork.Success)
            {
                var moveAdded = Moves.Add(move);
                return moveAdded ? OperationResult.BuildSuccess() : OperationResult.BuildFailure(ErrorType.MoveAlreadyExsited);
            }
            return OperationResult.BuildFailure(fork.ErrorMessage);
        }

        public void ChangeDirection(DirectionType direction)
        {
            CurrentDirection = direction;
        }
    }
}
