using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.Systems
{
    public class KillSystem : System
    {
        private List<Kill> kills;
        private List<You> yous;
        public KillSystem(NewGameView view) : base(view, typeof(Kill), typeof(You))
        {
            kills = new();
            yous = new();
        }
        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {

            if (change == Entity.ComponentChange.ADD)
            {
                if (component.GetType() == typeof(Kill))
                {
                    kills.Add(component as Kill);
                }
                else if (component.GetType() == typeof(You))
                {
                    yous.Add(component as You);
                }
            }
            else
            {
                if (component.GetType() == typeof(Kill))
                {
                    kills.Remove(component as Kill);
                }
                else if (component.GetType() == typeof(You))
                {
                    yous.Remove(component as You);
                }
            }
        }

        public void Check()
        {
            List<You> temp = new List<You>();
            foreach (You y in yous)
            {
                foreach(Kill k in kills)
                {
                    if (y.entity.transform.position.X == k.entity.transform.position.X && y.entity.transform.position.Y == k.entity.transform.position.Y)
                    {
                        temp.Add(y);
                    }
                }
            }
            foreach(You y in temp)
            {
                Transform t = y.entity.transform;
                y.entity.RemoveAll<Component>();
                y.entity.AddComponent(t);
                y.entity.AddComponent(new ItemLabel(ItemType.Empty));
            }
        }

        public override void Reset()
        {
            kills.Clear();
            yous.Clear();
        }
    }
}
