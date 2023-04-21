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
        private enum direction
        {
            Up,
            Down,
            Left,
            Right
        }


        public MoveSystem(NewGameView view) : base(view, typeof(You), typeof(Push))
        {
            controlledEntities = new List<Entity>();
            pushableEntities = new List<Entity>();
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

        public override void Update(GameTime gameTime)
        {
            
        }
       
        public void moveEntity( GameTime gameTime, string command)
        {
            foreach (Entity entity in controlledEntities)
            {
                Vector2 currPos = entity.transform.position;
                switch (command)
                {
                    case "Up":
                        entity.transform.position = currPos + new Vector2(0, -1);
                        break;
                    case "Down":
                        entity.transform.position = currPos + new Vector2(0, 1);
                        break;
                    case "Left":
                        entity.transform.position = currPos + new Vector2(-1, 0);
                        break;
                    case "Right":
                        entity.transform.position = currPos + new Vector2(1, 0);
                        break;
                }
            }
            

        }
    }
}
