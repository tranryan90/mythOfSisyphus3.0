using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace APEngProj
{
    public class Screen
    {
        protected SoundEffect btnSound;
        protected String nextScreen;
        protected bool exitGame = false;
        public string GetNextSscreen()
        {
            return nextScreen;
        }
        //To Load content and pictures such as audio
        public virtual void LoadContent(ContentManager conManager, GraphicsDeviceManager graphics) { }
        //To get unload all loaded things from load content
        public virtual void UnloadContent() { }
        //Using actual gametime to update according to situations
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public bool GetExitGame()
        {
            return exitGame;
        }
    }
}
