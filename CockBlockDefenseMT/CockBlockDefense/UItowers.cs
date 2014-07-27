using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CockBlockDefense
{
    class UItowers
    {private Texture2D texture;
       
        private SpriteFont font;

        // The position of the UI wave
        private Vector2 towerposition;
        private Vector2 towertextPosition;
        
        public UItowers(Texture2D texture, SpriteFont font, Vector2 position)//stat UI
         {
             this.texture = texture;
             this.font = font;

             this.towerposition = position;
             // Offset the text to the bottom right corner
            towertextPosition = new Vector2(130, position.Y + 10);
         }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, towerposition, Color.White);

            //string text = string.Format(""); for text
           
           // spriteBatch.DrawString(font, text, wavetextPosition, Color.Black);
        }

    }
}
