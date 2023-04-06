namespace Baba.GameComponents
{
    public static class EntityMaker
    {
        public static Entity MakeEntity(ItemType type)
        {
            Entity entity = new Entity();
            //foreach component in rules[type] addcomponent(component)

            return entity;
        }
        public static Entity MakeEntity(WordType type)
        {
            Entity entity = new Entity();
            // I added this because words are also entities
            return entity;
        }
    }
}