﻿using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Baba.GameComponents.Systems
{
    public class SpriteRenderer : System
    {
        private Dictionary<ItemType, Texture2D> itemTextures = new Dictionary<ItemType, Texture2D>();
        private Dictionary<ItemType, Color> itemColors = new Dictionary<ItemType, Color>();
        private Dictionary<WordType, Texture2D> wordTextures = new Dictionary<WordType, Texture2D>();
        private Dictionary<WordType, Color> wordColors = new Dictionary<WordType, Color>();

        private static readonly Rectangle defaultSource = new Rectangle(0, 0, 24, 24);

        private SpriteBatch m_spriteBatch;

        private List<Sprite> renderEntities;
        private HashSet<uint> frontEntities = new HashSet<uint>();
        private HashSet<uint> backEntities = new HashSet<uint>();
        
        private GameStateView view;
        CameraSystem cameraSystem;

        public SpriteRenderer(NewGameView view, GraphicsDevice graphics) : base(view, typeof(ItemLabel), typeof(WordLabel), typeof(Push), typeof(You))
        {
            cameraSystem = CameraSystem.Instance;
            m_spriteBatch = new SpriteBatch(graphics);
            renderEntities = new List<Sprite>();
            this.view = view;
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {

            if (component.GetType() == typeof(Push) || component.GetType() == typeof(You)) // This won't work if items can have more than one attribute in the rules
            {
                if (change == Entity.ComponentChange.ADD)
                {
                    frontEntities.Add(entity.id);
                }
                else
                {
                    frontEntities.Remove(entity.id);
                }
                return;
            }

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
                
                if (item != null && item.item == ItemType.Background)
                {
                    backEntities.Add(entity.id);
                }
            }
            else
            {
                renderEntities.Remove(entity.GetComponent<Sprite>());
            }
        }
        
        public void UpdateSprites()
        {
            for (int i = 0; i < renderEntities.Count; i++)
            {
                Sprite sprite = renderEntities[i];

                WordLabel word = sprite.entity.GetComponent<WordLabel>();
                ItemLabel item = sprite.entity.GetComponent<ItemLabel>();

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
        }

        public void Render()
        {
            m_spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront);
            for (int i = 0; i < renderEntities.Count; i++)
            {
                Render(renderEntities[i]);
            }
            m_spriteBatch.End();
        }

        private void Render(Sprite sprite)
        {
            Vector2 screenPos = cameraSystem.GameToScreenPos(sprite.entity.transform.position);
            Texture2D texture = sprite.texture;

            if (texture == null)
            {
/*                Debug.WriteLine("Texture not found!");
*/                return;
            }

            int width = cameraSystem.RenderScale;
            int height = cameraSystem.RenderScale;

            float layer = .5f;
            if (backEntities.Contains(sprite.entity.id))
            {
                layer = 1;
            }
            else if (frontEntities.Contains(sprite.entity.id))
            {
                layer = 0;
            }

            SpriteEffects effect = sprite.flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            m_spriteBatch.Draw(texture, new Rectangle((int)screenPos.X, (int)screenPos.Y, width, height), sprite.source, sprite.color, 0, Vector2.Zero, effect, layer);
        }


        public void LoadItems(ContentManager contentManager) 
        {
            itemTextures[ItemType.Baba] = contentManager.Load<Texture2D>("SpriteSheets/baba");
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
            itemTextures[ItemType.Water] = contentManager.Load<Texture2D>("SpriteSheets/lava");
            itemColors[ItemType.Water] = Color.Blue;
            itemTextures[ItemType.Background] = contentManager.Load<Texture2D>("SpriteSheets/wall");
            itemColors[ItemType.Background] = Color.DarkSlateGray;
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

            wordTextures[WordType.Water] = contentManager.Load<Texture2D>("SpriteSheets/word-water");
            wordColors[WordType.Water] = Color.Blue;

            wordTextures[WordType.You] = contentManager.Load<Texture2D>("SpriteSheets/word-you");
            wordColors[WordType.You] = Color.Pink;

            wordTextures[WordType.Win] = contentManager.Load<Texture2D>("SpriteSheets/word-win");
            wordColors[WordType.Win] = Color.Yellow;

            wordTextures[WordType.Kill] = contentManager.Load<Texture2D>("SpriteSheets/word-kill");
            wordColors[WordType.Kill] = Color.Red;

        }

        public override void Update(GameTime time)
        {
        }

        public override void Reset()
        {
            renderEntities.Clear();
            frontEntities.Clear();
            backEntities.Clear();
        }
    }
}
