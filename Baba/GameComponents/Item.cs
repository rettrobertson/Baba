using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents
{
    public class Item : IItem
    {
        public ItemType type { get; set; }
        private IItem[,] grid;
        private int x;
        private int y;
        public Item(ItemType type)
        {
            this.type = type;
            
        }
        public void AddLocation(IItem[,] grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void Draw(GameTime gameTime)
        {

            //How to make so that it can draw multiple types with as little code as possible
        }

        public bool Right()
        {
            throw new NotImplementedException();
        }

        public bool Left()
        {
            throw new NotImplementedException();
        }

        public bool Up()
        {
            throw new NotImplementedException();
        }

        public bool Down()
        {
            throw new NotImplementedException();
        }
    }
}
