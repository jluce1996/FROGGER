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
        Vector2 newlocation;
        public Square(
            Vector2 location,
            Texture2D texture,
            Rectangle initialframe,
            Vector2 velocity) :
            
            base(location, texture, initialframe, velocity)
        {
            newlocation = this.location;
        }
        public override void  Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right))
            {
                this.location.X += 50;
                newlocation = this.location;
            }
            if (keyboard.IsKeyDown(Keys.Left))
            {
                this.location.X += -50;
                newlocation = this.location;           
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                this.location.Y += 50;
                newlocation = this.location;               
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                this.location.Y += -50;
                newlocation = this.location;
            }
            if (this.location.X >= 800)
            {
                this.location.X = 750;
            }
            if (this.location.X <= 0)
            {
                this.location.X = 0;
            }
            if (this.location.Y <= 0)
            {
                this.location.X = 400;
                this.location.Y = 600;
            }
            if (this.location.Y >= 600)
            {
                this.location.Y = 550;
            }
            base.Update(gameTime);
        }
        
              public override void Draw(SpriteBatch spriteBatch)
            {
            base.Draw(spriteBatch);
            }
    }
}
