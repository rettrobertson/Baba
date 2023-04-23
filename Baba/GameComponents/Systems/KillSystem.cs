using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    public class KillSystem : System
    {
        private List<Kill> kills;
        private List<You> yous;
        private NewGameView view;

        public KillSystem(NewGameView view) : base(view, typeof(Kill), typeof(You))
        {
            kills = new();
            yous = new();
            this.view = view;
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
                view.KillEntities(y.entity);
            }
        }

        public override void Reset()
        {
            kills.Clear();
            yous.Clear();
        }
    }
}
