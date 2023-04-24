﻿using System;
using Baba.GameComponents.Systems;
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
            Random random = new Random();
            Color randomColor = new Color(random.Next(256), random.Next(256), random.Next(256));
            Color startColor = new Color(randomColor.R / 3, randomColor.G / 3, randomColor.B / 3);
            ParticleEmitter emitter = new ParticleEmitter();
            emitter.emissionRate = 0f;
            emitter.SetTexture("Square");
            emitter.ScheduleBurst(0, 1200);
            emitter.AddDecorator(new ColorOverLifetime(Gradient.From2Color(startColor, randomColor, .5f, .99f)));
            emitter.SetEmissionShape(new PointEmitter(EmissionShape.EmitType.AREA));

            emitter.AddDecorator(new InitialRadialVelocity(0, 360, .07f, 1f));
            emitter.blendState = BlendState.NonPremultiplied;
            emitter.AddDecorator(new InitialSize(0.05f, 0.07f));
            emitter.SetTexture("Square");
            emitter.SetLifetime(1, 2);

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
            emitter.AddDecorator(new InitialSize(0.07f, 0.1f));
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
