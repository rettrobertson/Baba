using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException("The programmer didn't want to implement this algorithm");
        }
    }
}
