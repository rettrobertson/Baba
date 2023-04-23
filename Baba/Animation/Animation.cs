using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Baba.Animation
{
    //ChatGPT looked this over and explained it to me and says it should work properly
    public class Animation
    {
        public Texture2D texture { get; private set; }
        protected KeyFrame[] keyFrames;
        protected TimeSpan m_duration;
        public TimeSpan Duration => m_duration;

        public Animation(string texture, int frames) 
        {
            keyFrames = new KeyFrame[frames];

            this.texture = AssetManager.GetTexture(texture);
            
            //add callback to assign texture when content is loaded
        }

        protected struct KeyFrame
        {
            public TimeSpan time;
            public Rectangle bounds;
        }

        public Rectangle Evaluate(TimeSpan time)
        {
            for (int i = 0; i < keyFrames.Length - 1; i++)
            {
                if (time < keyFrames[i+1].time)
                {
                    return keyFrames[i].bounds;
                }
            }
            return keyFrames[keyFrames.Length - 1].bounds;
        }
    }
}
