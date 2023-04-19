using Baba;
using Baba.GameComponents;
using Baba.GameComponents.ConcreteComponents;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Baba
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // for testing the gridmaker
            /*GridMaker maker = new GridMaker();
            List<Transform> transforms = maker.MakeGrid("Level-1");
            maker.TestList(transforms);
*/
            using (var game = new Assignment())
            {
                game.Run();
            }
        }
    }
}
