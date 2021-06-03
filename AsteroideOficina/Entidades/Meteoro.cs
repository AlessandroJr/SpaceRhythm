using AsteroideOficina.Engine;
using AsteroideOficina.Engine.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroideOficina.Entidades
{
    public enum EnumTipo
    {
        Meteorao,
        Meteoro,
        Meteorinho
    }

    public class Meteoro : DrawableGameComponent
    {
        public Texture2D Textura { get; set; }
        public Vector2 Posicao { get; set; }
        public float Rotacao { get; set; }
        public float Velocidade { get; set; }
        public Vector2 Inercia { get; set; }
        private Vector2 _direcao = Vector2.One;

        public EnumTipo Tipo { get; set; }

        public float Raio { get { return Textura.Width / 2; } }

        public Meteoro(Game game, EnumTipo tipo)
            : base(game)
        {
            game.Components.Add(this);
            Tipo = tipo;

            if (tipo == EnumTipo.Meteorao)
                Textura = game.Content.Load<Texture2D>("2d/meteorao");

            if (tipo == EnumTipo.Meteoro)
                Textura = game.Content.Load<Texture2D>("2d/meteoro");

            if (tipo == EnumTipo.Meteorinho)
                Textura = game.Content.Load<Texture2D>("2d/meteorinho");
        }        

        public override void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Posicao += Inercia * Velocidade * dt;

            if (Posicao.X < 0)
                Posicao = new Vector2(Game.Window.ClientBounds.Width - 1, Posicao.Y);

            if (Posicao.X > Game.Window.ClientBounds.Width)
                Posicao = new Vector2(0, Posicao.Y);

            if (Posicao.Y < 0)
                Posicao = new Vector2(Posicao.X, Game.Window.ClientBounds.Height - 1);

            if (Posicao.Y > Game.Window.ClientBounds.Height)
                Posicao = new Vector2(Posicao.X, 0);

            _direcao = _direcao.Rotate(MathHelper.ToRadians(Rotacao));
        }

        internal void Explode()
        {
            Game.Components.Remove(this);
            switch (Tipo)
            {
                case EnumTipo.Meteorao: (Game as Main).Gerador.Gerar(2, EnumTipo.Meteoro, Posicao); break;
                case EnumTipo.Meteoro: (Game as Main).Gerador.Gerar(2, EnumTipo.Meteorinho, Posicao); break;
                default:break;
            }

        }

        public override void Draw(GameTime gameTime)
        {
            Globals.SpriteBatch.Draw(Textura, Posicao, null, Color.White, -_direcao.Angulo(), new Vector2(Textura.Width / 2, Textura.Height / 2), 1f, SpriteEffects.None, 0);
        }
    }
}
