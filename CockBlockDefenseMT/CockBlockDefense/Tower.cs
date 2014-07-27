using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CockBlockDefense
{
    public class Tower : Sprite
    {

        protected Enemy target;
        protected Texture2D bulletTexture;
        public List<Bullet> bulletList = new List<Bullet>();
        protected float bulletTimer; // How long ago was a bullet fired
        
        public Enemy Target
        {
            get { return target; }
        }

        public void GetClosestEnemy(List<Enemy> enemies, List <Enemy> enemies1)
        {
            target = null;
            float smallestRange = radius;

            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(center, enemy.Center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(center, enemy.Center);
                    target = enemy;
                }
            }
            foreach (Enemy enemy in enemies1)
            {
                if (Vector2.Distance(center, enemy.Center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(center, enemy.Center);
                    target = enemy;
                }
            }
        }
        protected void FaceTarget()
        {
            Vector2 direction = center - target.Center;
            direction.Normalize();

            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }

        protected int cost; // How much will the tower cost to make
        protected int damage; // The damage done to enemy's

        protected float radius; // How far the tower can shoot

        public int Cost
        {
            get { return cost; }
        }
        public int Damage
        {
            get { return damage; }
        }

        public float Radius
        {
            get { return radius; }
        }
        public Tower(Texture2D texture,Texture2D bulletTexture, Vector2 position)
            : base (texture, position)
        {
            this.bulletTexture = bulletTexture;
        }
        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(center, position) <= radius)
                return true;

            return false;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (target != null)
            {
                FaceTarget();

                if (!IsInRange(target.Center) || target.IsDead)
                {
                    target = null;
                    bulletTimer = 0;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bulletList)
                bullet.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
