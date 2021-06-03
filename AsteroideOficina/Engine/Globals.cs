using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroideOficina.Engine
{
    public class Globals
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        public static Random Random = new Random();
        public static IPlayer Player;

        public static T GetPlayer<T>()
        {
            return (T)Player;
        }
    }
}
