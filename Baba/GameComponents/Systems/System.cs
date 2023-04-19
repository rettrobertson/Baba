using Microsoft.Xna.Framework;
using System;

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

        public virtual void Update(GameTime time) { }
        public virtual void Draw() { }

        protected virtual void EntityChanged(Entity entity, Component component, Entity.ComponentChange change) { }
    }
}
