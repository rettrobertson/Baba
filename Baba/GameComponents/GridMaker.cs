using Baba.GameComponenets;
using Baba.GameComponents.ConcreteComponents;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Baba.GameComponents
{
    public class GridMaker
    {
        private Dictionary<char, Entity> dict;
        public GridMaker() 
        {
            dict = new Dictionary<char, Entity>();
            dict.Add(' ', new Item(ItemType.Empty));
            dict.Add('h', new Item(ItemType.Hedge));
            dict.Add('g', new Item(ItemType.Grass));
            dict.Add('l', new Item(ItemType.Background));
            dict.Add('w', new Item(ItemType.Wall));
            dict.Add('b', new Item(ItemType.Baba));
            dict.Add('r', new Item(ItemType.Rock));
            dict.Add('f', new Item(ItemType.Flag));
            dict.Add('a', new Item(ItemType.Goop));
            dict.Add('v', new Item(ItemType.Lava));
            dict.Add('c', new Item(ItemType.Anni));
            dict.Add('e', new Item(ItemType.Love));
            dict.Add('d', new Item(ItemType.Bone));
            dict.Add('j', new Item(ItemType.Ice));
            dict.Add('o', new Item(ItemType.Kiki));
            
            dict.Add('Z', new Word(WordType.Empty));
           
            dict.Add('W', new Word(WordType.Wall));
            dict.Add('B', new Word(WordType.Baba));
            dict.Add('R', new Word(WordType.Rock));
            dict.Add('F', new Word(WordType.Flag));
            dict.Add('A', new Word(WordType.Goop));
            dict.Add('V', new Word(WordType.Lava));
            dict.Add('C', new Word(WordType.Anni));
            dict.Add('E', new Word(WordType.Love));
            dict.Add('D', new Word(WordType.Bone));
            dict.Add('J', new Word(WordType.Ice));
            dict.Add('O', new Word(WordType.Kiki));
            dict.Add('I', new Word(WordType.Is));
            dict.Add('S', new Word(WordType.Stop));
            dict.Add('P', new Word(WordType.Push));
            dict.Add('Y', new Word(WordType.You));
            dict.Add('X', new Word(WordType.Win));
            dict.Add('N', new Word(WordType.Sink));
            dict.Add('K', new Word(WordType.Kill));
            dict.Add('H', new Word(WordType.Hot));
            dict.Add('M', new Word(WordType.Melt));
            dict.Add('Q', new Word(WordType.Move));
            dict.Add('L', new Word(WordType.Slip));
            dict.Add('T', new Word(WordType.Best));
        }
        public List<Transform> MakeGrid(string fileName, string level)
        {
            List<Transform> returnList = new();
            (int width, int height, List<string> file) = getLength(fileName, level);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Entity temp = dict[file[i][j]];
                    returnList.Add(new Transform(new Vector2(j, i), temp));
                }
            }
            return returnList;
        }
        private static (int, int, List<string>) getLength(string fileName, string level)
        {
            IEnumerable<string> lines = File.ReadLines(fileName);
            int width = 0;
            int height = 0;
            int i = 0;
            List<string> file = new();
            bool temp = false;
            foreach (string line in lines)
            {
                if (i > 0)
                {
                    file.Add(line);
                    i--;
                }
                if (temp)
                {
                    string[] lengths = line.Split(' ');
                    width = int.Parse(lengths[0]);
                    height = int.Parse(lengths[2]);
                    temp = false;
                    i = height * 2;
                }
                temp = (line == level);
            }
            return (width, height, file);
        }
    }
}
