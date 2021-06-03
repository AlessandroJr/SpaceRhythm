using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroideOficina.Engine
{
    public class MinMax
    {
        public float Min { get; set; }
        public float Max { get; set; }

        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Random()
        {
            return Min + (float)Globals.Random.NextDouble() * (Max - Min);
        }

        public int RandomInt()
        {
            return (int)Random();
        }

        public int RandomRound()
        {
            return (int)MathF.Round(Random());
        }
    }
}
