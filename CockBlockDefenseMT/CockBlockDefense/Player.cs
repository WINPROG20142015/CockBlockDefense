using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace CockBlockDefense
{
    class Player
    {
        private string newTowerType;

        public string NewTowerType
        {
            set { newTowerType = value; }
        }

        private int money = 50;
        private int lives = 30;
        const int tileSize = 50;
        const int offSet = 25;

        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        private Texture2D towerTexture;
        private Texture2D bulletTexture;

        private List<Tower> towers = new List<Tower>();

        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame

        public int Money
        {
            get { return money; }
            set { money = value; }
        }
        public int Lives
        {
            get { return lives; }
        }
        private Level level;

        public Player(Level level, Texture2D towerTexture, Texture2D bulletTexture)
        {
            this.level = level;
            this.towerTexture = towerTexture;
            this.bulletTexture = bulletTexture;
        }
        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && cellX < level.Width && cellY < level.Height;

            bool spaceClear = true;

            foreach (Tower tower in towers)
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;
            }

            bool onPath = (level.GetIndex(cellX, cellY) == 0);

            return inBounds && spaceClear && onPath;
        }
        public void AddTower()
        {
            Tower towerToAdd = null;

            switch (newTowerType)
            {
                case "Normal Tower":
                    {
                        towerToAdd = new NormalTower(towerTexture, bulletTexture, new Vector2(tileX, tileY));
                        break;
                    }
            }

            if (IsCellClear() == true && towerToAdd.Cost <= money)
            {
                towers.Add(towerToAdd);
                money -= towerToAdd.Cost;

                newTowerType = string.Empty;
            }
        }

  
        public void Update(GameTime gameTime, List<Enemy> enemies, List<Enemy> enemies1)
        {
            mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / 50);
            cellY = (int)(mouseState.Y / 50);

            tileX = cellX * 50;
            tileY = cellY * 50; 
             
            if (mouseState.LeftButton == ButtonState.Released && 
                oldState.LeftButton == ButtonState.Pressed)
            {
                if (string.IsNullOrEmpty(newTowerType) == false)
                {
                    AddTower();
                }
            }
            foreach (Tower tower in towers)
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies, enemies1);
                }

                tower.Update(gameTime);
            }
            oldState = mouseState;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }
       
    }
}
