using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;

namespace Galaga.Input
{
    public class Button
    {
        private Rectangle rect;

        public Texture2D toptex { get; set; }

        public Button(Rectangle rect)
        {
            this.rect= rect;
        }
        public Button(int x, int y, int width, int height)
        {
            this.rect = new Rectangle(x, y, width, height);
        }
        
        public int GetX()
        {
            return rect.X;
        }
        public int GetY()
        {
            return rect.Y;
        }
        public int GetWidth()
        {
            return rect.Width;
        }
        public int GetHeight()
        {
            return rect.Height;
        }
    }
}
