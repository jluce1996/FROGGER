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
        enum GameStates { TitleScreen, Playing, PlayerDead, GameOver };
        GameStates gameState = GameStates.TitleScreen;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Square square;
        List<RECTANGLE> rectangles;
        Texture2D SQUARE;
        Texture2D titlescreen;
        Texture2D rectanglesprite;
        SpriteFont font;
        SoundEffect ThemeSong;
        int lives = 3;
        int level = 1;

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
            font = Content.Load<SpriteFont>("SpriteFont1");
            titlescreen = Content.Load<Texture2D>(@"titlescreen");
            ThemeSong = Content.Load<SoundEffect>("Music//ThemeSong");
            SoundEffectInstance ThemeSongLoop = ThemeSong.CreateInstance();
            ThemeSongLoop.IsLooped = true;
            ThemeSongLoop.Play();

            for (int x = 0; x < 4; x++)
            {
                rectangles.Add(new RECTANGLE(new Vector2(400 + x * 300, 450), rectanglesprite, new Rectangle(0, 0, 150, 50), new Vector2(-120, 0)));
                rectangles.Add(new RECTANGLE(new Vector2(200 - x * 300, 300), rectanglesprite, new Rectangle(0, 0, 150, 50), new Vector2(120, 0)));
                rectangles.Add(new RECTANGLE(new Vector2(400 + x * 300, 150), rectanglesprite, new Rectangle(0, 0, 150, 50), new Vector2(-120, 0)));
            }

            square = new Square(new Vector2(370, 550), SQUARE, new Rectangle(0, 0, 50, 50), Vector2.Zero);
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
            level = level + 1;
        }

        private void resetGame()
        {
            square.Location = new Vector2(370, 550);      
            lives = 3;
            level = 1;
            square.PlayerScore = 0;

            int wtf = 0;
            for (int x = 0; x < 4; x++)
            {
                rectangles[wtf++].Velocity = new Vector2( -120, 0);
                rectangles[wtf++].Velocity = new Vector2(120, 0);
                rectangles[wtf++].Velocity = new Vector2(-120, 0);
            }
        }


        //Load music here
        

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
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        gameState = GameStates.Playing;
                        resetGame();
                    }
                    break;
                case GameStates.Playing:
                    {
                        for (int i = 0; i < rectangles.Count; i++)
                        {
                            if (square.IsBoxColliding(rectangles[i].BoundingBoxRect))
                            {
                                square.Location = new Vector2(390, 550);
                                square.PlayerScore = square.PlayerScore - 100;
                                lives = lives - 1;
                                gameState = GameStates.PlayerDead;
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
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                            this.Exit();
                        square.Update(gameTime);
                        base.Update(gameTime);
                        if (lives == 0)
                        {
                            gameState = GameStates.GameOver;
                        }
                        else
                        {
                            gameState = GameStates.Playing;
                        }
                    }
                    break;
                case GameStates.PlayerDead:
                    {
                        square.Update(gameTime);
                        base.Update(gameTime);
                    }
                    break;
                case GameStates.GameOver:
                    {
                        base.Update(gameTime);
                        square.Update(gameTime);
                        for (int i = 0; i < rectangles.Count; i++)
                        {
                            rectangles[i].Update(gameTime);
                        }
                        gameState = GameStates.TitleScreen;
                        resetGame();
                    }
                    break;
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (gameState == GameStates.TitleScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(titlescreen, new Rectangle(0, 0, this.Window.ClientBounds.Width,
                    this.Window.ClientBounds.Height),
                    Color.White);
                spriteBatch.End();

            }
            if ((gameState == GameStates.Playing) ||
                (gameState == GameStates.PlayerDead) ||
                (gameState == GameStates.GameOver))
            {
                spriteBatch.Begin();
                square.Draw(spriteBatch);
                for (int i = 0; i < rectangles.Count; i++)
                {
                    rectangles[i].Draw(spriteBatch);
                }
                spriteBatch.DrawString(font, "Lives:" + lives.ToString(), new Vector2(20, 20), Color.White);
                spriteBatch.DrawString(font, "Score:" + square.PlayerScore.ToString(), new Vector2(600, 20), Color.White);
                spriteBatch.DrawString(font, "Level  " + level.ToString(), new Vector2(320, 20), Color.Orange);
                base.Draw(gameTime);
                spriteBatch.End();
            }
            if (gameState == GameStates.GameOver)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font,
                    " G A M E  O V E R!!!",
                    new Vector2(200, 40),
                    Color.Yellow);
                        spriteBatch.End();
                    }
            }
        }
    }

