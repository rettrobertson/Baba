using Baba.GameComponents.ConcreteComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    public class SpriteRenderer
    {
        public static SpriteRenderer Instance { get; private set; }

        private Dictionary<ItemType, Texture2D> itemTextures = new Dictionary<ItemType, Texture2D>();
        private Dictionary<ItemType, Color> itemColors = new Dictionary<ItemType, Color>();

        private const int renderScale = 80; // Number of pixels in a unit. This could be changed to number of units on the screen to support more resolutions
        private GraphicsDevice m_graphics;

        private SpriteBatch m_spriteBatch;

        private List<ItemLabel> renderEntities;

        private SpriteRenderer(GraphicsDevice graphics)
        {
            m_spriteBatch = new SpriteBatch(graphics);
            m_graphics = graphics;

            renderEntities = new List<ItemLabel>();
            ComponentRouterSystem.RegisterComponentListener<ItemLabel>(EntityChanged);
        }

        public static void Initialize(GraphicsDevice graphics)
        {
            Instance = new SpriteRenderer(graphics);
        }

        private void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {
            if (change == Entity.ComponentChange.ADD)
            {
                renderEntities.Add(component as ItemLabel);
            }
            else
            {
                renderEntities.Remove(component as ItemLabel);
            }
        }

        public void Render()
        {
            for (int i = 0; i < renderEntities.Count; i++)
            {
                Render(renderEntities[i]);
            }
        }

        private void Render(ItemLabel itemComponent)
        {
            Vector2 screenPos = GameToScreenPos(itemComponent.entity.transform.position);
            Texture2D texture = itemTextures[itemComponent.item];

            int width = texture.Width * renderScale;
            int height = texture.Height * renderScale;

            m_spriteBatch.Draw(texture, new Rectangle((int)screenPos.X, (int)screenPos.Y, width, height), texture.Bounds, Color.White);
        }

        private Vector2 GameToScreenPos(Vector2 pos)
        {
            Vector2 scale = new Vector2(1, -1);
            Vector2 offset = Vector2.Zero; //For grid with origin at the center: new Vector2(m_graphics.Viewport.Bounds.Width / 2, m_graphics.Viewport.Bounds.Height / 2);

            return pos * renderScale * scale + offset;
        }
    }
}
