using Baba.GameComponents.ConcreteComponents;
using System.Collections.Generic;

namespace Baba.GameComponents
{
    public sealed class Entity
    {
        private static uint nextID = 0;

        public uint id { get; }
        
        private List<Component> components;

        /// <summary>
        /// The transform component attached to this entity. This forces every entity to have a position
        /// </summary>
        public Transform transform { get; }

        /// <summary>
        /// Delegate to notify whenever a component is changed on this entity. This can by a system to let other systems know when components are added
        /// </summary>
        /// <param name="entity">The entity to which the change was applied</param>
        /// <param name="component">The added or removed component</param>
        public delegate void ComponentChangedCallback(Entity entity, Component component, ComponentChange change);
        public static event ComponentChangedCallback onComponentAdded;
        public static event ComponentChangedCallback onComponentRemoved;

        public enum ComponentChange { ADD, REMOVE };

        public Entity()
        {
            id = nextID++;
            components = new List<Component>();
            //transform = new Transform();
            AddComponent(transform);
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.entity = this;
            onComponentAdded?.Invoke(this, component, ComponentChange.ADD);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (component.GetType().IsAssignableTo(typeof(T)))
                {
                    return (T)component;
                }
            }
            return null;
        }

        public void RemoveComponent(Component component)
        {
            components.Remove(component);
            onComponentRemoved?.Invoke(this, component, ComponentChange.REMOVE);
        }

        public List<T> RemoveAll<T>() where T : Component
        {
            List<T> list = new List<T>();

            foreach (Component component in components)
            {
                if (component.GetType().IsAssignableTo(typeof(T)))
                {
                    RemoveComponent(component);
                    list.Add(component as T);
                }
            }

            return list;
        }
    }
}
