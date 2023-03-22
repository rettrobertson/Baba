using Microsoft.Xna.Framework;

namespace Baba.Input
{
    /// <summary>
    /// Abstract base class that defines how input is presented to game code.
    /// </summary>
    public interface IInputDevice
    {
        void Update(GameTime gameTime);
    }

    public class InputDeviceHelper
    {
        public delegate void CommandDelegate0(GameTime gameTime);
        public delegate void CommandDelegate(GameTime gameTime, float obj);
        public delegate void CommandDelegatePosition(GameTime GameTime, int x, int y);
    }
}
