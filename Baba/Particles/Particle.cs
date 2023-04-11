using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles
{
    internal class Particle
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public float rotation { get; set; }
        public float angularVelocity { get; set; }
        public Color color { get; set; } = Color.White;
        public float lifetime { get; set; }
        public float totalLifetime { get; }
        public float size { get; set; }

        public Particle(float lifetime)
        {
            totalLifetime = lifetime;
        }

        /// <summary>
        /// Updates the particles position and rotation based on velocity
        /// </summary>
        /// <returns>Returns true if the particle is dead, false if it is alive</returns>
        public bool Update(GameTime time)
        {
            if (lifetime >= totalLifetime) return true;

            position += velocity * (float)time.ElapsedGameTime.TotalSeconds;
            rotation += angularVelocity * (float)time.ElapsedGameTime.TotalSeconds;

            return false;
        }
    }
}
