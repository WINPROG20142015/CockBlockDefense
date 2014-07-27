using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace CockBlockDefense
{
    enum Screen
    {
        StartScreen,
        ModeScene,
        GamePlayScreen
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StartScreen startScreen;
        ModeScreen modeScreen;
        GamePlayScreen gamePlayScreen;
        Screen currentScreen;
     

        Song song;

       

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth =  800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";

          
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
            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;

            //Load the Song
            song = Content.Load<Song>("Sounds/BgMusic");

            // Set the Media player to repeat the song
            MediaPlayer.IsRepeating = true;

            // Tell MediaPlayer to play the song
            MediaPlayer.Play(song);

            
            
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Update(gameTime);
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                        gamePlayScreen.Update(gameTime);
                    break;
                case Screen.ModeScene:
                    if (modeScreen != null)
                        modeScreen.Update(gameTime);
                    break;
                //case Screen.GameOverScreen:
                    //break;
            }

            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Draw(spriteBatch);
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                        gamePlayScreen.Draw(spriteBatch);
                    break;
                case Screen.ModeScene:
                    if (modeScreen != null)
                        modeScreen.Draw(spriteBatch);
                    break;
                //case Screen.GameOverScreen:
                    //break;
            }
          
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void StartGame()
        {
            gamePlayScreen = new GamePlayScreen(this);
            currentScreen = Screen.GamePlayScreen;

            modeScreen = null;
        }
        public void modeSelect()
        {
            modeScreen = new ModeScreen(this);
            currentScreen = Screen.ModeScene;

            startScreen = null;
        }
        public void BackToStart()
        {
            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;

            modeScreen = null;
        }
    }
}
