using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.Systems
{
    public class WinSystem : System
    {
        private List<You> yous;
        private List<Win> wins;
        public WinSystem(NewGameView view) : base(view, typeof(Win), typeof(You))
        {
            yous = new();
            wins = new();
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {

            if (change == Entity.ComponentChange.ADD)
            {
                if (component.GetType() == typeof(You))
                {
                    yous.Add(component as You);
                }
                else if (component.GetType() == typeof(Win))
                {
                    wins.Add(component as Win);
                }
            }
            else
            {
                if (component.GetType() == typeof(You))
                {
                    yous.Remove(component as You);
                }
                else if (component.GetType() == typeof(Win))
                {
                    wins.Remove(component as Win);
                }
            }
        }

        public bool Win()
        {
            foreach (You you in yous)
            {
                foreach (Win win in wins)
                {
                    if (you.entity.transform.position.X == win.entity.transform.position.X && you.entity.transform.position.Y == win.entity.transform.position.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Reset()
        {
            yous.Clear();
            wins.Clear();
        }
    }
}
