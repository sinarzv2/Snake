using Domain.Enum;

namespace Domain.Model
{
    public class Move
    {
        public Move(DirectionType direction)
        {
            Direction = direction;
        }

        public DirectionType Direction { get; set; }
    }
}
