using Baba.GameComponents.ConcreteComponents;
using Baba.Views;
using Microsoft.Xna.Framework;
using System;
using System.Globalization;

namespace Baba.GameComponents.Systems
{
    public class CameraSystem : System
    {
        GraphicsDeviceManager graphicsDeviceManager;
        GameWindow window;

        private int width;
        private int height;

        public CameraSystem(NewGameView view, GameWindow window) : base(view, typeof(Camera))
        {
            this.window = window;
            window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnResized(EventHandler<EventArgs> args)
        {
            width = window.ClientBounds.Width;
            height = window.ClientBounds.Height;
        }

        public override void Reset()
        {
        }
    }
}
