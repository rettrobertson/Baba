using Baba.Particles.Decorators;
using Baba.Particles.EmissionTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Baba.Particles
{
    public class ParticleEmitter
    {
        List<ParticleDecorator> particleDecorators;
        List<EmissionDecorator> emissionDecorators;
        internal List<Particle> particles;
        List<Particle> deadParticles;
        TimeSpan emissionDuration;
        TimeSpan curTime;
        bool destroyOnComplete;
        bool active;
        public bool loop { get; set; }
        internal Texture2D particleTexture;

        private EmissionShape emissionShape;
        
        float minLifetime;
        float maxLifetime;

        public Vector2 emitLocation { get; set; }
        //public float emissionTime { get; set; }
        public float emissionRate {get; set; } = 1;// particles / second
        float emissionTimer;
        public BlendState blendState { get; set; }
        SpriteBatch spriteBatch;
        Random random;
        List<Burst> bursts;

        public int Count => particles.Count;

        public ParticleEmitter()
        {
            particleDecorators = new List<ParticleDecorator>();
            emissionDecorators = new List<EmissionDecorator>();

            particles = new List<Particle>();
            deadParticles = new List<Particle>();
            bursts = new List<Burst>();
            emissionDuration = TimeSpan.FromSeconds(5);
            random = new Random();
        }

        public bool Update(GameTime time)
        {
            if (active)
            {
                //Emit particles
                if (emissionRate > 0)
                {
                    emissionTimer += (float)time.ElapsedGameTime.TotalSeconds;

                    int particles = (int)(emissionTimer * emissionRate);

                    for (int i = 0; i < particles; i++)
                    {
                        EmitParticle();
                        emissionTimer -= 1/emissionRate;
                    }
                }

                //Emit bursts
                for (int i = 0; i < bursts.Count; i++)
                {
                    Burst burst = bursts[i];
                    if (!burst.fired && curTime >= burst.time)
                    {
                        for (int j = 0; j < burst.particles; j++)
                        {
                            EmitParticle();
                            burst.fired = true;
                        }
                    }
                }

            }
            //Update particles
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                //Add lifetime
                particles[i].lifetime += (float)time.ElapsedGameTime.TotalSeconds;

                for (int j = 0; j < particleDecorators.Count; j++)
                {
                    particleDecorators[j].UpdateParticle(time, particles[i]);
                }

                //Remove dead particles
                bool isDead = particles[i].Update(time);
                if (isDead)
                {
                    particles.RemoveAt(i);
                }
            }

            //Check if emission is complete
            curTime += time.ElapsedGameTime;

            if (curTime >= emissionDuration)
            {
                if (loop)
                {
                    curTime -= emissionDuration;
                    emissionTimer = 0;
                    for (int i = 0; i < bursts.Count; i++)
                    {
                        bursts[i].fired = false;
                    }
                }
                else
                {
                    Stop();
                }
            }
            return (curTime > emissionDuration && particles.Count == 0);
        }

        private void EmitParticle()
        {
            float lifetime = MathHelper.Lerp(minLifetime, maxLifetime, (float)random.NextDouble());

            Particle particle = new Particle(lifetime);
            particle.position = emissionShape.GetEmissionPoint() + emitLocation;

            foreach (EmissionDecorator decorator in emissionDecorators)
            {
                decorator.Apply(particle);
            }

            particles.Add(particle);
        }

        public void SetLifetime(float lifeTime)
        {
            minLifetime = lifeTime;
            maxLifetime = lifeTime;
        }

        public void SetLifetime(float minLifetime, float maxLifetime)
        {
            this.minLifetime = minLifetime;
            this.maxLifetime = maxLifetime;
        }

        public void SetParticleLifetime(float minTime, float maxTime)
        {
            minLifetime = minTime;
            maxLifetime = maxTime;
        }
        public void SetEmissionTime(float time)
        {
            emissionDuration = TimeSpan.FromSeconds(time);
        }
        
        public void SetEmissionShape(EmissionShape shape)
        {
            emissionShape = shape;
        }

        public void Start()
        {
            active = true;
            curTime = TimeSpan.Zero;
            emissionTimer = 0;
            for (int i = 0; i < bursts.Count; i++)
            {
                bursts[i].fired = false;
            }
        }

        public void Stop(bool clearParticles = false)
        {
            active = false;

            if (clearParticles)
            {
                particles.Clear();
            }

        }

        public void Pause()
        {
            active = false;
        }

        public void Resume()
        {
            active = true;
        }

        private void Reset()
        {

        }

        public void ScheduleBurst(float seconds, int particles)
        {
            bursts.Add(new Burst(particles, TimeSpan.FromSeconds(seconds)));
        }

        public void AddDecorator(ParticleDecorator decorator)
        {
            particleDecorators.Add(decorator);
        }
        public void AddDecorator(EmissionDecorator decorator)
        {
            emissionDecorators.Add(decorator);
        }

        public void SetTexture(string textureName)
        {
            Texture2D texture = AssetManager.GetTexture(textureName);

            if (texture == null)
            {
                AssetManager.onContentLoaded += () => particleTexture = AssetManager.GetTexture(textureName);
            }
            else
            {
                particleTexture = texture;
            }
        }

        public void Clear()
        {
            particles.Clear();
        }

        private class Burst
        {
            public int particles;
            public TimeSpan time;
            public bool fired;
            public Burst(int particles, TimeSpan time)
            {
                this.particles = particles;
                this.time = time;
            }
        }
    }
}
