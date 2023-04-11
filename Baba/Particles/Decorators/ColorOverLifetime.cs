using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.Decorators
{
    public class ColorOverLifetime : ParticleDecorator
    {
        Gradient gradient;
        internal override void UpdateParticle(GameTime time, Particle particle)
        {
            float t = particle.lifetime / particle.totalLifetime;
            particle.color = gradient.Evaluate(t);
        }

        public ColorOverLifetime(Gradient gradient)
        {
            this.gradient = gradient;
        }
    }
}
