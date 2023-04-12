using Microsoft.Xna.Framework;
using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.CompilerServices;
using System.Text;

namespace Baba.Particles.EmissionTypes
{
    public struct Circle
    {
        public float radius;
        public Vector2 center;

        public Circle(float radius, Vector2 center)
        {
            this.radius = radius;
            this.center = center;
        }
    }
}