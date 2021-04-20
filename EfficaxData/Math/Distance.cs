using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Math
{
    public static class Distance
    {
        public static double GetDistance(PositionData pos1, PositionData pos2)
        {
            return System.Math.Sqrt((GetDistanceSquared(pos1, pos2)));
        }
        public static float GetDistanceSquared(PositionData pos1, PositionData pos2)
        {
            return ((pos1.x - pos2.x) * (pos1.x - pos2.x) + (pos1.y - pos2.y) * (pos1.y - pos2.y));
        }
    }
}
