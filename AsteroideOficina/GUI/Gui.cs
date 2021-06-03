using AsteroideOficina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroideOficina.GUI
{
    public class Gui : DrawableGameComponent
    {
        public SpriteFont Fonte { get; set; }

        public Gui(Game game) : base(game)
        {
            game.Components.Add(this);

            Fonte = game.Content.Load<SpriteFont>("fonts/arial20");

            new PressStart(game);
        }

        public override void Draw(GameTime gameTime)
        {
            var pontos = $"Pontos:{Globals.GetPlayer<Player>().Pontos}";
            Globals.SpriteBatch.DrawString(Fonte, pontos, new Vector2(10, 10), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
         
        }
    }
}
