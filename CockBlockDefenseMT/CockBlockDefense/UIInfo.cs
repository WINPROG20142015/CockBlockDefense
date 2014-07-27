using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CockBlockDefense
{
    class UIInfo
    {
        private Texture2D texture;
       
        private SpriteFont font;

        // The position of the UI wave
        private Vector2 waveposition;
        private Vector2 wavetextPosition;
        
        public UIInfo(Texture2D texture, SpriteFont font, Vector2 position)//stat UI
         {
             this.texture = texture;
             this.font = font;

             this.waveposition = position;
             // Offset the text to the bottom right corner
             wavetextPosition = new Vector2(130, position.Y + 10);
         }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, waveposition, Color.White);

            string text = string.Format("");//wave count
            spriteBatch.DrawString(font, text, wavetextPosition, Color.White);
        }

    }
}
