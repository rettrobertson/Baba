using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Baba.GameComponents
{
    public static class GridMaker
    {
        public static Item[,] MakeGrid(string fileName, string level)
        {
            (int width, int height, List<string> file) = getLength(fileName, level);
            Item[,] grid= new Item[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    char temp = file[i][j];

                    switch (temp){
                        // empty
                        case ' ':
                            break;

                        // hedge
                        case 'h':
                            break;

                        // grass
                        case 'g':
                            break;

                        // background wall
                        case 'l':
                            break;

                        // wall
                        case 'w':
                            break;

                        // baba
                        case 'b':
                            break;

                        // rock
                        case 'r':
                            break;

                        // flag
                        case 'f':
                            break;

                        // goop
                        case 'a':
                            break;

                        // lava
                        case 'v':
                            break;

                        // Anni
                        case 'c':
                            break;

                        // love
                        case 'e':
                            break;

                        // bone
                        case 'd':
                            break;

                        // ice
                        case 'j':
                            break;

                        // kiki
                        case 'o':
                            break;

                        // words
                        // is
                        case 'I':
                            break;

                        // wall
                        case 'W':
                            break;

                        // stop
                        case 'S':
                            break;

                        // rock
                        case 'R':
                            break;

                        // push
                        case 'P':
                            break;

                        // baba
                        case 'B':
                            break;

                        // you
                        case 'Y':
                            break;

                        // flag
                        case 'F':
                            break;

                        // win
                        case 'X':
                            break;

                        // goop
                        case 'A':
                            break;

                        // sink
                        case 'N':
                            break;

                        // lava
                        case 'V':
                            break;

                        // kill
                        case 'K':
                            break;

                        // anni
                        case 'C':
                            break;

                        // hot
                        case 'H':
                            break;

                        // melt
                        case 'M':
                            break;

                        // move
                        case 'G':
                            break;

                        // slip
                        case 'L':
                            break;

                        // best
                        case 'T':
                            break;

                        // love
                        case 'E':
                            break;

                        // bone
                        case 'D':
                            break;

                        // ice
                        case 'J':
                            break;

                        // Kiki
                        case 'O':
                            break;
                    }
                    grid[i, j] = new Item();
                }
            }
            return grid;
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
