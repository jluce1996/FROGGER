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
    class RECTANGLE : Sprite
    {
        public RECTANGLE(
            Vector2 location,
            Texture2D texture,
            Rectangle initialframe,
            Vector2 velocity) :

            base(location, texture, initialframe, velocity)
        {
            velocity = new Vector2(-50, 0);
        }

    }
}
