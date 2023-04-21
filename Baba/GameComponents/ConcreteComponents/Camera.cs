using Baba.GameComponents;
using Microsoft.Xna.Framework;

namespace Baba.GameComponents.ConcreteComponents
{
    public class Camera : Component
    {
        /// <summary>
        /// How many screen pixels are in a single unit
        /// </summary>
        public int renderScale = 80;
        public Vector2 input;
        private GraphicsDeviceManager m_graphics;
        public bool enableMovement = false;
    }
}
