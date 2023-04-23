﻿using Baba.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Baba.GameComponents.ConcreteComponents;
using Baba.Particles;
using Baba.Views;

namespace Baba.GameComponents.Systems
{
    public class ParticleSystem : System
    {

        List<ParticleEmitter> emitters;

        private SpriteBatch spriteBatch;
        private List<ParticleEmitter> removeEmitters;
        public int particleCount => CountParticles();

        public ParticleSystem(NewGameView view, GraphicsDevice graphics) : base(view, typeof(You), typeof(Win))
        {
            spriteBatch = new SpriteBatch(graphics);
            emitters = new List<ParticleEmitter>();
            removeEmitters = new List<ParticleEmitter>();
        }

        protected override void EntityChanged(Entity entity, Component component, Entity.ComponentChange change)
        {
            if (change == Entity.ComponentChange.ADD)
            {
                if (component.GetType() == typeof(You))
                {
                    YouChanged(entity.transform.position);
                }
                if (component.GetType() == typeof(Win))
                {
                    WinChanged(entity.transform.position);
                }
            }
            else
            {
                if (component.GetType() == typeof(You))
                {
                    YouChanged(entity.transform.position);
                }
                if (component.GetType() == typeof(Win))
                {
                    WinChanged(entity.transform.position);
                }
            }
        }

        public override void Update(GameTime time)
        {
            for (int i = 0; i < emitters.Count; i++)
            {
                bool isDone = emitters[i].Update(time);
                if (isDone)
                {
                    removeEmitters.Add(emitters[i]);
                }
            }
            for (int i = 0; i < removeEmitters.Count; i++)
            {
                emitters.Remove(removeEmitters[i]);
            }

            // This could be replaced with object pooling
            removeEmitters.Clear();
        }

        private int CountParticles()
        {
            int total = 0;

            foreach (ParticleEmitter emitter in emitters)
            {
                total += emitter.Count;
            }

            return total;
        }

        public void ClearParticles()
        {
            foreach (ParticleEmitter emitter in emitters)
            {
                emitter.Clear();
            }
        }
        public void ObjectDestroyed(Vector2 position)
        {
            ParticleEmitter emitter = ParticlePresets.MakeObjectDestroyed();
            emitter.emitLocation = position;
            emitters.Add(emitter);
            emitter.Start();
        }

        public void WinChanged(Vector2 position)
        {
            ParticleEmitter emitter = ParticlePresets.MakeWinChange();
            emitter.emitLocation = position;
            emitters.Add(emitter);
            emitter.Start();
        }

        public void WinLevel()
        {
            ParticleEmitter emitter = ParticlePresets.MakeWin();
            emitters.Add(emitter);
            emitter.Start();
        }

        public void YouChanged(Vector2 position)
        {
            ParticleEmitter emitter = ParticlePresets.MakeYouChange();
            emitter.emitLocation = position;
            emitters.Add(emitter);
            emitter.Start();
        }

        public override void Draw()
        {
            for (int i = 0; i < emitters.Count; i++)
            {
                Render(emitters[i]);
            }
        }
        private void Render(ParticleEmitter emitter)
        {
            spriteBatch.Begin(blendState: emitter.blendState);
            CameraSystem camera = CameraSystem.Instance;

            Vector2 pivot = Pivot.CENTER;

            for (int i = 0; i < emitter.Count; i++)
            {
                Particle particle = emitter.particles[i];
                Vector2 screenPos = camera.GameToScreenPos(particle.position);

                int width = (int)(camera.RenderScale * particle.size);
                int height = (int)(camera.RenderScale * particle.size);

                Vector2 offset = new Vector2(-width * pivot.X, -height * pivot.Y);

                spriteBatch.Draw(emitter.particleTexture, new Rectangle((int)(screenPos.X + offset.X), (int)(screenPos.Y + offset.Y), width, height), particle.color);
            }
            spriteBatch.End();
        }

        public override void Reset()
        {
            ClearParticles();
        }
    }
}