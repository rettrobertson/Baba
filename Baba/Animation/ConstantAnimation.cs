using Microsoft.Xna.Framework;
using System;

namespace Baba.Animation
{
    public class ConstantAnimation : Animation
    {
        public ConstantAnimation(string texture, int frames, TimeSpan frameTime, int frameWidth, int frameHeight) : base(texture, frames)
        {
            for (int i = 0; i < frames; i++)
            {
                keyFrames[i] = new KeyFrame() { time = frameTime * i, bounds = new Rectangle(i * frameWidth, 0, frameWidth, frameHeight) };
            }
            m_duration = frames * frameTime;
        }
    }
}
