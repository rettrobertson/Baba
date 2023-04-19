using Baba.GameComponents.ConcreteComponents;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json.Serialization;

namespace Baba.GameComponents
{
    public class GridMaker
    {
        private Dictionary<char, (ItemType?, WordType?)> dict;
        public GridMaker() 
        {
            dict = new Dictionary<char, (ItemType?, WordType?)>();
/*            dict.Add(' ', EntityMaker.MakeEntity(ItemType.Empty));
*/            dict.Add('h', (ItemType.Hedge, null));
            dict.Add('g', (ItemType.Grass, null));
            dict.Add('l', (ItemType.Background, null));
            dict.Add('w', (ItemType.Wall, null));
            dict.Add('b', (ItemType.Baba, null));
            dict.Add('r', (ItemType.Rock, null));
            dict.Add('f', (ItemType.Flag, null));
            dict.Add('a', (ItemType.Goop, null));
            dict.Add('v', (ItemType.Lava, null));
            dict.Add('c', (ItemType.Anni, null));
            dict.Add('e', (ItemType.Love, null));
            dict.Add('d', (ItemType.Bone, null));
            dict.Add('j', (ItemType.Ice, null));
            dict.Add('o', (ItemType.Kiki, null));
            
            dict.Add('Z', (null, WordType.Empty));
           
            dict.Add('W', (null, WordType.Wall));
            dict.Add('B', (null, WordType.Baba));
            dict.Add('R', (null, WordType.Rock));
            dict.Add('F', (null, WordType.Flag));
            dict.Add('A', (null, WordType.Goop));
            dict.Add('V', (null, WordType.Lava));
            dict.Add('C', (null, WordType.Anni));
            dict.Add('E', (null, WordType.Love));
            dict.Add('D', (null, WordType.Bone));
            dict.Add('J', (null, WordType.Ice));
            dict.Add('O', (null, WordType.Kiki));
            dict.Add('I', (null, WordType.Is));
            dict.Add('S', (null, WordType.Stop));
            dict.Add('P', (null, WordType.Push));
            dict.Add('Y', (null, WordType.You));
            dict.Add('X', (null, WordType.Win));
            dict.Add('N', (null, WordType.Sink));
            dict.Add('K', (null, WordType.Kill));
            dict.Add('H', (null, WordType.Hot));
            dict.Add('M', (null, WordType.Melt));
            dict.Add('Q', (null, WordType.Move));
            dict.Add('L', (null, WordType.Slip));
            dict.Add('T', (null, WordType.Best));
        }
        public List<Transform> MakeGrid(string level)
        {
            List<Transform> returnList = new();
            (int width, int height, List<string> file) = getLength(Directory.GetCurrentDirectory() + "..\\..\\..\\..\\levels-all.bbiy", level);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (file[i][j] != ' ')
                    {
                        (ItemType?, WordType?) temp = dict[file[i][j]];
                        Entity entity = null;
                        if (temp.Item1!= null)
                        {
                            entity = EntityMaker.MakeEntity((ItemType)temp.Item1);
                        }
                        else if (temp.Item2 != null)
                        {
                            entity = EntityMaker.MakeEntity((WordType)temp.Item2);
                        }
                        entity.transform.entity = entity;
                        entity.transform.position = new Vector2(j, i);
                        returnList.Add(entity.transform);
                    }
                }
            }
            for (int i = height; i < 2*height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (file[i][j] != ' ')
                    {
                        (ItemType?, WordType?) temp = dict[file[i][j]];
                        Entity entity = null;
                        if (temp.Item1 != null)
                        {
                            entity = EntityMaker.MakeEntity((ItemType)temp.Item1);
                        }
                        else if (temp.Item2 != null)
                        {
                            entity = EntityMaker.MakeEntity((WordType)temp.Item2);
                        }
                        entity.transform.entity = entity;
                        entity.transform.position = new Vector2(j, i - height);
                        returnList.Add(entity.transform);
                    }
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

        // for testing purposes
        public void TestList(List<Transform> transforms)
        {
            char[][] temp = new char[20][];
            for (int i = 0; i < 20; i++)
            {
                temp[i] = new char[20] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };

            }
            foreach (Transform t in transforms)
            {
                temp[(int)t.position.Y][(int)t.position.X] = 'X';
            }
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Debug.Write(temp[i][j]);
                }
                Debug.Write("\n");
            }
            Debug.WriteLine(transforms.Count);
        }
    }
}
