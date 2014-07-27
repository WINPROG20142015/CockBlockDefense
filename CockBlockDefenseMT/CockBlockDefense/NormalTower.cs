using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CockBlockDefense
{
    class NormalTower : Tower
    {
        //Player player;
        public NormalTower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            this.damage = 50; // Set the damage
            this.cost = 15;   // Set the initial cost

            this.radius = 100; // Set the radius
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (bulletTimer >= 1.0f && target != null)
            {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center,
                new Vector2(bulletTexture.Width)), rotation, 3, damage);

                bulletList.Add(bullet);
                bulletTimer = 0;
                if (Vector2.Distance(bullet.Center, target.Center) < 22)
                {
                    target.CurrentHealth -= bullet.Damage;
                    if (target.CurrentHealth <= 0)
                        //player.Money += target.BountyGiven;                        
                    bullet.Kill();
                }
                
            }
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];

                bullet.SetRotation(rotation);
                bullet.Update(gameTime);
                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                if (target != null && Vector2.Distance(bullet.Center, target.Center) < 22)
                {
                    target.CurrentHealth -= bullet.Damage;
                    if (target.CurrentHealth <= 0)
                        //player.Money = player.Money + 1;
                    bullet.Kill();
                }

                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }

        }

    }
}
