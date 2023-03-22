using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Baba.Input;
using System.Globalization;
using Microsoft.Xna.Framework.Graphics;

namespace DrawingExample.Input
{
    /// <summary>
    /// Derived input device for the PC Keyboard
    /// </summary>
    public class ScreenButton
    {

        private Rectangle rect;

        public Texture2D toptex { get; set; }

        public ScreenButton(Rectangle rect)
        {
            this.rect = rect;
        }
        public ScreenButton(int x, int y, int width, int height)
        {
            this.rect = new Rectangle(x, y, width, height);
        }

        public int GetX()
        {
            return rect.X;
        }
        public int GetY()
        {
            return rect.Y;
        }
        public int GetWidth()
        {
            return rect.Width;
        }
        public int GetHeight()
        {
            return rect.Height;
        }
    }

    public enum Click
    {
        Right,
        Left,
        Hover,
        Move
    }

    public class MouseInput : IInputDevice
    {
        /// <summary>
        /// Registers a callback-based command
        /// </summary>
        public void registerCommand(ScreenButton key, bool keyPressOnly, Click click, InputDeviceHelper.CommandDelegate callback)
        {
            //
            // If already registered, remove it!
            if (m_commandEntries.ContainsKey((key, click)))
            {
                m_commandEntries.Remove((key, click));
            }
            m_commandEntries.Add((key, click), new CommandEntry(key, keyPressOnly, click, callback));
        }

        /// <summary>
        /// Track all registered commands in this dictionary
        /// </summary>
        private Dictionary<(ScreenButton, Click), CommandEntry> m_commandEntries = new Dictionary<(ScreenButton, Click), CommandEntry>();

        /// <summary>
        /// Used to keep track of the details associated with a command
        /// </summary>
        private struct CommandEntry
        {
            public CommandEntry(ScreenButton key, bool keyPressOnly, Click click, InputDeviceHelper.CommandDelegate callback)
            {
                this.key = key;
                this.keyPressOnly = keyPressOnly;
                this.callback = callback;
                this.click = click;
            }

            public Click click;
            public ScreenButton key;
            public bool keyPressOnly;
            public InputDeviceHelper.CommandDelegate callback;
        }

        /// <summary>
        /// Goes through all the registered commands and invokes the callbacks if they
        /// are active.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            foreach (CommandEntry entry in this.m_commandEntries.Values)
            {
                if (entry.keyPressOnly && keyPressed(state, entry.click, entry.key))
                {
                    entry.callback(gameTime, 1.0f);
                }
                else if (!entry.keyPressOnly && isKeyDown(state, entry.click, entry.key))
                {
                    entry.callback(gameTime, 1.0f);
                }
            }

            //
            // Move the current state to the previous state for the next time around
            m_statePrevious = state;
        }

        private MouseState m_statePrevious;

        /// <summary>
        /// Checks to see if a key was newly pressed
        /// </summary>
        /// 

        private bool isKeyDown(MouseState state, Click click, ScreenButton key)
        {
            if (click == Click.Left)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    if (state.X >= key.GetX() && state.X <= key.GetX() + key.GetWidth() && state.Y >= key.GetY() && state.Y <= key.GetY() + key.GetHeight())
                    {
                        return true;
                    }
                }
            }
            else if (click == Click.Right)
            {
                if (state.RightButton == ButtonState.Pressed)
                {
                    if (state.X >= key.GetX() && state.X <= key.GetX() + key.GetWidth() && state.Y >= key.GetY() && state.Y <= key.GetY() + key.GetHeight())
                    {
                        return true;
                    }
                }
            }
            else if (click == Click.Hover)
            {
                if (state.X >= key.GetX() && state.X <= key.GetX() + key.GetWidth() && state.Y >= key.GetY() && state.Y <= key.GetY() + key.GetHeight())
                    {
                        return true;
                    }
            }
            else if (click == Click.Move)
            {
                if (state.X >= key.GetX() && state.X <= key.GetX() + key.GetWidth() && state.Y >= key.GetY() && state.Y <= key.GetY() + key.GetHeight())
                {
                    if (state.X != m_statePrevious.X || state.Y != m_statePrevious.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool keyPressed(MouseState state, Click click, ScreenButton key)
        {
            return (isKeyDown(state, click, key) && !isKeyDown(m_statePrevious, click, key));
        }
    }
}
