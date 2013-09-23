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
        // ################## SYSTEM VARIABLES ##################
        GraphicsDeviceManager graphics;        // XNA Object that handles the graphicscard and screenmodes
        SpriteBatch spriteBatch;               // XNA Object that handles rendering sprites to the screen

        int VHEIGHT = 720;                    // The VIRTUAL Resolution - The resolution we are working with (fixed)
        int VWIDTH = 1280;

        float HEIGHT = 720f;                  // The RENDERED Resolution - This is the displayed resolution (changable)
        float WIDTH = 1280f;

        Boolean FULLSCREEN = false;

        // ################# OBJECT DECLARATION #################
        MainMenu menu;
        public Audio audio;
        public GUI gui;
        Scripthandler scripth;
        public CharacterManager charmanager;

        // ################## GAME VARIABLES ####################
        // Game State
        String GameState;        
        // Control variables
        KeyboardState newkeystate;  // Saves the current state of the keyboard
        KeyboardState oldkeystate;  // Saves the state of the keyboard of the last tick
        // Various
        Boolean PromptScriptExport; // "Do you want to export the script?"

        // Game Constructor
        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);

            //Set the initial resolution BEFORE Resoution.Init()
            graphics.PreferredBackBufferHeight = VHEIGHT;
            graphics.PreferredBackBufferWidth = VWIDTH;

            Resolution.Init(ref graphics);

            Content.RootDirectory = "Content";      // Defines where every game asset can be found

            Resolution.SetVirtualResolution((int)WIDTH, (int)HEIGHT);
            Resolution.SetResolution(VWIDTH, VHEIGHT, FULLSCREEN);

            Console.WriteLine(graphics.PreferredBackBufferHeight);
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
            menu = new MainMenu(this.Content);
            audio = new Audio(this.Content);

            GameState = "Game";
            PromptScriptExport = false;

            scripth = new Scripthandler(this);
            scripth.PlayScript("Demo");

            charmanager = new CharacterManager();
            
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
            charmanager.LoadCharacters(this.Content);
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
            scripth.Update();
            gui.Update();
            charmanager.Update();
            audio.Update();

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

            if (GameState == "Menu")
            {
                menu.Draw(spriteBatch);
            }
            else if (GameState == "Game")
            {
                gui.DrawBackground(spriteBatch);

                charmanager.Draw(spriteBatch);

                gui.Draw(spriteBatch, PromptScriptExport);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// This is where the keyboard and mouse checks are.
        /// </summary>
        private void CheckControls()
        {
            newkeystate = Keyboard.GetState();

            // END THE GAME
            if (newkeystate.IsKeyUp(Keys.Escape) && oldkeystate.IsKeyDown(Keys.Escape))
            {
                if (PromptScriptExport)
                    PromptScriptExport = false;
                else
                    Exit();
            }

            // PRESSING ENTER
            if (newkeystate.IsKeyDown(Keys.Enter) && oldkeystate.IsKeyUp(Keys.Enter))
            {
                gui.SetMSGSpeed(4f);
            }
            if (newkeystate.IsKeyUp(Keys.Enter) && oldkeystate.IsKeyDown(Keys.Enter))
            {
                if (GameState == "Menu")
                {
                    menu.Accept();
                }
                else if (GameState == "Game")
                {
                    gui.SetMSGSpeed(1f);
                    scripth.PushAccept(gui.selectedchoice);
                }

                if (PromptScriptExport)
                {
                    scripth.ExportScript();
                    PromptScriptExport = false;
                }

            }

            // UP and DOWN ARROWS - SELECTING STUFF!
            if (newkeystate.IsKeyUp(Keys.Up) && oldkeystate.IsKeyDown(Keys.Up))
            {
                if (GameState == "Menu")
                {
                    menu.SelectUp();
                }
                else if (GameState == "Game")
                {
                    gui.SelectUp();
                }
            }
            if (newkeystate.IsKeyUp(Keys.Down) && oldkeystate.IsKeyDown(Keys.Down))
            {
                if (GameState == "Menu")
                {
                    menu.SelectDown();
                }
                else if (GameState == "Game")
                {
                    gui.SelectDown();
                }
            }
            
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
            // EXPORT THE SCRIPT TO A... SCRIPT FOR VOICE ACTORS
            if (newkeystate.IsKeyDown(Keys.F8) && oldkeystate.IsKeyUp(Keys.F8))
            {
                if (!PromptScriptExport)
                {
                    PromptScriptExport = true;
                }
            }

            oldkeystate = newkeystate;
        }

    }
}
