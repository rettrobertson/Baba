using Baba.Views;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Security.Cryptography;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Baba.GameComponents.Systems
{
    public class AudioSystem : System
    {
        SoundEffect change;
        SoundEffect move;
        SoundEffect hurt;
        SoundEffectInstance win;
        List<SoundEffectInstance> fireworks;

        SoundEffectInstance level_one;
        SoundEffectInstance level_two;
        SoundEffectInstance level_three;
        SoundEffectInstance level_four;
        SoundEffectInstance level_five;
        SoundEffectInstance everything_else;

        SoundEffect escape;

        Random random;

        public AudioSystem(NewGameView view) : base(view)
        {
            everything_else = AssetManager.GetSound("menu").CreateInstance();
            level_one = AssetManager.GetSound("level-one").CreateInstance();
            level_two = AssetManager.GetSound("level-two").CreateInstance();
            level_three = AssetManager.GetSound("level-three").CreateInstance();
            level_four = AssetManager.GetSound("level-four").CreateInstance();
            level_five = AssetManager.GetSound("level-five").CreateInstance();
            win = AssetManager.GetSound("win").CreateInstance();

            level_one.Volume = .6f;
            level_two.Volume = .6f;
            level_three.Volume = .6f;
            level_four.Volume = .6f;
            level_five.Volume = .6f;
            everything_else.Volume = .6f;
            win.Volume = .1f;

            move = AssetManager.GetSound("move");
            hurt = AssetManager.GetSound("hurt");
            change = AssetManager.GetSound("change");

            fireworks = new();
            for (int i = 0; i < 10; i++)
            {
                fireworks.Add(AssetManager.GetSound("firework-1").CreateInstance());
                fireworks.Add(AssetManager.GetSound("firework-2").CreateInstance());
                fireworks.Add(AssetManager.GetSound("firework-3").CreateInstance());
                fireworks.Add(AssetManager.GetSound("firework-4").CreateInstance());
                fireworks.Add(AssetManager.GetSound("firework-5").CreateInstance());
                fireworks.Add(AssetManager.GetSound("firework-6").CreateInstance());
                fireworks.Add(AssetManager.GetSound("firework-7").CreateInstance());
            }
            escape = AssetManager.GetSound("escape");

            random = new Random();
        }

        public void PlayHurt()
        {
            hurt.Play();
        }
        public void PlayMove()
        {
            move.Play();
        }
        public void PlayVictory()
        {
            win.IsLooped = true;
            win.Play();
        }
        public void PlayChange()
        {
            change.Play();
        }
        public void PlayFirework()
        {
            fireworks[random.Next(0, fireworks.Count)].Play(/*0.3f, random.NextSingle() / 4, random.NextSingle() * 2 - 1*/);
        }
        public void StopFireworks()
        {
            foreach(SoundEffectInstance i in fireworks)
            {
                i.Stop();
            }
        }
        public void StopWin()
        {
            win.Stop();
        }

        public void PlayEscape()
        {
            escape.Play();
        }
        public void StartBGM(string level)
        {
            switch (level)
            {
                case "Level-1":
                    level_one.IsLooped = true;
                    level_one.Play();
                    break;
                case "Level-2":
                    level_two.IsLooped = true;
                    level_two.Play();
                    break;
                case "Level-3":
                    level_three.IsLooped = true;
                    level_three.Play();
                    break;
                case "Level-4":
                    level_four.IsLooped = true;
                    level_four.Play();
                    break;
                case "Level-5":
                    level_five.IsLooped = true;
                    level_five.Play();
                    break;
                default:
                    everything_else.IsLooped = true;
                    everything_else.Play();
                    break;
            }
        }
        public void StopBGM()
        {
            everything_else.Stop();
            level_one.Stop();
            level_two.Stop();
            level_three.Stop();
            level_four.Stop();
            level_five.Stop();

            StopFireworks();
            win.Stop();
        }

        public override void Update(GameTime time)
        {
            
        }

        public override void Reset()
        {
        }
    }
}
