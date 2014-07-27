using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CockBlockDefense
{
    class Options
    {
        private Texture2D textures;

        private Vector2 settingsPosition;


        public Options(Texture2D textures, SpriteFont font, Vector2 position)
        {
            this.textures= textures;

            this.settingsPosition = position;
            settingsPosition = new Vector2(700, position.Y);


        }


        public void Draw(SpriteBatch spriteBatch, Player player)
        {
        ;
            spriteBatch.Draw(textures, settingsPosition, Color.White);


        }

    }
}
