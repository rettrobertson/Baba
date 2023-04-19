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

        public AnimationSystem(NewGameView view) : base(view)
        {
            animators = new List<SpriteAnimator>();
            //Subscribe to rule system transformation
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
