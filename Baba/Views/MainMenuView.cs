using Baba.Input;
using DrawingExample.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Threading;

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
            base.loadContent(contentManager);
            //Keyboard input for up down and enter
            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Down, true, new InputDeviceHelper.CommandDelegate(OnDown));
            m_inputKeyboard.registerCommand(Keys.Up, true, new InputDeviceHelper.CommandDelegate(OnUp));
            m_inputKeyboard.registerCommand(Keys.Enter, true, new InputDeviceHelper.CommandDelegate(OnEnter));
            m_inputKeyboard.registerCommand(Keys.Space, true, new InputDeviceHelper.CommandDelegate(OnEnter));


            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");

            m_inputMouse = new MouseInput();
            Vector2 stringSize = m_fontMenu.MeasureString("Select");
            int y = 200;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetNew));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Controls");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetControls));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("About");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetCredits));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Quit");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetQuit));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            m_inputGamePad = new GamePadInput(PlayerIndex.One);
        }
        public override GameStateEnum processInput(GameTime gameTime)
        {
            m_inputKeyboard.Update(gameTime);
            m_inputMouse.Update(gameTime);
            m_inputGamePad.Update(gameTime);
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
            m_spriteBatch.Begin();

            // I split the first one's parameters on separate lines to help you see them better
            float bottom = DrawMenuItem(
                m_currentSelection == MenuState.LevelSelect ? m_fontMenuSelect : m_fontMenu,
                "Level Selector",
                200,
                m_currentSelection == MenuState.LevelSelect ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(m_currentSelection == MenuState.Controls ? m_fontMenuSelect : m_fontMenu, "Controls", bottom, m_currentSelection == MenuState.Controls ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(m_currentSelection == MenuState.Credits ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.Credits ? Color.Yellow : Color.Blue);
           

            DrawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.Yellow : Color.Blue);

            m_spriteBatch.End();
        }

        private float DrawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            m_spriteBatch.DrawString(
                font,
                text,
                new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                color);

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
/*            Thread.Sleep(50);
*/            switch (m_currentSelection)
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
        }
        #endregion
    }
}