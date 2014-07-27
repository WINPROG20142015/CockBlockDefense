using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CockBlockDefense
{
    class Level
    {
        const int tileSize = 50;

        int[,] map = new int[,] 
        {
            {11,10,11,10,11,10,11,10,11,10,11,10,11,10,12},
            { 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,11},
            { 1, 1, 6, 0,12, 9, 0, 7, 1, 1, 1, 1, 1, 1, 1},
            {10, 0, 2, 0,11,10, 0, 3, 0, 0, 0, 0, 0, 0,12},
            { 9, 0, 3, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0,11},
            {10, 0, 5, 1, 8, 1, 8, 4, 0, 7, 1, 1, 6, 0,12},
            { 9, 0, 0, 0, 3, 0, 2, 0, 0, 3, 0, 0, 2, 0,11},
            {10, 0, 0, 0, 2, 0, 3, 0, 0, 2, 0, 0, 3, 0,12},
            { 1, 1, 1, 1, 4, 0, 5, 1, 1, 4, 0, 0, 5, 1, 1},
            { 9, 0, 0, 0, 0, 0, 0, 0, 0, 0,12, 9, 0, 0,12},
            {10,12, 9,12, 9,12, 9,12, 9,12, 9,12, 9,12, 9},


        };
        
        public int Width
        {
            get { return 15; }
        }
        public int Height
        {
            get { return 11; }
        }

        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX >= 15 || cellY < 0 || cellY >= 11)
                return 0;

            return map[cellY, cellX];
        }

        
        private List<Texture2D> tileTextures = new List<Texture2D>();

        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }
        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    int textureIndex = map[y, x];
                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];
                    batch.Draw(texture, new Rectangle(
                        x * tileSize +25 , y * tileSize +25, tileSize, tileSize), Color.White);
                }
            }
        }

        private Queue<Vector2> waypoints = new Queue<Vector2>();
        private Queue<Vector2> waypoints2 = new Queue<Vector2>();

        public Level()
        {
            waypoints.Enqueue(new Vector2(0, 2) * tileSize);
            waypoints.Enqueue(new Vector2(2, 2) * tileSize);
            waypoints.Enqueue(new Vector2(2, 5) * tileSize);
            waypoints.Enqueue(new Vector2(7, 5) * tileSize);
            waypoints.Enqueue(new Vector2(7, 2) * tileSize);
            waypoints.Enqueue(new Vector2(14, 2) * tileSize);

            waypoints2.Enqueue(new Vector2(0, 8) * tileSize);
            waypoints2.Enqueue(new Vector2(4, 8) * tileSize);
            waypoints2.Enqueue(new Vector2(4, 5) * tileSize);
            waypoints2.Enqueue(new Vector2(6, 5) * tileSize);
            waypoints2.Enqueue(new Vector2(6, 8) * tileSize);
            waypoints2.Enqueue(new Vector2(9, 8) * tileSize);
            waypoints2.Enqueue(new Vector2(9, 5) * tileSize);
            waypoints2.Enqueue(new Vector2(12, 5) * tileSize);
            waypoints2.Enqueue(new Vector2(12, 8) * tileSize);
            waypoints2.Enqueue(new Vector2(14, 8) * tileSize);
        }

        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }
        public Queue<Vector2> Waypoints2
        {
            get { return waypoints2; }
        }
    }
}
