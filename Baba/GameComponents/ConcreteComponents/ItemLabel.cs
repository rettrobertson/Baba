using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.ConcreteComponents
{
    /// <summary>
    /// An enum representing the which object this entity is. Ex. Rock, wall, baba. Used for determining rules and sprite
    /// </summary>
    public class ItemLabel : Component
    {
        public ItemType item { get; set; }

        public ItemLabel(ItemType item)
        {
            this.item = item;
        }
    }
}
