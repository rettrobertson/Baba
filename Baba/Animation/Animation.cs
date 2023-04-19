using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Baba.Animation
{
    public class Animation
    {
        protected Texture2D texture;
        protected KeyFrame[] keyFrames;

        public Animation(string texture, int frames) 
        {
            keyFrames = new KeyFrame[frames];
            //add callback to assign texture when content is loaded
        }

        protected struct KeyFrame
        {
            public TimeSpan time;
            public Rectangle bounds;
        }
    }
}
