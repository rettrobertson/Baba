namespace Baba.GameComponents.ConcreteComponents
{
    public class AttributeLabel : Component
    {
        public AttributeType attribute { get; set; }

        public AttributeLabel(AttributeType type)
        {
            this.attribute = type;
        }
    }
}