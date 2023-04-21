using Microsoft.Xna.Framework;
using System;

namespace Baba.GameComponents.Systems
{
    public class CameraSystem
    {
        public static CameraSystem Instance;

        GameWindow window;

        private int width;
        private int height;
        private Vector2 gameStart;
        private const int halfGridSize = 10;

        private int renderScale;
        public int RenderScale => renderScale;

        private CameraSystem(GameWindow window)
        {
            this.window = window;
            width = window.ClientBounds.Width;
            height = window.ClientBounds.Height;
            window.ClientSizeChanged += OnResized;
        }

        public static void Initialize(GameWindow gameWindow)
        {
            Instance = new CameraSystem(gameWindow);
        }

        private void OnResized(object sender, EventArgs e)
        {
            width = window.ClientBounds.Width;
            height = window.ClientBounds.Height;

            renderScale = Math.Min(width, height) / 20;

            gameStart = new Vector2(width / 2, height / 2) - Vector2.One * renderScale * halfGridSize;
        }

        public Vector2 GameToScreenPos(Vector2 pos)
        {
            return pos * renderScale + gameStart;
        }

        public void Shake()
        {

        }
    }
}
