﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Galaga.Input;
using System.Globalization;

namespace DrawingExample.Input
{
    /// <summary>
    /// Derived input device for the PC Keyboard
    /// </summary>
    public class MouseInput : IInputDevice
    {
        /// <summary>
        /// Registers a callback-based command
        /// </summary>
        public void registerCommand(Button key, bool keyPressOnly, Click click, InputDeviceHelper.CommandDelegate callback)
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
        private Dictionary<(Button, Click), CommandEntry> m_commandEntries = new Dictionary<(Button, Click), CommandEntry>();

        /// <summary>
        /// Used to keep track of the details associated with a command
        /// </summary>
        private struct CommandEntry
        {
            public CommandEntry(Button key, bool keyPressOnly, Click click, InputDeviceHelper.CommandDelegate callback)
            {
                this.key = key;
                this.keyPressOnly = keyPressOnly;
                this.callback = callback;
                this.click = click;
            }

            public Click click;
            public Button key;
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
                    entry.callback(gameTime);
                }
                else if (!entry.keyPressOnly && isKeyDown(state, entry.click, entry.key))
                {
                    entry.callback(gameTime);
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

        private bool isKeyDown(MouseState state, Click click, Button key)
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
            return false;
        }
        private bool keyPressed(MouseState state, Click click, Button key)
        {
            return (isKeyDown(state, click, key) && !isKeyDown(m_statePrevious, click, key));
        }
    }
}
