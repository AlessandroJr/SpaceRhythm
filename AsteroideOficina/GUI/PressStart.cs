using AsteroideOficina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroideOficina.GUI
{
    public class PressStart : DrawableGameComponent
    {
        public SpriteFont Fonte { get; set; }
        public float TempoPiscadinha { get; set; } = 1f;

        public PressStart(Game game) : base(game)
        {
            game.Components.Add(this);
            Fonte = game.Content.Load<SpriteFont>("fonts/arial20");

        }

        public override void Update(GameTime gameTime)
        {
            if (Globals.GetPlayer<Player>().Enabled)
            {
                Visible = false;
                return;
            }

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Visible = TempoPiscadinha > 0.5f;

            TempoPiscadinha -= dt;
            if (TempoPiscadinha < 0)
                TempoPiscadinha = 1f;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                (Game as Main).IniciarJogo();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var msg = "Press ENTER";
            var size = Fonte.MeasureString(msg);
            var pos = new Vector2(Game.Window.ClientBounds.Width / 2 - size.X/2, Game.Window.ClientBounds.Height / 2 - size.Y/2);
            Globals.SpriteBatch.DrawString(Fonte, msg, pos, Color.White);
        }

        
    }
}
