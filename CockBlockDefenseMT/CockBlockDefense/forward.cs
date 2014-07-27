using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CockBlockDefense
{
    class forward
    {
        private Texture2D texture;
        
        private Vector2 ForwardPosition;
       


        public forward(Texture2D texture, SpriteFont font, Vector2 position)
        {
            this.texture = texture;

            this.ForwardPosition = position;
            ForwardPosition= new Vector2(75, position.Y );
        

        }
       
      
        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, ForwardPosition, Color.White);
           
          
        }

    }
}
