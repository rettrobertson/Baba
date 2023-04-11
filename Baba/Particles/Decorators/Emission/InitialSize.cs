using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.Decorators.Emission
{
    public class InitialSize : EmissionDecorator
    {
        private float m_minSize;
        private float m_maxSize;
        private Random m_random;

        public InitialSize(float size) : this(size, size)
        {
        }

        public InitialSize(float minSize, float maxSize)
        {
            m_random = new Random();
            m_minSize = minSize;
            m_maxSize = maxSize;
        }

        internal override void Apply(Particle particle)
        {
            float size = m_random.NextSingle() * (m_maxSize - m_minSize) + m_minSize;
            particle.size = size;
        }
    }
}
