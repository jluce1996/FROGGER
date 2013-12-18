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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Square square;
        List<RECTANGLE> rectangles;
        Texture2D SQUARE;
        Texture2D rectanglesprite;
        SpriteFont Pericles14;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();


            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            rectanglesprite = Content.Load<Texture2D>("rectangle");
            SQUARE = Content.Load<Texture2D>("square");
            rectanglesprite = Content.Load<Texture2D>("rectangle");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            rectangles = new List<RECTANGLE>();
            Pericles14 = Content.Load<SpriteFont>(@"Pericles14.spritefont");

            for (int x = 0; x < 4; x++)
            {
                rectangles.Add(new RECTANGLE(new Vector2(400 + x * 300, 450), rectanglesprite, new Rectangle(0, 0, 150, 50), new Vector2(-120, 0)));
                rectangles.Add(new RECTANGLE(new Vector2(200 - x * 300, 300), rectanglesprite, new Rectangle(0, 0, 150, 50), new Vector2(120, 0)));
                rectangles.Add(new RECTANGLE(new Vector2(400 + x * 300, 150), rectanglesprite, new Rectangle(0, 0, 150, 50), new Vector2(-120, 0)));
            }

            square = new Square(new Vector2(390, 550), SQUARE, new Rectangle(0, 0, 50, 50), Vector2.Zero);
            square.OnWin += new EventHandler(this.OnWin);

        }

        private void OnWin(object sender, EventArgs e)
        {
            for (int i = 0; i < rectangles.Count; i++)
            {
                if (rectangles[i].Velocity.X < 0)
                {
                    rectangles[i].Velocity += new Vector2(-40, 0);
                }
                if (rectangles[i].Velocity.X > 0)
                {
                    rectangles[i].Velocity += new Vector2(40, 0);
                }
            }
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < rectangles.Count; i++)
            {
                if(square.IsBoxColliding(rectangles[i].BoundingBoxRect))
                {
                    square.Location = new Vector2(390, 550);
                }
                if (rectangles[i].Location.X > this.Window.ClientBounds.Width && rectangles[i].Velocity.X > 0)
                {
                    rectangles[i].Location = new Vector2(-400, rectangles[i].Location.Y);
                }
                if (rectangles[i].Location.X < -150 && rectangles[i].Velocity.X < 0)
                {
                    rectangles[i].Location = new Vector2(1050, rectangles[i].Location.Y);
                }

            rectangles[i].Update(gameTime);       
            }

          

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            square.Update(gameTime);
            base.Update(gameTime);
          
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);            
            spriteBatch.Begin();
            square.Draw(spriteBatch);
            for (int i = 0; i < rectangles.Count; i++)
            {
                rectangles[i].Draw(spriteBatch);
            }
            spriteBatch.DrawString(Pericles14,"Score:" + square.PlayerScore.ToString(), new Vector2(500, 20), Color.White);
            base.Draw(gameTime);
            spriteBatch.End();            
        }
    }
}
