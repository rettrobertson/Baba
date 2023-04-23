using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baba.GameComponents.Systems
{
    internal class UndoSystem : System
    {
        public event Action OnUndo;

        private Stack<Dictionary<uint, (Vector2, ItemType?)>> snapshots;
        public UndoSystem(NewGameView view) : base(view, typeof(Transform))
        {
            snapshots = new Stack<Dictionary<uint, (Vector2, ItemType?)>>();
        }

        public void ArrowKeyPress(List<Transform> transforms)
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

        public void UndoKeyPress(List<Transform> transforms)
        {   
            if (snapshots.Count == 0)
            {
                return;
            }
            Dictionary<uint, (Vector2, ItemType?)> temp = snapshots.Peek();
            foreach (Transform transform in transforms)
            {
                transform.position = temp[transform.entity.id].Item1;
                if (temp[transform.entity.id].Item2 != null)
                {
                    transform.entity.GetComponent<ItemLabel>().item = (ItemType)temp[transform.entity.id].Item2;
                }
            }
            if (snapshots.Count > 1)
            {
                snapshots.Pop();
            }
            OnUndo?.Invoke();
        }

        public override void Reset()
        {
            snapshots.Clear();
        }
    }
}
