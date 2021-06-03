using AsteroideOficina.Engine;
using AsteroideOficina.Engine.Extension;
using AsteroideOficina.Entidades;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroideOficina.Geradores
{
    public class GeradorMeteoros : GameComponent
    {
        public float Tempo { get; set; } = 2f;

        public GeradorMeteoros(Game game)
            : base(game)
        {
            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            if(!Globals.GetPlayer<Player>().Enabled)
                return;

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((Tempo -= dt) > 0)
                return;

            Tempo = 2f;

            Gerar(1);
        }

        public void Gerar(int quant, EnumTipo tipo, Vector2 posicao)
        {
            var ang = new MinMax(0, 359);

            for (int i = 0; i < quant; i++)
                Gerar(posicao, tipo, ang.Random());
        }

        public void Gerar(int quant)
        {
            for (int i = 0; i < quant; i++)
                Gerar();
        }

        private void Gerar(Vector2? posOrig = null , EnumTipo? tipo = null, float? ang = null)
        {
            var rot = new MinMax(-1f, 1f).Random();
            var vel = new MinMax(20f, 50f).Random();
            
            Vector2 pos;
            if (posOrig.HasValue)
            {
                pos = posOrig.Value;
            }
            else
            {
                var bordaInfSup = new MinMax(0, Game.Window.ClientBounds.Width).RandomInt();
                var bordaEsqDir = new MinMax(0, Game.Window.ClientBounds.Height).RandomInt();

                var cantos = new MinMax(0, 3).RandomRound();

                switch (cantos)
                {
                    case 0: pos = new Vector2(bordaInfSup, 0); break;
                    case 1: pos = new Vector2(bordaInfSup, Game.Window.ClientBounds.Height); break;
                    case 2: pos = new Vector2(0, bordaEsqDir); break;
                    case 3: pos = new Vector2(Game.Window.ClientBounds.Width, bordaEsqDir); break;
                    default: pos = Vector2.Zero; break;
                }
            }

            var values = Enum.GetValues(typeof(EnumTipo));
            var index = new MinMax(0, values.Length - 1).RandomRound();
            var tipoM = (EnumTipo)values.GetValue(index);

            var inercia = GeradorInercia(pos, Globals.GetPlayer<Player>().Posicao);
            if (ang.HasValue)
                inercia = inercia.Rotate(MathHelper.ToRadians(ang.Value));

            new Meteoro(Game, tipo ?? tipoM)
            {
                Posicao = new Vector2(pos.X, pos.Y),
                Rotacao = rot,
                Velocidade = vel,
                Inercia = inercia
            };
        }

        private Vector2 GeradorInercia(Vector2 pos, Vector2 plPos)
        {
            var dx = plPos.X - pos.X;
            var dy = plPos.Y - pos.Y;
            var ine = new Vector2(dx, dy);
            ine.Normalize();
            return ine;
        }

        
    }
}
