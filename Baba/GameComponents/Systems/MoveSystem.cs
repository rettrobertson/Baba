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
        List<Entity> wallEntities;
        private Entity[,] hittables;
        

        public MoveSystem(NewGameView view) : base(view, typeof(You), typeof(Push), typeof(Stop))
        {
            controlledEntities = new List<Entity>();
            pushableEntities = new List<Entity>();
            wallEntities = new List<Entity>();
            hittables = new Entity[20,20];
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
            for (int i = 0; i < controlledEntities.Count; i++)
            {
                Vector2 controlled = controlledEntities[i].transform.position;
                hittables[(int)controlled.Y, (int)controlled.X] = controlledEntities[i];
            }
            for (int i = 0; i < pushableEntities.Count; i++)
            {
                Vector2 controlled = pushableEntities[i].transform.position;
                hittables[(int)controlled.Y, (int)controlled.X] = pushableEntities[i];
            }
            for (int i = 0; i < wallEntities.Count; i++)
            {
                Vector2 controlled = pushableEntities[i].transform.position;
                hittables[(int)controlled.Y, (int)controlled.X] = wallEntities[i];

            }
            foreach (Entity entity in controlledEntities)
            {
                Vector2 currPos = entity.transform.position;
                Vector2 newPos = currPos;
                switch (command)
                {
                    case "Up":
                        newPos = currPos + new Vector2(0, -1);
                        

                        break;
                    case "Down":
                        newPos = currPos + new Vector2(0, 1);
                        
                        
                        break;
                    case "Left":
                        newPos = currPos + new Vector2(-1, 0);
                        
                        break;
                    case "Right":
                        newPos = currPos + new Vector2(1, 0);
                        break;
                    
                }

                if (canMove(newPos, command) && newPos != currPos)
                {
                    if (hittables[(int)newPos.Y, (int)newPos.X] != null)
                    {
                        move(hittables[(int)newPos.Y, (int)newPos.X], command);
                    }
                    entity.transform.position = newPos;
                }
            }
        }


        private bool canMove(Vector2 newPos, string direction)
        {
            if (newPos.X > 20 || newPos.X < 0 || newPos.Y > 20 || newPos.Y < 0)
            {
                return false;
            }
            if (hittables[(int)newPos.Y, (int)newPos.X] == null)
            {
                return true;
            }
            else
            {
                switch (direction)
                {
                    case "Up":
                        return canMove(newPos + new Vector2(0, -1), direction);
                    case "Down":
                        return canMove(newPos + new Vector2(0, 1), direction);
                    case "Left":
                        return canMove(newPos + new Vector2(-1, 0), direction);;
                    case "Right":
                        return canMove(newPos + new Vector2(1, 0), direction);
                }
            }
            return false;
        }
        private void move(Entity entity, string direction)
        {
            Vector2 currPos = entity.transform.position;
            Vector2 newPos = currPos;
            switch (direction)
            {
                case "Up":
                    newPos = currPos + new Vector2(0, -1);


                    break;
                case "Down":
                    newPos = currPos + new Vector2(0, 1);


                    break;
                case "Left":
                    newPos = currPos + new Vector2(-1, 0);

                    break;
                case "Right":
                    newPos = currPos + new Vector2(1, 0);
                    break;

            }
            if (hittables[(int)newPos.Y, (int)newPos.X] != null)
            {
                move(hittables[(int)newPos.Y, (int)newPos.X], direction);
            }
            entity.transform.position = newPos;
        }
        public override void Reset()
        {
            controlledEntities.Clear();
            
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    hittables[i, j] = null;
                }
            }

            pushableEntities.Clear();
        }

    }
}
