using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CockBlockDefense
{
    class WaveManager
    {
        private int numberOfWaves; // How many waves the game will have
        private float timeSinceLastWave; // How long since the last wave ended

        private Queue<Wave> waves1 = new Queue<Wave>(); // A queue of all our waves
        private Queue<Lane2> waves2 = new Queue<Lane2>(); // A queue of all our waves

        private Texture2D enemyTexture; // The texture used to draw the enemies

        private bool waveFinished = false; // Is the current wave over?

        private Level level; // A reference to our level class

        public Wave CurrentWave1 // Get the wave at the front of the queue
        {
            get { return waves1.Peek(); }
        }
        public Lane2 CurrentWave2 // Get the wave at the front of the queue
        {
            get { return waves2.Peek(); }
        }
        public List<Enemy> Enemies // Get a list of the current enemeies
        {
            get { return CurrentWave1.Enemies; }
        }

        public int Round // Returns the wave number
        {
            get { return CurrentWave1.RoundNumber + 1; }
        }
        public WaveManager(Level level, int numberOfWaves, Texture2D enemyTexture)
        {
            this.numberOfWaves = numberOfWaves;
            this.enemyTexture = enemyTexture;
            this.level = level;

            for (int i = 0; i < numberOfWaves; i++)
            {
                int initialNumerOfEnemies = 6;
                int numberModifier = (i / 6) + 1;

                Wave wave1 = new Wave(i, initialNumerOfEnemies *
                    numberModifier, level, enemyTexture);
                Lane2 wave2 = new Lane2(i, initialNumerOfEnemies *
                    numberModifier, level, enemyTexture);

                waves1.Enqueue(wave1);
                waves2.Enqueue(wave2);

                StartNextWave();
            }
        }
        private void StartNextWave()
        {
            if (waves1.Count > 0) // If there are still waves left
            {
                waves1.Peek().Start(); // Start the next one
                waves2.Peek().Start(); // Start the next one

                timeSinceLastWave = 0; // Reset timer
                waveFinished = false;
            }
        }
        public void Update(GameTime gameTime)
        {
            CurrentWave1.Update(gameTime); // Update the wave
            CurrentWave2.Update(gameTime); // Update the wave

            if (CurrentWave1.RoundOver && CurrentWave2.RoundOver) // Check if it has finished
            {
                waveFinished = true;
            }

            if (waveFinished) // If it has finished
            {
                timeSinceLastWave += (float)gameTime.ElapsedGameTime.TotalSeconds; // Start the timer
            }

            if (timeSinceLastWave > 5.0f) // If 5 seconds has passed
            {
                waves1.Dequeue(); // Remove the finished wave
                waves2.Dequeue();
                StartNextWave(); // Start the next wave
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave1.Draw(spriteBatch);
            CurrentWave2.Draw(spriteBatch);
        }

    }
}
