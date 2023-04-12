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
            dict.Add(' ', EntityMaker.MakeEntity(ItemType.Empty));
            dict.Add('h', EntityMaker.MakeEntity(ItemType.Hedge));
            dict.Add('g', EntityMaker.MakeEntity(ItemType.Grass));
            dict.Add('l', EntityMaker.MakeEntity(ItemType.Background));
            dict.Add('w', EntityMaker.MakeEntity(ItemType.Wall));
            dict.Add('b', EntityMaker.MakeEntity(ItemType.Baba));
            dict.Add('r', EntityMaker.MakeEntity(ItemType.Rock));
            dict.Add('f', EntityMaker.MakeEntity(ItemType.Flag));
            dict.Add('a', EntityMaker.MakeEntity(ItemType.Goop));
            dict.Add('v', EntityMaker.MakeEntity(ItemType.Lava));
            dict.Add('c', EntityMaker.MakeEntity(ItemType.Anni));
            dict.Add('e', EntityMaker.MakeEntity(ItemType.Love));
            dict.Add('d', EntityMaker.MakeEntity(ItemType.Bone));
            dict.Add('j', EntityMaker.MakeEntity(ItemType.Ice));
            dict.Add('o', EntityMaker.MakeEntity(ItemType.Kiki));
            
            dict.Add('Z', EntityMaker.MakeEntity(WordType.Empty));
           
            dict.Add('W', EntityMaker.MakeEntity(WordType.Wall));
            dict.Add('B', EntityMaker.MakeEntity(WordType.Baba));
            dict.Add('R', EntityMaker.MakeEntity(WordType.Rock));
            dict.Add('F', EntityMaker.MakeEntity(WordType.Flag));
            dict.Add('A', EntityMaker.MakeEntity(WordType.Goop));
            dict.Add('V', EntityMaker.MakeEntity(WordType.Lava));
            dict.Add('C', EntityMaker.MakeEntity(WordType.Anni));
            dict.Add('E', EntityMaker.MakeEntity(WordType.Love));
            dict.Add('D', EntityMaker.MakeEntity(WordType.Bone));
            dict.Add('J', EntityMaker.MakeEntity(WordType.Ice));
            dict.Add('O', EntityMaker.MakeEntity(WordType.Kiki));
            dict.Add('I', EntityMaker.MakeEntity(WordType.Is));
            dict.Add('S', EntityMaker.MakeEntity(WordType.Stop));
            dict.Add('P', EntityMaker.MakeEntity(WordType.Push));
            dict.Add('Y', EntityMaker.MakeEntity(WordType.You));
            dict.Add('X', EntityMaker.MakeEntity(WordType.Win));
            dict.Add('N', EntityMaker.MakeEntity(WordType.Sink));
            dict.Add('K', EntityMaker.MakeEntity(WordType.Kill));
            dict.Add('H', EntityMaker.MakeEntity(WordType.Hot));
            dict.Add('M', EntityMaker.MakeEntity(WordType.Melt));
            dict.Add('Q', EntityMaker.MakeEntity(WordType.Move));
            dict.Add('L', EntityMaker.MakeEntity(WordType.Slip));
            dict.Add('T', EntityMaker.MakeEntity(WordType.Best));
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
