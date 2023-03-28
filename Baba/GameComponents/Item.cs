using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents
{
    public class Item
    {
        public enum Type
        {
            Grass, 
            Hedge,
            Empty,
            Rock,
            Wall,

        }
        public Type type { get; set; }
    }
}
