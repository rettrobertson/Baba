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
        public SinkSystem(NewGameView view) : base(view, typeof(Sink), typeof(ItemLabel))
        {
            sinks = new();
            items = new();
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
        
        public void Check()
        {
            foreach (ItemLabel item in items)
            {
                if (item.entity.GetComponent<Sink>() == null)
                {
                    foreach (Sink sink in sinks)
                    {
                        if (item.entity.transform.position.X == sink.entity.transform.position.X && item.entity.transform.position.Y == sink.entity.transform.position.Y)
                        {
                            item.entity.RemoveAll<Component>();
                            item.entity.AddComponent(item.entity.transform);
                            item.entity.AddComponent(new ItemLabel(ItemType.Empty));

                            sink.entity.RemoveAll<Component>();
                            sink.entity.AddComponent(sink.entity.transform);
                            sink.entity.AddComponent(new ItemLabel(ItemType.Empty));
                        }
                    }
                }
            }
        }

        public override void Reset()
        {
            sinks.Clear();
            items.Clear();
        }
    }
}
