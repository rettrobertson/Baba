using Microsoft.Xna.Framework;

namespace Baba.Particles.Decorators.Emission
{
    public class InitialColor : EmissionDecorator
    {
        private Color m_color;
        public InitialColor(Color color)
        {
            m_color = color;
        }

        internal override void Apply(Particle particle)
        {
            particle.color = m_color;
        }
    }
}
