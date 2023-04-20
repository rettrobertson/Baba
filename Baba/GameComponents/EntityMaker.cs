using Baba.GameComponents.ConcreteComponents;
using System;
using System.Collections.Generic;

namespace Baba.GameComponents
{
    public static class EntityMaker
    {
        private static Dictionary<WordType, RuleType> wordRuleTypes = new Dictionary<WordType, RuleType>();

        public static Entity MakeEntity(ItemType type)
        {
            Entity entity = new Entity();

            entity.AddComponent(new ItemLabel(type));

            //TODO: foreach component in rules[type] addcomponent(component)

            return entity;
        }
        public static Entity MakeEntity(WordType type)
        {
            Entity entity = new Entity();
            RuleType ruleType = RuleType.Is;

            if ((int)type >= 32)
            {
                ruleType = RuleType.Attribute;
            }
            else if ((int)type >= 1)
            {
                ruleType = RuleType.Item;
            }
            else
            {
                ruleType = RuleType.Is;
            }

            entity.AddComponent(new WordLabel(type, ruleType));
            return entity;
        }
    }
}