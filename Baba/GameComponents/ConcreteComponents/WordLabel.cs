using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.ConcreteComponents
{
    /// <summary>
    /// An enum representing the which word this entity is. Ex. Rock, wall, baba. Used for determining rules and sprite
    /// </summary>
    public class WordLabel : Component
    {
        public WordType item { get; set; }

        public WordLabel(WordType item)
        {
            this.item = item;
        }
    }
}