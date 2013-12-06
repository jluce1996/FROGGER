using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace FROGGER
{
    class Square : Sprite
    {
       
        public Vector2 target;
        public Square(
            Vector2 location,
            Texture2D texture,
            Rectangle initialframe,
            Vector2 velocity) :
            
            base(location, texture, initialframe, velocity)
        {
        }
        public override void  Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                target = new Vector2(25, 0);
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                target = new Vector2(-25, 0);               
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                target = new Vector2(0, 25);                
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                target = new Vector2(0, -25);  
            }
        }
              public override void Draw(SpriteBatch spriteBatch)
            {
            base.Draw(spriteBatch);
            }
    }
}
