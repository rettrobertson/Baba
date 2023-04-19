using System;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    /// <summary>
    /// System for keeping track of components on entities
    /// </summary>
    public static class ComponentRouterSystem
    {
        private static Dictionary<Type, List<Entity.ComponentChangedCallback>> callbacks = new Dictionary<Type, List<Entity.ComponentChangedCallback>>();

        public static void Initialize()
        {
            Entity.onComponentAdded += Notify;
            Entity.onComponentRemoved += Notify;
        }

        private static void Notify(Entity entity, Component component, Entity.ComponentChange change)
        {
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
        public static void RegisterComponentListener<T>(Entity.ComponentChangedCallback callback)
        {
            RegisterComponentListener(typeof(T), callback);
        }

        /// <summary>
        /// Register a callback to be notified when a component of the given type is added or removed
        /// </summary>
        /// <param name="type">Type of component to listen to</param>
        /// <param name="callback"></param>
        public static void RegisterComponentListener(Type type, Entity.ComponentChangedCallback callback)
        {
            if (callbacks.ContainsKey(type))
            {
                callbacks[type].Add(callback);
            }
            else
            {
                callbacks.Add(type, new List<Entity.ComponentChangedCallback>());
            }
        }

    }
}
