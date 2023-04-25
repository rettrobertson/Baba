using Baba.GameComponents.ConcreteComponents;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Baba.GameComponents
{
    public class GridMaker
    {
        private Dictionary<char, (ItemType?, WordType?)> dict;
        private readonly string levelsFile = Path.Combine("Content", "levels-all.bbiy");
        public GridMaker() 
        {
            dict = new Dictionary<char, (ItemType?, WordType?)>
            {
                { 'h', (ItemType.Hedge, null) },
                { 'g', (ItemType.Grass, null) },
                { 'l', (ItemType.Background, null) },
                { 'w', (ItemType.Wall, null) },
                { 'b', (ItemType.Baba, null) },
                { 'r', (ItemType.Rock, null) },
                { 'f', (ItemType.Flag, null) },
                { 'a', (ItemType.Water, null) },
                { 'v', (ItemType.Lava, null) },
                { 'c', (ItemType.Anni, null) },
                { 'e', (ItemType.Love, null) },
                { 'd', (ItemType.Bone, null) },
                { 'j', (ItemType.Ice, null) },
                { 'o', (ItemType.Kiki, null) },

                { 'Z', (null, WordType.Empty) },

                { 'W', (null, WordType.Wall) },
                { 'B', (null, WordType.Baba) },
                { 'R', (null, WordType.Rock) },
                { 'F', (null, WordType.Flag) },
                { 'A', (null, WordType.Water) },
                { 'V', (null, WordType.Lava) },
                { 'C', (null, WordType.Anni) },
                { 'E', (null, WordType.Love) },
                { 'D', (null, WordType.Bone) },
                { 'J', (null, WordType.Ice) },
                { 'O', (null, WordType.Kiki) },
                { 'I', (null, WordType.Is) },
                { 'S', (null, WordType.Stop) },
                { 'P', (null, WordType.Push) },
                { 'Y', (null, WordType.You) },
                { 'X', (null, WordType.Win) },
                { 'N', (null, WordType.Sink) },
                { 'K', (null, WordType.Kill) },
                { 'H', (null, WordType.Hot) },
                { 'M', (null, WordType.Melt) },
                { 'Q', (null, WordType.Move) },
                { 'L', (null, WordType.Slip) },
                { 'T', (null, WordType.Best) }
            };
        }


        public List<Transform> MakeGrid(string level)
        {
            List<Transform> returnList = new();


            (int width, int height, List<string> file) = getLength(levelsFile,  level);
            for (int i = 0; i < height; i++)
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
                        entity.transform.position = new Vector2(j, i);
                        returnList.Add(entity.transform);
                    }
                }
            }
            for (int i = height; i < 2 * height; i++)
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
        public List<string> getLevels()
        {
            List <string> levels = new List<string>();
            IEnumerable<string> lines = File.ReadLines(levelsFile);
            foreach(string line in lines)
            {
                char[] chars = line.ToCharArray();
                if (chars[0].ToString() != " "&& chars[0].ToString() != "h" && chars[0] != null && chars[0].ToString() != "2")
                {
                    levels.Add(line.Trim());
                }
            }

            return levels;
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
