using Microsoft.Xna.Framework;

namespace Baba.Particles.EmissionTypes
{
    public class PointEmitter : EmissionShape
    {
        public PointEmitter(EmitType type) : base(type)
        {
        }

        protected override Vector2 GetEmissionPointEdge()
        {
            return Vector2.Zero;
        }

        protected override Vector2 GetEmissionPointArea()
        {
            return Vector2.Zero;
        }
    }
}
