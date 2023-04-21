using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.Systems
{
    internal class UndoSystem : System
    {
        public event Action OnUndo;

        private List<Transform> transforms;
        private Stack<Dictionary<uint, (Vector2, ItemType?)>> snapshots;
        public UndoSystem(NewGameView view) : base(view, typeof(Transform))
        {
            snapshots = new Stack<Dictionary<uint, (Vector2, ItemType?)>>();
            transforms = new List<Transform>();
        }
        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {

            if (change == Entity.ComponentChange.ADD)
            {
                transforms.Add(component as Transform);
            }
            else
            {
                transforms.Remove(component as Transform);
            }
        }

        public void ArrowKeyPress()
        {
            Dictionary<uint, (Vector2, ItemType?)> temp = new();
            foreach (Transform transform in transforms)
            {
                var turnary = transform.entity.GetComponent<ItemLabel>();
                if (turnary == null)
                {
                    temp.Add(transform.entity.id, (transform.position, null));
                }
                else
                {
                    temp.Add(transform.entity.id, (transform.position, transform.entity.GetComponent<ItemLabel>().item));
                }
            }
            snapshots.Push(temp);
        }

        public void UndoKeyPress()
        {
            if (snapshots.Count == 0) return;

            Dictionary<uint, (Vector2, ItemType?)> temp = snapshots.Pop();
            foreach (Transform transform in transforms)
            {
                transform.position = temp[transform.entity.id].Item1;
                if (temp[transform.entity.id].Item2 != null)
                {
                    transform.entity.GetComponent<ItemLabel>().item = (ItemType)temp[transform.entity.id].Item2;
                }
            }
            OnUndo?.Invoke();
        }

        public override void Reset()
        {
            snapshots.Clear();
            transforms.Clear();
        }
    }
}
