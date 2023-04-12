using Baba.GameComponents.ConcreteComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.Systems
{
    public abstract class System
    {
        public System(params Type[] types) 
        {
            foreach (Type type in types)
            {
                ComponentRouterSystem.RegisterComponentListener(type, EntityChanged);
            }
        }

        public abstract void Update(GameTime time);
        public virtual void Draw(SpriteBatch spriteBatch) { }

        protected virtual void EntityChanged(Entity entity, Component component, Entity.ComponentChange change) { }
    }
}
