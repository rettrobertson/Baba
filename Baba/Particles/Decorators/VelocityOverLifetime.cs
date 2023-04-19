using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.Decorators
{
    public class VelocityOverLifetime : ParticleDecorator
    {
        Vector2 startVelocity;
        Vector2 endVelocity;

        private VelocityOverLifetime(Vector2 startVelocity, Vector2 endVelocity)
        {
            this.startVelocity = startVelocity;
            this.endVelocity = endVelocity;
        }

        internal override void UpdateParticle(GameTime time, Particle particle)
        {
            particle.velocity = Vector2.Lerp(startVelocity, endVelocity, particle.lifetime / particle.totalLifetime);
        }
        public static VelocityOverLifetime FromEndPoints(Vector2 startVelocity, Vector2 endVelocity)
        {
            return new VelocityOverLifetime(startVelocity, endVelocity);
        }
    }
}
