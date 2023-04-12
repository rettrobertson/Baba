using Baba.GameComponents.ConcreteComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    public class RuleSystem : System
    {
        private Dictionary<ItemType, HashSet<AttributeType>> rules;
        private List<Entity> dummyEntities;

        private List<Transform> isList;

        private Dictionary<AttributeType, Type> attributeComponents;
        
        private Dictionary<Type, ObjectPool> objectPools;
        private const int poolSize = 20;

        public RuleSystem() : base(typeof(ItemLabel))
        {
            rules = new Dictionary<ItemType, HashSet<AttributeType>>();
            isList = new List<Transform>();

            attributeComponents = new Dictionary<AttributeType, Type>()
            {
                { AttributeType.Best, typeof(Best)},
                { AttributeType.Empty, typeof(Empty)},
                { AttributeType.Hot, typeof(Hot)},
                { AttributeType.Kill, typeof(Kill)},
                { AttributeType.Melt, typeof(Melt)},
                { AttributeType.Move, typeof(Move)},
                { AttributeType.Push, typeof(Push)},
                { AttributeType.Sink, typeof(Sink)},
                { AttributeType.Slip, typeof(Slip)},
                { AttributeType.Stop, typeof(Stop)},
                { AttributeType.Win, typeof(Win)},
                { AttributeType.You, typeof(You)},
            };

            foreach (Type type in attributeComponents.Values)
            {
                Activator.CreateInstance(type);
            }
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {
            if (change == Entity.ComponentChange.ADD)
            {
                isList.Add(entity.transform);
            }
            else
            {
                isList.Remove(entity.transform);
            }
        }

        private void UpdateRules()
        {
            //Clear all existing rules

            foreach (ItemType item in rules.Keys)
            {
                foreach (AttributeType attribute in rules[item])
                {
                    foreach (Entity entity in dummyEntities)
                    {

                        entity.RemoveComponent()
                    }
                }
                rules[item].Clear();
            }

            //Check each is word for updates
            for (int j = 0; j < isList.Count; j++)
            {
                ItemLabel top = null;
                ItemLabel left = null;
                AttributeLabel bottom = null;
                AttributeLabel right = null;

                for (int i = 0; i < dummyEntities.Count; i++)
                {
                    Entity entity = dummyEntities[i];

                    if (entity.transform.position == isList[j].position + Vector2.UnitY)
                    {
                        top = entity.GetComponent<ItemLabel>();
                    }
                    else if (entity.transform.position == isList[j].position - Vector2.UnitX)
                    {
                        left = entity.GetComponent<ItemLabel>();
                    }
                    else if (entity.transform.position == isList[j].position - Vector2.UnitY)
                    {
                        bottom = entity.GetComponent<AttributeLabel>();
                    }
                    else if (entity.transform.position == isList[j].position + Vector2.UnitX)
                    {
                        right = entity.GetComponent<AttributeLabel>();
                    }
                }

                if (top != null && bottom != null)
                {
                    AddRule(top.item, bottom.attribute);
                }
                
                if (left != null && right != null)
                {
                    AddRule(left.item, right.attribute);
                }
            }

            //Apply all rules
            ApplyRules();
        }

        private void AddRule(ItemType item, AttributeType attribute)
        {
            if (rules.ContainsKey(item))
            {
                rules[item].Add(attribute);
            }
            else
            {
                rules[item] = new HashSet<AttributeType> { attribute };
            }
        }

        public void ApplyRules()
        {

        }

        public override void Update(GameTime time)
        {
        }
    }
}
