using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    public class MoveSystem : System
    {
        List<Entity> controlledEntities;
        List<Entity> pushableEntities;

        public MoveSystem(NewGameView view) : base(view, typeof(You), typeof(Push))
        {
            controlledEntities = new List<Entity>();
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {
            List<Entity> list = entity.GetComponent<You>() != null ? controlledEntities : pushableEntities;

            if (change == Entity.ComponentChange.ADD)
            {
                list.Add(entity);
            }
            else
            {
                list.Remove(entity);
            }
        }

        public override void Update(GameTime time)
        {
        }

        public void MoveEntities()
        {

        }
    }
}
