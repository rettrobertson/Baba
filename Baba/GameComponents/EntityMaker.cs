using Baba.GameComponents.ConcreteComponents;

namespace Baba.GameComponents
{
    public static class EntityMaker
    {
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
            //Changed this to work
            entity.AddComponent(new WordLabel(type, RuleType.Is));
            // I added this because words are also entities
            return entity;
        }
    }
}