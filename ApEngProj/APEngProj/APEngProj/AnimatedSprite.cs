using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



namespace APEngProj
{
    public class AnimatedSprite
    {

        public Texture2D Texture { get; set; }
        public int Frames { get; set; }
        private int currentFrame;
        public int Delay { get; set; }
        public int delayCounter;

        public AnimatedSprite(Texture2D texture, int frames, int delay)
        {
            Texture = texture;
            Frames = frames;
            currentFrame = 0;
            Delay = delay;
            delayCounter = 0;
        }

        public void Update()
        {
            delayCounter++;
            if (delayCounter == Delay)
            {
                currentFrame++;
                delayCounter = 0;
                if (currentFrame == Frames)
                    currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Frames;
            int height = Texture.Height;

            Rectangle sourceRectangle = new Rectangle(width * currentFrame, 0, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
            
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

    }
}
