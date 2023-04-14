using Baba.GameComponents.ConcreteComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    public class SpriteRenderer
    {
        public static SpriteRenderer Instance { get; private set; }

        private Dictionary<ItemType, Texture2D> itemTextures = new Dictionary<ItemType, Texture2D>();
        private Dictionary<ItemType, Color> itemColors = new Dictionary<ItemType, Color>();
        private Dictionary<WordType, Texture2D> wordTextures = new Dictionary<WordType, Texture2D>();
        private Dictionary<WordType, Color> wordColors = new Dictionary<WordType, Color>();

        private const int renderScale = 80; // Number of pixels in a unit. This could be changed to number of units on the screen to support more resolutions
        private GraphicsDevice m_graphics;

        private SpriteBatch m_spriteBatch;

        private List<ItemLabel> renderEntities;

        public SpriteRenderer(GraphicsDevice graphics)
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

        private void Render(WordLabel wordComponent)
        {
            Vector2 screenPos = GameToScreenPos(wordComponent.entity.transform.position);
            Texture2D texture = wordTextures[wordComponent.item];

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
        public void loadItems(ContentManager contentManager) 
        {
            itemTextures[ItemType.Flag] = contentManager.Load<Texture2D>("SpriteSheets/flag");
            itemColors[ItemType.Flag] = Color.Yellow;
            itemTextures[ItemType.Rock] = contentManager.Load<Texture2D>("SpriteSheets/rock");
            itemColors[ItemType.Rock] = Color.YellowGreen;
            itemTextures[ItemType.Wall] = contentManager.Load<Texture2D>("SpriteSheets/wall");
            itemColors[ItemType.Wall] = Color.Gray;
            itemTextures[ItemType.Grass] = contentManager.Load<Texture2D>("SpriteSheets/grass");
            itemColors[ItemType.Grass] = Color.Green;
            itemTextures[ItemType.Hedge] = contentManager.Load<Texture2D>("SpriteSheets/hedge");
            itemColors[ItemType.Hedge] = Color.Green;
            itemTextures[ItemType.Lava] = contentManager.Load<Texture2D>("SpriteSheets/lava");
            itemColors[ItemType.Lava] = Color.Orange;
        }

        public void loadWords(ContentManager contentManager)
        {
            wordTextures[WordType.Is] = contentManager.Load<Texture2D>("SpriteSheets/word-is");
            wordColors[WordType.Is] = Color.White;

            wordTextures[WordType.Baba] = contentManager.Load<Texture2D>("SpriteSheets/word-baba");
            wordColors[WordType.Baba] = Color.White;

            wordTextures[WordType.Flag] = contentManager.Load<Texture2D>("SpriteSheets/word-flag");
            wordColors[WordType.Flag] = Color.Yellow;

            wordTextures[WordType.Lava] = contentManager.Load<Texture2D>("SpriteSheets/word-lava");
            wordColors[WordType.Lava] = Color.Orange;

            wordTextures[WordType.Lava] = contentManager.Load<Texture2D>("SpriteSheets/word-lava");
            wordColors[WordType.Lava] = Color.Orange;

            wordTextures[WordType.Push] = contentManager.Load<Texture2D>("SpriteSheets/word-push");
            wordColors[WordType.Push] = Color.Yellow;

            wordTextures[WordType.Rock] = contentManager.Load<Texture2D>("SpriteSheets/word-rock");
            wordColors[WordType.Rock] = Color.YellowGreen;

            wordTextures[WordType.Sink] = contentManager.Load<Texture2D>("SpriteSheets/word-sink");
            wordColors[WordType.Sink] = Color.Blue;

            wordTextures[WordType.Stop] = contentManager.Load<Texture2D>("SpriteSheets/word-stop");
            wordColors[WordType.Stop] = Color.Green;

            wordTextures[WordType.Wall] = contentManager.Load<Texture2D>("SpriteSheets/word-wall");
            wordColors[WordType.Wall] = Color.Gray;

            wordTextures[WordType.Goop] = contentManager.Load<Texture2D>("SpriteSheets/word-water");
            wordColors[WordType.Goop] = Color.Blue;

            wordTextures[WordType.You] = contentManager.Load<Texture2D>("SpriteSheets/word-you");
            wordColors[WordType.You] = Color.Pink;
        }
    }
}
