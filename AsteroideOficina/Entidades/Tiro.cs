using AsteroideOficina.Engine;
using AsteroideOficina.Engine.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroideOficina.Entidades
{
    public class Tiro : DrawableGameComponent
    {
        public Texture2D Textura { get; set; }
        public Vector2 Direcao { get; set; }
        public Vector2 Posicao { get; set; }
        public float Velocidade { get; set; }

        public Tiro(Game game) : base(game)
        {
            game.Components.Add(this);
            Textura = game.Content.Load<Texture2D>("2d/tiro");
        }

        public override void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Posicao -= Direcao * Velocidade * dt;

            if (Posicao.X < 0)
                Game.Components.Remove(this);

            if (Posicao.X > Game.Window.ClientBounds.Width)
                Game.Components.Remove(this);

            if (Posicao.Y < 0)
                Game.Components.Remove(this);

            if (Posicao.Y > Game.Window.ClientBounds.Height)
                Game.Components.Remove(this);

            var meteoros = Game.Components.OfType<Meteoro>();

            foreach (var m in meteoros)
            {
                var dist = Vector2.Distance(m.Posicao, Posicao);
                if (dist < m.Raio)
                {
                    Globals.GetPlayer<Player>().Pontos++;
                    Game.Components.Remove(this);
                    m.Explode();
                    break;
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            Globals.SpriteBatch.Draw(Textura, Posicao, null, Color.White, -Direcao.Angulo(), new Vector2(Textura.Width / 2, Textura.Height / 2), 1f, SpriteEffects.None, 0);
        }
    }
}
