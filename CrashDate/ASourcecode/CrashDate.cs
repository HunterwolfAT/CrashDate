#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace CrashDate
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        float HEIGHT = 1080f;
        float WIDTH = 1920f;

        int VHEIGHT = 1080;
        int VWIDTH = 1920;

        GUI gui;
        Character testy;

        KeyboardState newkeystate;
        KeyboardState oldkeystate;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);

            //Set the initial resolution BEFORE Resoution.Init()
            graphics.PreferredBackBufferHeight = VHEIGHT;
            graphics.PreferredBackBufferWidth = VWIDTH;

            Resolution.Init(ref graphics);

            Content.RootDirectory = "Content";

            Resolution.SetVirtualResolution((int)WIDTH, (int)HEIGHT);
            Resolution.SetResolution(VWIDTH, VHEIGHT, false);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gui = new GUI(this.Content);
            gui.WriteMSG("Es ist so ein schöner Tag!\nKomm Senpai, lass uns Schlittschulaufen gehen! Es sieht so herrlich aus!");

            List<String> body = new List<String>();
            List<String> face = new List<String>();
            body.Add("testchar");
            testy = new Character("Testy", body, face, this.Content);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CheckControls();

            // TODO: Add your update logic here
            gui.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            Resolution.BeginDraw();

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, Resolution.getTransformationMatrix());
            
            gui.DrawBackground(spriteBatch);
            
            testy.Draw(spriteBatch);
            
            gui.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void CheckControls()
        {
            newkeystate = Keyboard.GetState();
            // END THE GAME
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // CHANGE RESOLUTION
            if (newkeystate.IsKeyDown(Keys.F2) && oldkeystate.IsKeyUp(Keys.F2))
            {
                if (graphics.PreferredBackBufferHeight == 1080)
                {
                    Resolution.SetResolution(1280, 720, graphics.IsFullScreen);
                }
                else if (graphics.PreferredBackBufferHeight == 720)
                {
                    Resolution.SetResolution(1920, 1080, graphics.IsFullScreen);
                }
                graphics.ApplyChanges();
            }
            // TOGGLE FULLSCREEN
            if (newkeystate.IsKeyDown(Keys.F5) && oldkeystate.IsKeyUp(Keys.F5))
            {
                Resolution.ToggleFullscreen();
            }

            oldkeystate = newkeystate;
        }

    }
}
