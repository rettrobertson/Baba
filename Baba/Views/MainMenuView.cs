using Baba.Input;
using DrawingExample.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using Baba.Style;
using System.Reflection.Metadata.Ecma335;

namespace Baba.Views
{
    public class MainMenuView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private KeyboardInput m_inputKeyboard;
        private MouseInput m_inputMouse;
        private GamePadInput m_inputGamePad;
        private GameStateEnum returnEnum = GameStateEnum.MainMenu;
        private MenuState prevEnum = MenuState.LevelSelect;
        private SoundEffect effect;
        private SoundEffect enter;
        private float fullWidth;

        //enum for the different menus, borrowed from startercode
        private enum MenuState
        {
            LevelSelect,
            Controls,
            Credits,
           
            Quit
        }

        private MenuState m_currentSelection = MenuState.LevelSelect;
        public override void loadContent(ContentManager contentManager)
        {
            fullWidth = m_graphics.GraphicsDevice.Viewport.Width;
            base.loadContent(contentManager);
            //Keyboard input for up down and enter
            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Down, true, new InputDeviceHelper.CommandDelegate(OnDown));
            m_inputKeyboard.registerCommand(Keys.Up, true, new InputDeviceHelper.CommandDelegate(OnUp));
            m_inputKeyboard.registerCommand(Keys.W, true, new InputDeviceHelper.CommandDelegate(OnUp));
            m_inputKeyboard.registerCommand(Keys.S, true, new InputDeviceHelper.CommandDelegate(OnDown));

            m_inputKeyboard.registerCommand(Keys.Enter, true, new InputDeviceHelper.CommandDelegate(OnEnter));
            m_inputKeyboard.registerCommand(Keys.Space, true, new InputDeviceHelper.CommandDelegate(OnEnter));


            m_fontMenu = AssetManager.GetFont(Fonts.UI);// contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = AssetManager.GetFont(Fonts.UI); //contentManager.Load<SpriteFont>("Fonts/menu-select");

            m_inputMouse = new MouseInput();
            Vector2 stringSize = m_fontMenu.MeasureString("Level Selector");
            int y = m_graphics.GraphicsDevice.Viewport.Height / 5 * 2;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetNew));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Controls");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetControls));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("About");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetCredits));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Quit");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetQuit));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            m_inputGamePad = new GamePadInput(PlayerIndex.One);

            effect = AssetManager.GetSound("menu-bump");
            enter = AssetManager.GetSound("enter");
        }
        public override GameStateEnum processInput(GameTime gameTime)
        {
            
            m_inputKeyboard.Update(gameTime);
            m_inputMouse.Update(gameTime);
            m_inputGamePad.Update(gameTime);
            if (prevEnum != m_currentSelection)
            {
                effect.Play();
            }
            prevEnum = m_currentSelection;
            //if return enum changed we'll go to the new view
            GameStateEnum temp = returnEnum;
            returnEnum = GameStateEnum.MainMenu;
            return temp;
        }
        public override void update(GameTime gameTime)
        {
        }
        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
            m_spriteBatch.Begin(samplerState:SamplerState.PointClamp);
            float scale = m_graphics.GraphicsDevice.Viewport.Width / fullWidth;
            Vector2 stringSize = m_fontMenu.MeasureString("-- BABA IS YOU --") * scale *  1.6f;
            

           
            m_spriteBatch.DrawString(m_fontMenu, "-- BABA IS YOU --", new Vector2(m_graphics.GraphicsDevice.Viewport.Width /*m_graphics.PreferredBackBufferWidth*/ / 2 - stringSize.X / 2, m_graphics.GraphicsDevice.Viewport.Height / 10), Colors.title, 0, Vector2.Zero, 1.6f * scale, SpriteEffects.None, 0);

            // I split the first one's parameters on separate lines to help you see them better
            float bottom = DrawMenuItem(
                m_currentSelection == MenuState.LevelSelect ? m_fontMenuSelect : m_fontMenu,
                "Level Selector",

                400 * scale,
                m_currentSelection == MenuState.LevelSelect ? Color.Yellow : Colors.text1);
            bottom = DrawMenuItem(m_currentSelection == MenuState.Controls ? m_fontMenuSelect : m_fontMenu, "Controls", bottom, m_currentSelection == MenuState.Controls ? Color.Yellow : Colors.text2);
            bottom = DrawMenuItem(m_currentSelection == MenuState.Credits ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.Credits ? Color.Yellow : Colors.text3);
           

            DrawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.Yellow : Colors.text4);

            m_spriteBatch.End();
        }

        private float DrawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            float scale = m_graphics.GraphicsDevice.Viewport.Width / fullWidth;
            Vector2 stringSize = font.MeasureString(text) * scale;
            
            m_spriteBatch.DrawString(
                font,
                text,
                new Vector2(m_graphics.GraphicsDevice.Viewport.Width /*m_graphics.PreferredBackBufferWidth*/ / 2 - stringSize.X / 2, y),
                color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

            return y + stringSize.Y;
        }

        #region input Handlers
        // if statements on down and up to ensure wraparound

        private void SetNew(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.LevelSelect;
        }
        private void SetControls(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.Controls;
        }
        private void SetCredits(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.Credits;
        }
        

        private void SetQuit(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.Quit;
        }
        private void OnDown(GameTime gametime, float scale)
        {
            m_currentSelection ++;
            if (m_currentSelection > MenuState.Quit)
            {
                m_currentSelection = MenuState.LevelSelect;
            }
        }

        private void OnUp(GameTime gametime, float scale)
        {
            m_currentSelection--;
            if (m_currentSelection  < MenuState.LevelSelect) 
            {
                m_currentSelection = MenuState.Quit;
            }
        }

        private void OnEnter(GameTime gametime, float scale)
        {
            
            switch (m_currentSelection)
            {
                //changes the return enum
                case MenuState.LevelSelect:
                    returnEnum = GameStateEnum.LevelSelect;
                    break;
                case MenuState.Controls:
                    returnEnum = GameStateEnum.Controls;
                    break;
                case MenuState.Credits:
                    returnEnum = GameStateEnum.Credits;
                    break;
                case MenuState.Quit:
                    returnEnum = GameStateEnum.Exit;
                    break;
            }
            enter.Play();
        }
        #endregion
    }
}
