using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.Decorators
{
    public class OpposingAcceleration : ParticleDecorator
    {
        private float strength;
        public OpposingAcceleration(float strength)
        {
            this.strength = strength;
        }

        internal override void UpdateParticle(GameTime time, Particle particle)
        {
            particle.velocity -= particle.velocity.Normalized() * strength * (float)time.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
