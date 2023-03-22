using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Baba.Input
{
    /// <summary>
    /// Derived input device for the XBox 360 controller
    /// </summary>
    public class GamePadInput : IInputDevice
    {
        public GamePadInput(PlayerIndex index)
        {
            m_playerIndex = index;
        }
        protected PlayerIndex m_playerIndex;

        /// <summary>
        /// Register a command that has an analog value associated with it
        /// </summary>
        public void registerCommand(Buttons button, bool buttonPressOnly, InputDeviceHelper.CommandDelegate callback)
        {
            //
            // If already registered, remove it!
            if (m_commandEntries.ContainsKey(button))
            {
                m_commandEntries.Remove(button);
            }
            m_commandEntries.Add(button, new CommandEntry(button, buttonPressOnly, callback));
        }

        /// <summary>
        /// Track all registered commands in this dictionary
        /// </summary>
        private Dictionary<Buttons, CommandEntry> m_commandEntries = new Dictionary<Buttons, CommandEntry>();

        /// <summary>
        /// Used to keep track of the details associated with a command
        /// </summary>
        private struct CommandEntry
        {
            public CommandEntry(Buttons button, bool buttonPressOnly, InputDeviceHelper.CommandDelegate callback)
            {
                this.button = button;
                this.buttonPressOnly = buttonPressOnly;
                this.callback = callback;
            }

            public Buttons button;
            public bool buttonPressOnly;
            public InputDeviceHelper.CommandDelegate callback;
        }

        /// <summary>
        /// Go through and fire any callback that are active
        /// </summary>
        public void Update(GameTime gameTime)
        {
            GamePadState state = GamePad.GetState(m_playerIndex);

            foreach (CommandEntry entry in m_commandEntries.Values)
            {
                if (entry.buttonPressOnly && buttonPressed(entry.button))
                {
                    //
                    // If Button press, it is always a value of 1.0
                    entry.callback(gameTime, 1.0f);

                }
                else if (!entry.buttonPressOnly && state.IsButtonDown(entry.button))
                {
                    //
                    // Now, if this button has an analog value associated with it, get it!
                    // I'm not in love with this switch statement, but it works.
                    switch (entry.button)
                    {
                        case Buttons.LeftThumbstickLeft:
                            entry.callback(gameTime, -1.0f * state.ThumbSticks.Left.X);       // Have to transform it into a positive "left"
                            break;
                        case Buttons.LeftThumbstickRight:
                            entry.callback(gameTime, state.ThumbSticks.Left.X);
                            break;
                        case Buttons.LeftThumbstickUp:
                            entry.callback(gameTime, state.ThumbSticks.Left.Y);
                            break;
                        case Buttons.LeftThumbstickDown:
                            entry.callback(gameTime, -1.0f * state.ThumbSticks.Left.Y);     // Have to transform it into a positive "down"
                            break;
                        default:
                            entry.callback(gameTime, 1.0f);
                            break;
                    }
                }
            }

            //
            // Move the current state to the previous state for the next time around
            m_statePrevious = state;
        }

        private GamePadState m_statePrevious;

        /// <summary>
        /// Checks to see if a button was newly pressed
        /// </summary>
        private bool buttonPressed(Buttons button)
        {
            return (GamePad.GetState(m_playerIndex).IsButtonDown(button) && !m_statePrevious.IsButtonDown(button));
        }
    }
}
