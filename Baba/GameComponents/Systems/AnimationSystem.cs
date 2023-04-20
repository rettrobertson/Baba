using Baba.Animation;
using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Baba.GameComponents.Systems
{
    public class AnimationSystem : System
    {
        List<SpriteAnimator> animators;

        private Dictionary<ItemType, Animation.Animation> itemAnimations = new Dictionary<ItemType, Animation.Animation>
        {
            { ItemType.Flag, Animations.FLAG },
            { ItemType.Grass, Animations.GRASS },
            { ItemType.Hedge, Animations.HEDGE },
            { ItemType.Lava, Animations.LAVA },
            { ItemType.Rock, Animations.ROCK },
            { ItemType.Wall, Animations.WALL },
            { ItemType.Water, Animations.WATER }
        };

        private Dictionary<WordType, Animation.Animation> wordAnimations = new Dictionary<WordType, Animation.Animation>
        {
        };

        public AnimationSystem(NewGameView view) : base(view, typeof(Sprite))
        {
            animators = new List<SpriteAnimator>();
            //Subscribe to rule system transformation
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {
            if (change == Entity.ComponentChange.ADD)
            {
                ItemLabel itemLabel = entity.GetComponent<ItemLabel>();
                WordLabel wordLabel = entity.GetComponent<WordLabel>();
                SpriteAnimator sprite = null;

                if (itemLabel != null && itemAnimations.ContainsKey(itemLabel.item))
                {
                    sprite = new SpriteAnimator(entity.GetComponent<Sprite>());
                    sprite.animation = itemAnimations[itemLabel.item];
                }
                else if (wordLabel != null && wordAnimations.ContainsKey(wordLabel.item))
                {
                    sprite = new SpriteAnimator(entity.GetComponent<Sprite>());
                    sprite.animation = wordAnimations[wordLabel.item];
                }

                if (sprite != null)
                {
                    entity.AddComponent(sprite);
                    animators.Add(sprite);
                }
                
            }
        }

        public override void Update(GameTime time)
        {
            foreach (SpriteAnimator animator in animators) 
            {
                animator.time += time.ElapsedGameTime;
                Sprite sprite = animator.sprite;
                sprite.texture = animator.animation.texture;

                if (animator.time > animator.animation.Duration)
                {
                    animator.time -= animator.animation.Duration;
                }

                sprite.source = animator.animation.Evaluate(animator.time);
            }
        }
    }
}
