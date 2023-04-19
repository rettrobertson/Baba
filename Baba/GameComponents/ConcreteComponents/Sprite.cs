using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.ConcreteComponents
{
    public class Sprite : Component
    {
        public Texture2D texture;
        public Rectangle source;
        public Color color;
    }
}
