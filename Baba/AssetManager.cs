﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Baba
{
    public class AssetManager
    {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();


        public delegate void ContentLoadedCallback();
        public static event ContentLoadedCallback onContentLoaded;
        private static ContentManager contentManager;

        public static void LoadContent(ContentManager content)
        {
            contentManager = content;
            LoadTexture("baba");
            LoadTexture("Square");
            LoadTexture("Circle");

            LoadTexture("flag");
            LoadTexture("floor");
            LoadTexture("flowers");
            LoadTexture("grass");
            LoadTexture("hedge");
            LoadTexture("lava");
            LoadTexture("rock");
            LoadTexture("wall");
            LoadTexture("water");

            LoadTexture("word-baba");
            LoadTexture("word-flag");
            LoadTexture("word-is");
            LoadTexture("word-kill");
            LoadTexture("word-lava");
            LoadTexture("word-push");
            LoadTexture("word-rock");
            LoadTexture("word-sink");
            LoadTexture("word-stop");
            LoadTexture("word-wall");
            LoadTexture("word-water");
            LoadTexture("word-win");
            LoadTexture("word-you");

            LoadSound("menu");
            LoadSound("level-one");
            LoadSound("level-two");
            LoadSound("level-three");
            LoadSound("level-four");
            LoadSound("level-five");

            LoadSound("win");
            LoadSound("move");
            LoadSound("hurt");
            LoadSound("change");


            onContentLoaded?.Invoke();
        }

        private static void LoadTexture(string texture)
        {
            textures.Add(texture, contentManager.Load<Texture2D>("SpriteSheets/" + texture));
        }
        private static void LoadFont(string font)
        {
            fonts.Add(font, contentManager.Load<SpriteFont>("Fonts/" + font));
        }
        private static void LoadSound(string sound)
        {
            sounds.Add(sound, contentManager.Load<SoundEffect>("Audio/Sounds/" + sound));
        }

        public static Texture2D GetTexture(string texture)
        {
            if (textures.ContainsKey(texture))
            {
                return textures[texture];
            }
            else
            {
                return null;
            }
        }

        public static SpriteFont GetFont(string font)
        {
            return fonts[font];
        }
        public static SoundEffect GetSound(string sound)
        {
            return sounds[sound];
        }
    }
}
