using Baba.Views;
using Microsoft.Xna.Framework;
using System;

namespace Baba.GameComponents.Systems
{
    public abstract class System
    {
        public System(NewGameView view, params Type[] types) 
        {
            foreach (Type type in types)
            {
                view.router.RegisterComponentListener(type, EntityChanged);
            }
        }

        public virtual void Update(GameTime time) { }
        public virtual void Draw() { }
        public abstract void Reset();
        protected virtual void EntityChanged(Entity entity, Component component, Entity.ComponentChange change) { }
    }
}