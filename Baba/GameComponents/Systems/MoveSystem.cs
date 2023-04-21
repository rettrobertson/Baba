using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Baba.GameComponents.Systems
{
    public class MoveSystem : System
    {
        List<Entity> controlledEntities;
        List<Entity> pushableEntities;
        private List<List<bool>> hittables;
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
            hittables = new List<List<bool>>();
            for (int i = 0; i < 20; i++)
            {
                hittables.Add(new List<bool>());
                for (int j = 0; j < 20; j++)
                {
                    hittables[i].Add(false);
                }
            }
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
            for (int i = 0; i < controlledEntities.Count; i++)
            {
                Vector2 controlled = controlledEntities[i].transform.position;
                hittables[(int)controlled.Y][(int)controlled.X] = true;
            }
            for (int i = 0; i < pushableEntities.Count; i++)
            {
                Vector2 controlled = pushableEntities[i].transform.position;
                hittables[(int)controlled.Y][(int)controlled.X] = true;
            }
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
