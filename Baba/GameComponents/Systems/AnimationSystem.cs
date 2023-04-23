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
            { ItemType.Baba, Animations.FLAG },
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
            { WordType.Baba, Animations.WORD_BABA },
            { WordType.Flag, Animations.WORD_FLAG },
            { WordType.Is, Animations.WORD_IS },
            { WordType.Kill, Animations.WORD_KILL },
            { WordType.Lava, Animations.WORD_LAVA },
            { WordType.Push, Animations.WORD_PUSH },
            { WordType.Rock, Animations.WORD_ROCK },
            { WordType.Sink, Animations.WORD_SINK },
            { WordType.Stop, Animations.WORD_STOP },
            { WordType.Wall, Animations.WORD_WALL },
            { WordType.Water, Animations.WORD_WATER },
            { WordType.Win, Animations.WORD_WIN },
            { WordType.You, Animations.WORD_YOU },

        };

        public AnimationSystem(NewGameView view) : base(view, typeof(Sprite))
        {
            animators = new List<SpriteAnimator>();
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

        public void UpdateAnimations()
        {
            for (int i = 0; i < animators.Count; i++)
            {
                ItemLabel itemLabel = animators[i].entity.GetComponent<ItemLabel>();
                WordLabel wordLabel = animators[i].entity.GetComponent<WordLabel>();

                if (itemLabel != null)
                {
                    animators[i].animation = itemAnimations[itemLabel.item];
                }
                else if (wordLabel != null)
                {
                    animators[i].animation = wordAnimations[wordLabel.item];
                }

            }
        }

        public override void Update(GameTime time)
        {
            foreach (SpriteAnimator animator in animators) 
            {
                animator.time += time.ElapsedGameTime;

                if (animator.time >= animator.animation.Duration)
                {
                    animator.time -= animator.animation.Duration;
                }

                Sprite sprite = animator.sprite;
                sprite.texture = animator.animation.texture;

                sprite.source = animator.animation.Evaluate(animator.time);
            }
        }

        public override void Reset()
        {
            animators.Clear();
        }
    }
}
