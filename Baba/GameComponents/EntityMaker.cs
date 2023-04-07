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
    }
}