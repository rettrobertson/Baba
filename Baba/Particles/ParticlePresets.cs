using Baba.Particles.Decorators;
using Baba.Particles.Decorators.Emission;
using Baba.Particles.EmissionTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Baba.Particles
{
    public static class ParticlePresets
    {
        public static ParticleEmitter MakeWin()
        {
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            emitter.SetTexture("Square");
            emitter.ScheduleBurst(0, 1000);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.FadeColor(Color.Red, 0.8f)));
            emitter.SetEmissionShape(new PointEmitter(EmissionShape.EmitType.AREA));
            emitter.AddDecorator(new InitialSize(0.1f, 0.2f));
            emitter.SetTexture("Square");
            emitter.SetLifetime(5);

            return emitter;
        }
        public static ParticleEmitter MakeWinChange()
        {
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            emitter.SetTexture("Square");
            emitter.ScheduleBurst(0, 1000);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.FadeInOut(Color.Yellow)));
            emitter.AddDecorator(new InitialRadialVelocity(0, 360, .1f, .2f));
            emitter.AddDecorator(new InitialSize(0.1f, 0.2f));
            emitter.blendState = BlendState.NonPremultiplied;
            emitter.SetLifetime(1);
            emitter.SetEmissionShape(new RectangleEmitter(new EmissionTypes.Rectangle(0, 0, 1, 1), EmissionShape.EmitType.EDGE));
            emitter.SetTexture("Square");

            return emitter;
        }
        public static ParticleEmitter MakeYouChange()
        {
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            emitter.SetTexture("Square");
            emitter.ScheduleBurst(0, 200);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.FadeInOut(Color.White)));
            emitter.AddDecorator(new InitialRadialVelocity(0, 360, .1f, .2f));
            emitter.AddDecorator(new InitialSize(0.1f, 0.2f));
            emitter.SetLifetime(1);
            emitter.blendState = BlendState.NonPremultiplied;
            emitter.SetEmissionShape(new RectangleEmitter(new EmissionTypes.Rectangle(0, 0, 1, 1), EmissionShape.EmitType.EDGE));
            emitter.SetTexture("Square");

            return emitter;
        }
        public static ParticleEmitter MakeObjectDestroyed()
        {
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            emitter.SetTexture("Square");
            emitter.ScheduleBurst(0, 200);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.FadeInOut(Color.Red)));
            emitter.AddDecorator(new InitialRadialVelocity(0, 360, .1f, .2f));
            emitter.AddDecorator(new InitialSize(0.1f, 0.2f));
            emitter.SetLifetime(1);
            emitter.blendState = BlendState.NonPremultiplied;
            emitter.SetEmissionShape(new RectangleEmitter(new EmissionTypes.Rectangle(0, 0, 1, 1), EmissionShape.EmitType.EDGE));
            emitter.SetTexture("Square");
            return emitter;
        }
    }
}
