using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    public class RuleSystem : System
    {
        private Dictionary<ItemType, HashSet<AttributeType>> rules;
        private List<Entity> itemEntities;
        private List<Transform> wordsList;
        private List<Transform> isList;

        private Dictionary<AttributeType, Type> attributeComponents;
        private Dictionary<WordType, ItemType> itemWords;
        private Dictionary<WordType, AttributeType> attributeWords;

        private Dictionary<Type, ObjectPool> objectPools;
        private const int poolSize = 20;

        private Dictionary<ItemType, ItemType> transformations;
        public event Action onTransformationsFinished;

        public RuleSystem(NewGameView view) : base(view, typeof(WordLabel), typeof(ItemLabel))
        {
            rules = new Dictionary<ItemType, HashSet<AttributeType>>();
            isList = new List<Transform>();
            objectPools = new Dictionary<Type, ObjectPool>();
            itemEntities = new List<Entity>();
            wordsList = new List<Transform>();
            transformations = new Dictionary<ItemType, ItemType>();

            attributeComponents = new Dictionary<AttributeType, Type>()
            {
                { AttributeType.Best, typeof(Best)},
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

            attributeWords = new Dictionary<WordType, AttributeType>()
            {
                { WordType.You, AttributeType.You },
                { WordType.Move, AttributeType.Move },
                { WordType.Win, AttributeType.Win },
                { WordType.Sink, AttributeType.Sink },
                { WordType.Kill, AttributeType.Kill },
                { WordType.Slip, AttributeType.Slip },
                { WordType.Best, AttributeType.Best },
                { WordType.Hot, AttributeType.Hot },
                { WordType.Melt, AttributeType.Melt },
                { WordType.Push, AttributeType.Push },
                { WordType.Stop, AttributeType.Stop }
            };
            itemWords = new Dictionary<WordType, ItemType>()
            {
                { WordType.Baba, ItemType.Baba },
                { WordType.Lava, ItemType.Lava },
                { WordType.Kiki, ItemType.Kiki },
                { WordType.Rock, ItemType.Rock },
                { WordType.Bone, ItemType.Bone },
                { WordType.Anni, ItemType.Anni },
                { WordType.Flag, ItemType.Flag },
                { WordType.Wall, ItemType.Wall },
                { WordType.Love, ItemType.Love },
                { WordType.Ice, ItemType.Ice },
                { WordType.Water, ItemType.Water },
            };

            foreach (Type type in attributeComponents.Values)
            {
                objectPools.Add(type, new ObjectPool(poolSize, type));
            }
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {
            WordLabel word = component as WordLabel;

            if (word != null)
            {
                List<Transform> list = word.ruleType == RuleType.Is ? isList : wordsList;

                if (change == Entity.ComponentChange.ADD)
                {
                    list.Add(entity.transform);
                }
                else
                {
                    list.Remove(entity.transform);
                }
            }

            ItemLabel item = component as ItemLabel;

            if (item != null)
            {
                if (change == Entity.ComponentChange.ADD)
                {
                    itemEntities.Add(entity);
                }
                else
                {
                    itemEntities.Remove(entity);
                }
            }
        }

        //Update the game rules based on the position of entities in the world
        public void UpdateRules()
        {
            //Clear all existing rules

            foreach (HashSet<AttributeType> attributes in rules.Values)
            {
                attributes.Clear();
            }

            //Check each is word for updates
            for (int j = 0; j < isList.Count; j++)
            {
                WordLabel top = null;
                WordLabel left = null;
                WordLabel bottom = null;
                WordLabel right = null;

                for (int i = 0; i < wordsList.Count; i++)
                {
                    Transform transform = wordsList[i];

                    if (transform.position == isList[j].position - Vector2.UnitY)
                    {
                        top = transform.entity.GetComponent<WordLabel>();
                    }
                    else if (transform.position == isList[j].position - Vector2.UnitX)
                    {
                        left = transform.entity.GetComponent<WordLabel>();
                    }
                    else if (transform.position == isList[j].position + Vector2.UnitY)
                    {
                        bottom = transform.entity.GetComponent<WordLabel>();
                    }
                    else if (transform.position == isList[j].position + Vector2.UnitX)
                    {
                        right = transform.entity.GetComponent<WordLabel>();
                    }
                }

                if (top != null && bottom != null && top.ruleType == RuleType.Item)
                {
                    if (bottom.ruleType == RuleType.Attribute)
                    {
                        AddRule(itemWords[top.item], attributeWords[bottom.item]);
                    }
                    else if (right.ruleType == RuleType.Item)
                    {
                        transformations.TryAdd(itemWords[top.item], itemWords[bottom.item]);
                    }
                }

                if (left != null && right != null && left.ruleType == RuleType.Item)
                {
                    if (right.ruleType == RuleType.Attribute)
                    {
                        AddRule(itemWords[left.item], attributeWords[right.item]);
                    }
                    else if (right.ruleType == RuleType.Item)
                    {
                        transformations.TryAdd(itemWords[left.item], itemWords[right.item]);
                    }
                }
            }

            //Apply all rules
            PerformTransformations();
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

        private void PerformTransformations()
        {
            bool transformed = false;

            foreach (Entity entity in itemEntities)
            {
                ItemLabel label = entity.GetComponent<ItemLabel>();
                if (transformations.ContainsKey(label.item))
                {
                    label.item = transformations[label.item];
                    transformed = true;
                }
            }
            transformations.Clear();

            if (transformed)
            {
                onTransformationsFinished?.Invoke();
            }
        }

        public void ApplyRules()
        {
            foreach (Entity entity in itemEntities)
            {
                List<RuleComponent> components = entity.RemoveAll<RuleComponent>();
                foreach (RuleComponent component in components)
                {
                    objectPools[component.GetType()].ReturnObject(component);
                }
            }

            foreach (Entity entity in itemEntities)
            {
                ItemLabel itemLabel = entity.GetComponent<ItemLabel>();

                if (!rules.ContainsKey(itemLabel.item)) continue;

                foreach (AttributeType attribute in rules[itemLabel.item])
                {
                    // This line is pretty hard to read, but it gets the component's object pool, removes a component, and adds it to the object
                    entity.AddComponent(objectPools[attributeComponents[attribute]].GetObject<Component>());
                }
            }
        }

        public void SetEmpty()
        {

        }

        public override void Update(GameTime time)
        {
            
        }

        public override void Reset()
        {
            isList.Clear();
            rules.Clear();

            for (int i = 0; i < itemEntities.Count; i++) 
            {
                List<RuleComponent> removedComponents = itemEntities[i].RemoveAll<RuleComponent>();
                foreach (RuleComponent component in removedComponents)
                {
                    objectPools[component.GetType()].ReturnObject(component);
                }
            }
            itemEntities.Clear();
        }
    }
}
