using System;

namespace Baba.Animation
{
    public static class Animations
    {
        public static Animation BABA = new ThreeFrameAnimation("baba");
        public static Animation FLAG = new ThreeFrameAnimation("flag");
        public static Animation FLOOR = new ThreeFrameAnimation("floor");
        public static Animation FLOWERS = new ThreeFrameAnimation("flowers");
        public static Animation GRASS = new ThreeFrameAnimation("grass");
        public static Animation HEDGE = new ThreeFrameAnimation("hedge");
        public static Animation LAVA = new ThreeFrameAnimation("lava");
        public static Animation ROCK = new ThreeFrameAnimation("rock");
        public static Animation WALL = new ThreeFrameAnimation("wall");
        public static Animation WATER = new ThreeFrameAnimation("water");
        public static Animation EMPTY = new ConstantAnimation(null, 1, TimeSpan.Zero, 24, 24);

        public static Animation WORD_BABA = new ThreeFrameAnimation("word-baba");
        public static Animation WORD_FLAG = new ThreeFrameAnimation("word-flag");
        public static Animation WORD_IS = new ThreeFrameAnimation("word-is");
        public static Animation WORD_KILL = new ThreeFrameAnimation("word-kill");
        public static Animation WORD_LAVA = new ThreeFrameAnimation("word-lava");
        public static Animation WORD_PUSH = new ThreeFrameAnimation("word-push");
        public static Animation WORD_ROCK = new ThreeFrameAnimation("word-rock");
        public static Animation WORD_SINK = new ThreeFrameAnimation("word-sink");
        public static Animation WORD_STOP = new ThreeFrameAnimation("word-stop");
        public static Animation WORD_WALL = new ThreeFrameAnimation("word-wall");
        public static Animation WORD_WATER = new ThreeFrameAnimation("word-water");
        public static Animation WORD_WIN = new ThreeFrameAnimation("word-win");
        public static Animation WORD_YOU = new ThreeFrameAnimation("word-you");
    }
}
