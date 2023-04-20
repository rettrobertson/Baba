using System;

namespace Baba.Animation
{
    public class ThreeFrameAnimation : ConstantAnimation
    {
        private const float SPEED = .2f;

        public ThreeFrameAnimation(string texture) : base(texture, 3, TimeSpan.FromSeconds(SPEED), 24, 24)
        {
        }
    }
}
