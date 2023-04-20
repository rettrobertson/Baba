using BreakoutGame.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Baba.Animation
{
    public class Animation
    {
        public Texture2D texture { get; private set; }
        protected KeyFrame[] keyFrames;
        protected TimeSpan m_duration;
        public TimeSpan Duration => m_duration;

        public Animation(string texture, int frames) 
        {
            keyFrames = new KeyFrame[frames];

            AssetManager.onContentLoaded += () =>
            {
                this.texture = AssetManager.GetTexture(texture);
            };
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
                    return keyFrames[i+1].bounds;
                }
            }
            return keyFrames[keyFrames.Length - 1].bounds;
        }
    }
}
