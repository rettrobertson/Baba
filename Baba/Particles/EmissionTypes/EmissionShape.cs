using Microsoft.Xna.Framework;
using System;

namespace Baba.Particles.EmissionTypes
{
    public abstract class EmissionShape
    {
        public enum EmitType { EDGE, AREA }
        protected EmitType type;
        protected Random random;

        public EmissionShape(EmitType type)
        {
            this.type = type;
            random = new Random();
        }

        public Vector2 GetEmissionPoint()
        {
            switch (type)
            {
                case EmitType.EDGE:
                    return GetEmissionPointEdge();
                case EmitType.AREA:
                    return GetEmissionPointArea();
            }
            return Vector2.Zero;
        }
       
        protected abstract Vector2 GetEmissionPointEdge();
        protected abstract Vector2 GetEmissionPointArea();
    }
}
