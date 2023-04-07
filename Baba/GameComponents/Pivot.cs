using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba
{
    public static class Pivot
    {
        public static readonly Vector2 CENTER = new Vector2(0.5f, 0.5f);
        public static readonly Vector2 TOP = new Vector2(0.5f, 1);
        public static readonly Vector2 BOTTOM = new Vector2(0.5f, 0);
        public static readonly Vector2 LEFT = new Vector2(0, 0.5f);
        public static readonly Vector2 RIGHT = new Vector2(1, 0.5f);
        public static readonly Vector2 BOTTOM_RIGHT = Vector2.UnitX;
        public static readonly Vector2 BOTTOM_LEFT = Vector2.Zero;
        public static readonly Vector2 TOP_RIGHT = Vector2.One;
        public static readonly Vector2 TOP_LEFT = Vector2.UnitY;
    }
}
