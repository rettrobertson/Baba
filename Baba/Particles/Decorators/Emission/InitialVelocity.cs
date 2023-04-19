using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.Decorators.Emission
{
    public class InitialRadialVelocity : EmissionDecorator
    {
        private int m_degreesMin;
        private int m_degreesMax;
        private float m_speedMin;
        private float m_speedMax;
        private Random m_random;

        public InitialRadialVelocity(int degreesMin, int degreesMax, float speed) : this(degreesMin, degreesMax, speed, speed)
        {
        }

        public InitialRadialVelocity(int degreesMin, int degreesMax, float speedMin, float speedMax)
        {
            m_degreesMin = degreesMin;
            m_degreesMax = degreesMax;
            m_speedMin = speedMin;
            m_speedMax = speedMax;
            m_random = new Random();
        }

        internal override void Apply(Particle particle)
        {
            float angle = MathHelper.ToRadians(m_random.Next(m_degreesMin, m_degreesMax + 1));
            float speed = m_random.NextSingle() * (m_speedMax - m_speedMin) + m_speedMin;

            particle.velocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
        }
    }

    public class InitalLinearVelocity : EmissionDecorator
    {
        Vector2 m_direction;
        private float m_speedMin;
        private float m_speedMax;
        private Random m_random;

        public InitalLinearVelocity(Vector2 direction, float speed) : this(direction, speed, speed)
        {
        }

        public InitalLinearVelocity(Vector2 direction, float speedMin, float speedMax)
        {
            m_direction = direction;
            m_speedMin = speedMin;
            m_speedMax = speedMax;
            m_random = new Random();
        }

        internal override void Apply(Particle particle)
        {
            float speed = m_random.NextSingle() * (m_speedMax - m_speedMin) + m_speedMin;

            particle.velocity = m_direction.Normalized() * speed;
        }
    }

    public class InitalOutwardVelocity : EmissionDecorator
    {
        Vector2 m_point;
        private float m_speedMin;
        private float m_speedMax;
        private Random m_random;

        public InitalOutwardVelocity(Vector2 point, float speed) : this(point, speed, speed)
        {
        }

        public InitalOutwardVelocity(Vector2 point, float speedMin, float speedMax)
        {
            m_point = point;
            m_speedMin = speedMin;
            m_speedMax = speedMax;
            m_random = new Random();
        }

        internal override void Apply(Particle particle)
        {
            float speed = m_random.NextSingle() * (m_speedMax - m_speedMin) + m_speedMin;

            particle.velocity = (particle.position - m_point).Normalized() * speed;
        }
    }
}
