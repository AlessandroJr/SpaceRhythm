using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroideOficina.Engine.Extension
{
    public static class Vector2Extension
    {
        public static Vector2 Rotate(this Vector2 v, float rad, Vector2 origem)
        {
            var dx = v.X - origem.X;
            var dy = v.Y - origem.Y;
            var x = dx * MathF.Cos(rad) + dy * MathF.Sin(rad);
            var y = -dx * MathF.Sin(rad) + dy * MathF.Cos(rad);
            return new Vector2(x + origem.X, y + origem.Y);
        }

        public static Vector2 Rotate(this Vector2 v, float rad)
        {
            return v.Rotate(rad, Vector2.Zero);
        }

        public static float Angulo(this Vector2 v)
        {
            var a = MathHelper.TwoPi - (MathF.Atan2(v.X, v.Y) * -1);
            if (v.X < 0)
                return a;
            return MathF.Atan2(v.X, v.Y);
        }
    }
}
