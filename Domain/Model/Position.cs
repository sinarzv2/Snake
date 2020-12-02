using System;
using Domain.Enum;

namespace Domain.Model
{
    public class Position
    {
        public Position()
        {
            PositionState = PositionState.Empty;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public PositionState PositionState { get; set; }

        public int? Order { get; private set; }

        public void SetOrder(int? i)
        {
            if (PositionState != PositionState.Snake)
                throw new Exception("Set Order for not snake");
            Order = i;
        }
    }
}
