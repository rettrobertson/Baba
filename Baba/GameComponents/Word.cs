using Baba.GameComponents;
using Microsoft.Xna.Framework;
using System;

namespace Baba.GameComponenets
{
    public class Word : IItem
    {
        public WordType wordType { get; set; }
        public RuleType ruleType { get; set; }
        public IItem[,] grid;
        private int x;
        private int y;

        public Word(WordType type)
        {
            this.wordType = type;
            switch (this.wordType)
            {
                case WordType.Is:
                    ruleType = RuleType.Is;
                    break;
                case WordType.Wall:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Rock:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Baba:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Flag:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Goop:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Lava:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Anni:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Love:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Bone:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Ice:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Kiki:
                    ruleType = RuleType.Item;
                    break;
                case WordType.Stop:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Push:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.You:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Win:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Sink:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Kill:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Hot:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Melt:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Move:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Slip:
                    ruleType = RuleType.Attribute;
                    break;
                case WordType.Best:
                    ruleType = RuleType.Attribute;
                    break;
            }
        }

        public void AddLocation(IItem[,] grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
        public void Draw(GameTime gameTime)
        {
        }

        public bool Down()
        {
            throw new NotImplementedException();
        }

        public bool Left()
        {
            throw new NotImplementedException();
        }

        public bool Right()
        {
            throw new NotImplementedException();
        }

        public bool Up()
        {
            throw new NotImplementedException();
        }
    }
}
