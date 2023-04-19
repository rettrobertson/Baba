using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.Particles.EmissionTypes
{
    /// <summary>
    /// A rectangle centered at the bottom-left corner
    /// </summary>
    public struct Rectangle
    {
        public readonly float x;
        public readonly float y;
        public readonly float width;
        public readonly float height;
        public readonly float perimeter;

        public Vector2 bottomRight => new Vector2(x + width, y);
        public Vector2 bottomLeft => new Vector2(x, y);
        public Vector2 topRight => new Vector2(x + width, y + height);
        public Vector2 topLeft => new Vector2(x, y + height);

        public Vector2 center => new Vector2(x + width / 2, y + height / 2);

        public Rectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            perimeter = width * 2 + height * 2;
        }
    }
}
