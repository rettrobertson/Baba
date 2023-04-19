using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Animation
{
    public class ThreeFrameAnimation : ConstantAnimation
    {
        private const float SPEED = 1;

        public ThreeFrameAnimation(string texture) : base(texture, 3, TimeSpan.FromSeconds(SPEED), 24, 24)
        {
        }
    }
}
