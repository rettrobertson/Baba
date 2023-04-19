using System;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    /// <summary>
    /// System for keeping track of components on entities
    /// 
    /// THIS NEEDS TO CHANGE!!!!! 
    /// Right now it is a singleton so every view registers to the same one and everything breaks
    /// </summary>
    public class ComponentRouterSystem
    {
        private Dictionary<Type, List<Entity.ComponentChangedCallback>> callbacks = new Dictionary<Type, List<Entity.ComponentChangedCallback>>();

        public ComponentRouterSystem()
        {
            Entity.onComponentAdded += Notify;
            Entity.onComponentRemoved += Notify;
        }

        private void Notify(Entity entity, Component component, Entity.ComponentChange change)
        {
            if (!callbacks.ContainsKey(component.GetType())) return;

            List<Entity.ComponentChangedCallback> listeners = callbacks[component.GetType()];
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i](entity, component, change);
            }
        }

        /// <summary>
        /// Register a callback to be notified when a component of the given type is added or removed
        /// </summary>
        /// <typeparam name="T">Generic type of component to listen to</typeparam>
        /// <param name="callback">Callback for when a component is changed</param>
        public void RegisterComponentListener<T>(Entity.ComponentChangedCallback callback)
        {
            RegisterComponentListener(typeof(T), callback);
        }

        /// <summary>
        /// Register a callback to be notified when a component of the given type is added or removed
        /// </summary>
        /// <param name="type">Type of component to listen to</param>
        /// <param name="callback">Callback for changed components</param>
        public void RegisterComponentListener(Type type, Entity.ComponentChangedCallback callback)
        {
            if (!callbacks.ContainsKey(type))
            {
                callbacks.Add(type, new List<Entity.ComponentChangedCallback>());
            }
            
            callbacks[type].Add(callback);
        }

    }
}
