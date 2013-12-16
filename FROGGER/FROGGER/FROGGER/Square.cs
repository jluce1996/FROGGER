﻿using System;
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
       bool KeyDown = false;
        Keys Key = Keys.None;
        Vector2 newlocation;

        public EventHandler OnWin;
 
        public Square(
            Vector2 location,
            Texture2D texture,
            Rectangle initialframe,
            Vector2 velocity) :
            
            base(location, texture, initialframe, velocity)
        {
            newlocation = this.location;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            DetectKeyPress(kb, Keys.Right);
            DetectKeyPress(kb, Keys.Left);
            DetectKeyPress(kb, Keys.Up);
            DetectKeyPress(kb, Keys.Down);

            if (KeyDown)
            {
                if (kb.IsKeyUp(Key))
                {
                    switch (Key)
                    {
                        case Keys.Right:
                            {
                                this.location.X += 50;
                                break;
                            }
                        case Keys.Left:
                            {
                                this.location.X += -50;
                                break;
                            }
                        case Keys.Up:
                            {
                                this.location.Y += -50;
                                break;
                            }
                        case Keys.Down:
                            {
                                this.location.Y += 50;
                                break;
                            }
                    }
                    KeyDown = false;
                    Key = Keys.None;
                }
            }

            //To check if it is in the window.
            if (this.location.X >= 800)
            {
                this.location.X = 750;
            }
            if (this.location.X <= 0)
            {
                this.location.X = 0;
            }
            if (this.location.Y < 0)
            {
                this.location.X = 400;
                this.location.Y = 600;

                if (OnWin != null)
                    OnWin(this, null);
            }
            if (this.location.Y >= 600)
            {
                this.location.Y = 550;
            }
            base.Update(gameTime);
        }
       
        protected void DetectKeyPress(KeyboardState kb, Keys key)
        {
            if (kb.IsKeyDown(key))
            {
                Key = key;
                KeyDown = true;
            }
        }
             
        public override void Draw(SpriteBatch spriteBatch)
            {
            base.Draw(spriteBatch);
            }
    }
}
