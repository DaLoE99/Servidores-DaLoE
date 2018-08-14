namespace BoomBang.Game.Pathfinding
{
    using BoomBang.Specialized;
    using System;

    public static class Rotation
    {
        public static int Calculate(Vector2 PositionOne, Vector2 PositionTwo)
        {
            if ((PositionOne.Int32_0 > PositionTwo.Int32_0) && (PositionOne.Int32_1 > PositionTwo.Int32_1))
            {
                return 2;
            }
            if ((PositionOne.Int32_0 < PositionTwo.Int32_0) && (PositionOne.Int32_1 < PositionTwo.Int32_1))
            {
                return 1;
            }
            if ((PositionOne.Int32_0 > PositionTwo.Int32_0) && (PositionOne.Int32_1 < PositionTwo.Int32_1))
            {
                return 4;
            }
            if ((PositionOne.Int32_0 < PositionTwo.Int32_0) && (PositionOne.Int32_1 > PositionTwo.Int32_1))
            {
                return 3;
            }
            if (PositionOne.Int32_0 > PositionTwo.Int32_0)
            {
                return 8;
            }
            if (PositionOne.Int32_0 < PositionTwo.Int32_0)
            {
                return 5;
            }
            if (PositionOne.Int32_1 < PositionTwo.Int32_1)
            {
                return 7;
            }
            if (PositionOne.Int32_1 > PositionTwo.Int32_1)
            {
                return 6;
            }
            return 0;
        }
    }
}

