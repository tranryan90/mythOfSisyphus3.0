using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

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
        public static KeyboardState oldState, newState;
        public static float rotation;
        public static int hillOffset;
        public static float timeSinceSpace;
        public static float progress;
        public static Dictionary<string, Screen> Screens;

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

            Content = base.Content;
            //Adding screens to the dictionary

            base.LoadContent();

            sisyphus = new AnimatedSprite(Content.Load<Texture2D>("Sisyphus"), 8, 5);
            hill = Content.Load<Texture2D>("hill");
            boulder = Content.Load<Texture2D>("boulder");
            progressBar = Content.Load<Texture2D>("progress");
            progressBarFull = Content.Load<Texture2D>("progressFull");

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
            newState = Keyboard.GetState();

            if (oldState.IsKeyUp(Keys.Space) && newState.IsKeyDown(Keys.Space))
            {
                timeSinceSpace = 0;
            } else
            {
                timeSinceSpace += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (timeSinceSpace < 200)
            {
                progress += gameTime.ElapsedGameTime.Milliseconds;
                sisyphus.Update();

                rotation += MathHelper.Pi / 200;

                hillOffset += 1;
                if (hillOffset == 300)
                {
                    hillOffset = 0;
                }
            }

            oldState = newState;

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

            spriteBatch.Draw(hill, new Vector2(-hillOffset * 8f / 3f, -480 + hillOffset), Color.White);
            spriteBatch.Draw(boulder, new Vector2(528, 220), new Rectangle(0, 0, 128, 128), Color.White, rotation, new Vector2(64, 64), 1.0f, SpriteEffects.None, 0f);
            sisyphus.Draw(spriteBatch, new Vector2(400, 200));
            spriteBatch.Draw(progressBar, new Vector2(200, 420), Color.White);
            spriteBatch.Draw(progressBarFull, new Vector2(200, 420), new Rectangle(0, 0, (int)(progress / 60000f * 400f), 20), Color.White);
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
