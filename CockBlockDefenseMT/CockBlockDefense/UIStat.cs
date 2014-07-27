using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CockBlockDefense
{
    class UIStat
    {
        private Texture2D texture;
        // A class to access the font we created
        private SpriteFont font;


        // The position of the UI stat
        private Vector2 statposition;
        private Vector2 stattextPosition;

        // The position of the UI wave
        private Vector2 waveposition;
        private Vector2 wavetextPosition;
      


        public UIStat(Texture2D texture, SpriteFont font, Vector2 position)//stat UI
        {
            this.texture = texture;
            this.font = font;

            this.statposition = position;
            stattextPosition = new Vector2(525, position.Y + 50);   
        }

      
        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, statposition, Color.White);

            string text = string.Format("Gold : {0}         Lives : {1}", player.Money, player.Lives);
            spriteBatch.DrawString(font, text, stattextPosition, Color.Black);
        }


    }
}
