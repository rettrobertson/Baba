using Microsoft.Xna.Framework;
using System;

namespace Baba.Particles.EmissionTypes
{
    public class RectangleEmitter : EmissionShape
    {
        private Rectangle rectangle;
        public RectangleEmitter(Rectangle rectangle, EmitType type) : base(type)
        {
            this.rectangle = rectangle;
        }

        protected override Vector2 GetEmissionPointArea()
        {
            return rectangle.center - new Vector2(random.NextSingle() * rectangle.width, random.NextSingle() * rectangle.height) + rectangle.bottomLeft;
        }

        protected override Vector2 GetEmissionPointEdge()
        {
            float rand = random.NextSingle() * rectangle.perimeter;

            if (rand < rectangle.height) // Left wall
            {
                return rectangle.bottomLeft + Vector2.UnitY * rand;
            }
            else if (rand < rectangle.height + rectangle.width) // Bottom wall
            {
                return rectangle.bottomLeft + Vector2.UnitX * (rand - rectangle.height);
            }
            else if (rand < rectangle.height * 2 + rectangle.width) // Right wall
            {
                return rectangle.bottomRight + Vector2.UnitY * (rand - rectangle.width - rectangle.height);
            }
            else // Top wall
            {
                return rectangle.topLeft + Vector2.UnitX * (rand - rectangle.width - rectangle.height * 2);
            }
        }
    }
}
