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

namespace Tutorial_Series_1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite player;
        Texture2D cakeTexture;
        List<Sprite> cakeList = new List<Sprite>();

        float timer = 0f;
        float dropInterval = 2f;
        float speed = 4f;

        Random random;

        HUD hud;

        SoundEffect soundEffect;
        Song song;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

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
            random = new Random();

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
            cakeTexture = Content.Load<Texture2D>("Cake");

            player = new Sprite();
            player.Texture = Content.Load<Texture2D>("Face");

            // retrieve the height of the screen
            int screenHeight = GraphicsDevice.Viewport.Height;
            // find the center point of the screen along the x-axis
            int screenCenterX = GraphicsDevice.Viewport.Width / 2;
            player.Position = new Vector2(
                screenCenterX - (player.Texture.Width / 2), 
                screenHeight - player.Texture.Height - 20);

            player.SetRelativeBoundingBox(20, 74, player.Texture.Width - 40, 20);

            hud = new HUD();
            hud.Font = Content.Load<SpriteFont>("Arial");

            // Load the SoundEffect
            soundEffect = Content.Load<SoundEffect>("bite");

            //Load the Song
            song = Content.Load<Song>("Yes");

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

            // TODO: Add your update logic here
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= dropInterval)
            {
                int xPos = random.Next(GraphicsDevice.Viewport.Width - 50);
                cakeList.Add(new Sprite(cakeTexture, new Vector2(xPos, -100)));
                timer = 0f;
            }

            HandleCollisions();

            HandleFallingCake();

            HandleInput();

            base.Update(gameTime);
        }

        private void HandleFallingCake()
        {
            List<Sprite> toRemove = new List<Sprite>();

            foreach (Sprite cake in cakeList)
            {
                if (cake.Position.Y > (GraphicsDevice.Viewport.Height + 20))
                {
                    toRemove.Add(cake);
                }
                else
                    cake.Position += new Vector2(0, speed);
            }

            if (toRemove.Count > 0)
            {
                foreach (Sprite cake in toRemove)
                {
                    cakeList.Remove(cake);
                }
            }
        }

        private void HandleInput()
        {
            // Retrieve the current state of the keyboard
            KeyboardState keyboardState = Keyboard.GetState();

            Vector2 playerVelocity = Vector2.Zero;

            // Check if the Left arrow key is pressed and change the velocity of the character accordingly
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                playerVelocity += new Vector2(-speed, 0);
            }

            // Check if the Right arrow key is pressed and change the velocity of the character accordingly
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                playerVelocity += new Vector2(speed, 0);
            }

            // Apply the velocity to the character's position
            player.Position += playerVelocity;

            // Prevent player from moving off the left edge of the screen
            if (player.Position.X < 0)
                player.Position = new Vector2(0, player.Position.Y);

            // Prevent player from moving off the right edge of the screen
            int rightEdge = GraphicsDevice.Viewport.Width - player.Texture.Width;
            if (player.Position.X > rightEdge)
                player.Position = new Vector2(rightEdge, player.Position.Y);
        }

        private void HandleCollisions()
        {
            Sprite toRemove = null;

            foreach (Sprite cake in cakeList)
            {
                if (player.BoundingBox.Intersects(cake.BoundingBox))
                {
                    toRemove = cake;

                    // Play sound effect
                    soundEffect.Play();
                    
                    hud.Score += 10;
                    break;
                }
            }

            if (toRemove != null)
                cakeList.Remove(toRemove);
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

            player.Draw(spriteBatch);

            foreach (Sprite cake in cakeList)
            {
                cake.Draw(spriteBatch);
            }

            hud.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
