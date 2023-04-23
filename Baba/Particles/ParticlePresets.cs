using Baba.Particles.Decorators;
using Baba.Particles.Decorators.Emission;
using Baba.Particles.EmissionTypes;
using Microsoft.Xna.Framework;

namespace Baba.Particles
{
    public static class ParticlePresets
    {
        public static ParticleEmitter MakeWin()
        {
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            //emitter.SetLifetime(4);
            emitter.ScheduleBurst(0, 1000);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.FadeColor(Color.Red, 0.8f)));
            emitter.SetEmissionShape(new PointEmitter(EmissionShape.EmitType.AREA));
            emitter.AddDecorator(new InitialSize(0.1f, 0.2f));
            emitter.SetTexture("Square");

            return emitter;
        }
        public static ParticleEmitter MakeWinChange()
        {
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            emitter.SetLifetime(4);
            emitter.ScheduleBurst(0, 1000);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.FadeInOut(Color.Yellow)));
            emitter.AddDecorator(new InitialRadialVelocity(0, 360, .1f, .2f));
            emitter.AddDecorator(new InitialSize(0.1f, 0.2f));
            emitter.SetEmissionShape(new RectangleEmitter(new EmissionTypes.Rectangle(0, 0, 1, 1), EmissionShape.EmitType.EDGE));
            emitter.SetTexture("Square");

            return emitter;
        }
        public static ParticleEmitter MakeYouChange()
        {
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            emitter.SetLifetime(4);
            emitter.ScheduleBurst(0, 200);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.FadeInOut(Color.White)));
            emitter.AddDecorator(new InitialRadialVelocity(0, 360, .1f, .2f));
            emitter.AddDecorator(new InitialSize(0.1f, 0.2f));
            emitter.SetEmissionShape(new RectangleEmitter(new EmissionTypes.Rectangle(0, 0, 1, 1), EmissionShape.EmitType.EDGE));
            emitter.SetTexture("Square");

            return emitter;
        }
        public static ParticleEmitter MakeObjectDestroyed()
        {
            return null;
        }
    }
}
