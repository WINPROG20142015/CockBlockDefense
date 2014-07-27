using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace CockBlockDefense
{
    class pause
    {
        private Texture2D textures;
        private Vector2 pauses;

        public pause(Texture2D texture, SpriteFont font, Vector2 position)
        {
            this.textures = texture;
            this.pauses = position;
            pauses = new Vector2(20, position.Y);
        }
        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(textures, pauses, Color.White);


        }

         
    }
}
