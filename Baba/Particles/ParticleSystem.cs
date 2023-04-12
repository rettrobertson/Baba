using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;

namespace Baba.Particles
{
    public class ParticleSystem
    {
        public enum EmissionLayer { FOREGROUND, BACKGROUND }

        Dictionary<EmissionLayer, List<ParticleEmitter>> emitters = new Dictionary<EmissionLayer, List<ParticleEmitter>>()
        {
            { EmissionLayer.FOREGROUND, new List<ParticleEmitter>() },
            { EmissionLayer.BACKGROUND, new List<ParticleEmitter>() }
        };

        GraphicsDevice device;
        public int particleCount => CountParticles();

        public ParticleSystem()
        {
        }

        public void Update(GameTime time)
        {
            for (int i = 0; i < emitters[EmissionLayer.BACKGROUND].Count; i++)
            {
                emitters[EmissionLayer.BACKGROUND][i].Update(time);
            }

            for (int i = 0; i < emitters[EmissionLayer.FOREGROUND].Count; i++)
            {
                emitters[EmissionLayer.FOREGROUND][i].Update(time);
            }
        }

        private void AddEmitter(ParticleEmitter emitter, EmissionLayer layer)
        {
            emitters[layer].Add(emitter);
        }

        public void Render()
        {
            for (int i = 0; i < emitters[EmissionLayer.FOREGROUND].Count; i++)
            {
                Render(emitters[EmissionLayer.FOREGROUND][i]);
            }
        }

        private void Render(ParticleEmitter emitter)
        {

        }

        private int CountParticles()
        {
            int total = 0;

            foreach (ParticleEmitter emitter in emitters[EmissionLayer.FOREGROUND])
            {
                total += emitter.Count;
            }
            foreach (ParticleEmitter emitter in emitters[EmissionLayer.BACKGROUND])
            {
                total += emitter.Count;
            }

            return total;
        }

        public void ClearParticles()
        {
            foreach (ParticleEmitter emitter in emitters[EmissionLayer.FOREGROUND])
            {
                emitter.Clear();
            }
            foreach (ParticleEmitter emitter in emitters[EmissionLayer.BACKGROUND])
            {
                emitter.Clear();
            }
        }
    }
}