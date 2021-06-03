using AsteroideOficina.Engine;
using AsteroideOficina.Entidades;
using AsteroideOficina.Geradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace AsteroideOficina
{
    public class Main : Game
    {
        public GeradorMeteoros Gerador { get; set; }

        public Main()
        {
            Globals.Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            Globals.Graphics.PreferredBackBufferWidth = 1024;
            Globals.Graphics.PreferredBackBufferHeight = 768;
            
            Globals.Graphics.ApplyChanges();

            IniciarJogo();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        internal void IniciarJogo()
        {
            Components.Clear();

            Gerador = new GeradorMeteoros(this);
            new GUI.Gui(this);

            var pl = new Player(this);
            pl.Enabled = true;
            pl.Visible = true;
            Globals.Player = pl;

            Gerador.Gerar(10);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            Globals.SpriteBatch.Begin();
            base.Draw(gameTime);
            Globals.SpriteBatch.End();
        }
    }
}
