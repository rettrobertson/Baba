using Baba.Views;
using Baba.GameComponents.ConcreteComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.Systems
{
    public class SinkSystem : System
    {
        private List<Sink> sinks;
        private List<ItemLabel> items;
        private NewGameView view;

        public SinkSystem(NewGameView view) : base(view, typeof(Sink), typeof(ItemLabel))
        {
            sinks = new();
            items = new();
            this.view = view;
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {

            if (change == Entity.ComponentChange.ADD)
            {
                if (component.GetType() == typeof(ItemLabel))
                {
                    items.Add(component as ItemLabel);
                }
                else if (component.GetType() == typeof(Sink))
                {
                    sinks.Add(component as Sink);
                }
            }
            else
            {
                if (component.GetType() == typeof(ItemLabel))
                {
                    items.Remove(component as ItemLabel);
                }
                else if (component.GetType() == typeof(Sink))
                {
                    sinks.Remove(component as Sink);
                }
            }
        }
        
        public void Check(AudioSystem system)
        {
            List<(Sink, ItemLabel)> temp = new List<(Sink, ItemLabel)>();
            foreach (ItemLabel item in items)
            {
                if (item.entity.GetComponent<Sink>() == null)
                {
                    foreach (Sink sink in sinks)
                    {
                        if (item.entity.transform.position.X == sink.entity.transform.position.X && item.entity.transform.position.Y == sink.entity.transform.position.Y)
                        {
                            temp.Add((sink, item));
                        }
                    }
                }
            }
            foreach((Sink s, ItemLabel i) in temp)
            {
                system.PlayHurt();
                /*Transform t = s.entity.transform;
                s.entity.RemoveAll<Component>();
                s.entity.AddComponent(t);
                s.entity.AddComponent(new ItemLabel(ItemType.Empty));

                t = i.entity.transform;
                i.entity.RemoveAll<Component>();
                i.entity.AddComponent(t);
                i.entity.AddComponent(new ItemLabel(ItemType.Empty));*/
                view.ruleSystem.ReturnComponents(s.entity.RemoveAll<RuleComponent>());
                view.ruleSystem.ReturnComponents(i.entity.RemoveAll<RuleComponent>());
            }
        }

        public override void Reset()
        {
            sinks.Clear();
            items.Clear();
        }
    }
}
