using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.Decorators
{
    public abstract class ParticleDecorator
    {
        internal abstract void UpdateParticle(GameTime time, Particle particle);
    }
}
