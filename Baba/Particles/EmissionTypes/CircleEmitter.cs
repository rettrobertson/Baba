//using BreakoutGame.Engine.Physics.Shapes;
using Microsoft.Xna.Framework;
using System;

namespace Baba.Particles.EmissionTypes
{
    public class CircleEmitter : EmissionShape
    {
        private Circle circle;

        public CircleEmitter(Circle circle, EmitType type) : base(type) 
        {
            this.circle = circle;
        }

        protected override Vector2 GetEmissionPointArea()
        {
            float angle = MathHelper.ToRadians(random.Next(0, 360));
            return circle.center + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * circle.radius * random.NextSingle();
        }

        protected override Vector2 GetEmissionPointEdge()
        {
            float angle = MathHelper.ToRadians(random.Next(0, 360));
            return circle.center + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * circle.radius;
        }
    }
}