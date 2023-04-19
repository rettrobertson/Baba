using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Baba.GameComponents.Systems
{
    public class SpriteRenderer : System
    {
        private Dictionary<ItemType, Texture2D> itemTextures = new Dictionary<ItemType, Texture2D>();
        private Dictionary<ItemType, Color> itemColors = new Dictionary<ItemType, Color>();
        private Dictionary<WordType, Texture2D> wordTextures = new Dictionary<WordType, Texture2D>();
        private Dictionary<WordType, Color> wordColors = new Dictionary<WordType, Color>();

        private static readonly Rectangle defaultSource = new Rectangle(0, 0, 24, 24);

        private const int renderScale = 80; // Number of pixels in a unit. This could be changed to number of units on the screen to support more resolutions

        private SpriteBatch m_spriteBatch;

        private List<Sprite> renderEntities;

        public SpriteRenderer(GameStateView view, GraphicsDevice graphics) : base(view, typeof(ItemLabel), typeof(WordLabel))
        {
            m_spriteBatch = new SpriteBatch(graphics);
            renderEntities = new List<Sprite>();
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {
            if (change == Entity.ComponentChange.ADD)
            {
                Sprite sprite = new Sprite();
                entity.AddComponent(sprite);
                renderEntities.Add(sprite);

                WordLabel word = component as WordLabel;
                ItemLabel item = component as ItemLabel;

                if (word != null)
                {
                    sprite.color = wordColors[word.item];

                    Texture2D texture = wordTextures[word.item];
                    sprite.texture = texture;
                    sprite.source = defaultSource;
                }
                else if (item != null && itemColors.ContainsKey(item.item))
                {
                    sprite.color = itemColors[item.item];
                    Texture2D texture = itemTextures[item.item];
                    sprite.texture = texture;
                    sprite.source = defaultSource;
                }
            }
            else
            {
                renderEntities.Remove(entity.GetComponent<Sprite>());
            }
        }
        
        public void UpdateItemType(ItemLabel itemLabel)
        {
            itemLabel.entity.GetComponent<Sprite>().color = itemColors[itemLabel.item];
        }

        public void Render()
        {
            m_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            for (int i = 0; i < renderEntities.Count; i++)
            {
                Render(renderEntities[i]);
            }
            m_spriteBatch.End();
        }

        private void Render(Sprite sprite)
        {
            Vector2 screenPos = GameToScreenPos(sprite.entity.transform.position);
            Texture2D texture = sprite.texture;

            if (texture == null)
            {
                Debug.WriteLine("Texture not found!");
                return;
            }

            int width = renderScale;
            int height = renderScale;

            m_spriteBatch.Draw(texture, new Rectangle((int)screenPos.X, (int)screenPos.Y, width, height), sprite.source, sprite.color);
        }

        private Vector2 GameToScreenPos(Vector2 pos)
        {
            Vector2 scale = new Vector2(1, -1);
            Vector2 offset = Vector2.Zero; //For grid with origin at the center: new Vector2(m_graphics.Viewport.Bounds.Width / 2, m_graphics.Viewport.Bounds.Height / 2);

            return pos * renderScale * scale + offset;
        }
        public void LoadItems(ContentManager contentManager) 
        {
            itemTextures[ItemType.Baba] = contentManager.Load<Texture2D>("SpriteSheets/flag");
            itemColors[ItemType.Baba] = Color.White;
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

        public void LoadWords(ContentManager contentManager)
        {
            wordTextures[WordType.Is] = contentManager.Load<Texture2D>("SpriteSheets/word-is");
            wordColors[WordType.Is] = Color.White;

            wordTextures[WordType.Baba] = contentManager.Load<Texture2D>("SpriteSheets/word-baba");
            wordColors[WordType.Baba] = Color.White;

            wordTextures[WordType.Flag] = contentManager.Load<Texture2D>("SpriteSheets/word-flag");
            wordColors[WordType.Flag] = Color.Yellow;

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

        public override void Update(GameTime time)
        {
        }
    }
}
