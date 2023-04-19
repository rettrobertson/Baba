using Microsoft.Xna.Framework;
using System;

namespace Baba.GameComponents.ConcreteComponents
{
    public class SpriteAnimator : Component
    {
        public Animation.Animation animation;
        public TimeSpan time;
        public Color color;
        public Sprite sprite;

        public SpriteAnimator(Sprite sprite)
        {
            this.sprite = sprite;
        }
    }
}
