using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace APEngProj
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ScreenManager : Game
    {
        public static GraphicsDeviceManager graphicsMger;
        public static SpriteBatch spriteBatch;
        public static AnimatedSprite sisyphus;
        public static Texture2D hill;
        public static Texture2D boulder;
        public static Texture2D progressBar;
        public static Texture2D progressBarFull;
        public static Texture2D gameOver;
        public static KeyboardState oldState, newState;
        public static float rotation;
        public static int hillOffset;
        public static float timeSinceSpace;
        public static float progress;
        public static string mode;
        public static int boulderOffset;
        public static int gameOverCounter;
        public static Dictionary<string, Screen> Screens;
        Song bgSong;
        

        public ScreenManager()
        {
            graphicsMger = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Screens = new Dictionary<string, Screen>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
            rotation = 0;
            hillOffset = 0;
            progress = 0;
            boulderOffset = 0;
            gameOverCounter = 0;
            mode = "start";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Where we are gonna add event handling
            this.bgSong = Content.Load<Song>("Exist");
            Content = base.Content;
            //Adding screens to the dictionary

            base.LoadContent();

            sisyphus = new AnimatedSprite(Content.Load<Texture2D>("Sisyphus"), 8, 5);
            hill = Content.Load<Texture2D>("hill");
            boulder = Content.Load<Texture2D>("boulder");
            progressBar = Content.Load<Texture2D>("progress");
            progressBarFull = Content.Load<Texture2D>("progressFull");
            gameOver = Content.Load<Texture2D>("gameOver");

            MediaPlayer.Play(bgSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(bgSong);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (mode == "start")
            {
                newState = Keyboard.GetState();

                if (newState.IsKeyDown(Keys.Space))
                {
                    mode = "game";
                }

                oldState = newState;
            }
            if (mode == "game")
            {
                newState = Keyboard.GetState();

                if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space))
                {
                    timeSinceSpace = 0;
                }
                else
                {
                    timeSinceSpace += gameTime.ElapsedGameTime.Milliseconds;
                }

                if (timeSinceSpace < 200)
                {
                    progress += gameTime.ElapsedGameTime.Milliseconds;
                    sisyphus.Update();

                    rotation += MathHelper.Pi / 150;

                    hillOffset += 1;
                    if (hillOffset >= 600 / .9f)
                    {
                        hillOffset = 0;
                    }
                }
                else
                {
                    mode = "falling";
                }

                if (progress >= 59000)
                {
                    mode = "falling";
                }

                oldState = newState;
            }
            else if (mode == "falling")
            {
                rotation -= MathHelper.Pi / 40;
                boulderOffset += 5;
                if (boulderOffset > 528)
                {
                    mode = "gameOver";
                }
            }
            else if (mode == "gameOver")
            {
                gameOverCounter++;
                if (gameOverCounter >= 300)
                {
                    mode = "reset";
                }
            }
            else if (mode == "reset")
            {
                oldState = new KeyboardState();
                rotation = 0;
                hillOffset = 0;
                progress = 0;
                boulderOffset = 0;
                gameOverCounter = 0;
                mode = "start";
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (mode == "start")
            {
                spriteBatch.Draw(hill, new Vector2(-hillOffset * 8f / 3f * .45f, -480 + hillOffset * .45f), Color.White);
                spriteBatch.Draw(boulder, new Vector2(528, 220), new Rectangle(0, 0, 128, 128), Color.White, rotation, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0f);
                sisyphus.Draw(spriteBatch, new Vector2(400, 200));
                spriteBatch.Draw(progressBar, new Vector2(200, 420), Color.White);
                spriteBatch.Draw(progressBarFull, new Vector2(200, 420), new Rectangle(0, 0, (int)(progress / 60000f * 400f), 20), Color.White);
            }
            if (mode == "game")
            {
                spriteBatch.Draw(hill, new Vector2(-hillOffset * 8f / 3f * .45f, -480 + hillOffset * .45f), Color.White);
                spriteBatch.Draw(boulder, new Vector2(528, 220), new Rectangle(0, 0, 128, 128), Color.White, rotation, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0f);
                sisyphus.Draw(spriteBatch, new Vector2(400, 200));
                spriteBatch.Draw(progressBar, new Vector2(200, 420), Color.White);
                spriteBatch.Draw(progressBarFull, new Vector2(200, 420), new Rectangle(0, 0, (int)(progress / 60000f * 400f), 20), Color.White);
            }
            else if (mode == "falling")
            {
                spriteBatch.Draw(hill, new Vector2(-hillOffset * 8f / 3f * .45f, -480 + hillOffset * .45f), Color.White);
                spriteBatch.Draw(boulder, new Vector2(528 - boulderOffset * 8f / 3f * .45f, 220 + boulderOffset * .45f), new Rectangle(0, 0, 128, 128), Color.White, rotation, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0f);
                sisyphus.Draw(spriteBatch, new Vector2(400, 200));
                spriteBatch.Draw(progressBar, new Vector2(200, 420), Color.White);
                spriteBatch.Draw(progressBarFull, new Vector2(200, 420), new Rectangle(0, 0, (int)(progress / 60000f * 400f), 20), Color.White);
            }
            else if (mode == "gameOver")
            {
                spriteBatch.Draw(hill, new Vector2(-hillOffset * 8f / 3f * .45f, -480 + hillOffset * .45f), Color.White);
                sisyphus.Draw(spriteBatch, new Vector2(400, 200));
                spriteBatch.Draw(progressBar, new Vector2(200, 420), Color.White);
                spriteBatch.Draw(progressBarFull, new Vector2(200, 420), new Rectangle(0, 0, (int)(progress / 60000f * 400f), 20), Color.White);
                spriteBatch.Draw(gameOver, new Vector2(0, 0), new Color(Color.White, gameOverCounter / 100f));
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            //Need to make some other parts before we can start this 
            //probably need to make title and game screen
        }
    }
}
