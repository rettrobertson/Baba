using Microsoft.Xna.Framework;

namespace Baba.GameComponents.ConcreteComponents
{
    public class Transform : Component
    {
        public Vector2 position;

        public Transform()
        {
        }

        public Transform(Vector2 position, Entity entity)
        {
            this.position = position;
            this.entity = entity;
        }
    }
}
