using Baba.Input;
using DrawingExample.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System;


namespace Baba.Views
{
    public class LevelSelectView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private KeyboardInput m_inputKeyboard;
        private MouseInput m_inputMouse;
        private GameStateEnum returnEnum = GameStateEnum.LevelSelect;
        private enum MenuState
        {
            LevelOne,
            LevelTwo,
            LevelThree,
            LevelFour,
            LevelFive,
            MainMenu,
            Quit
        }
        private MenuState m_currentSelection = MenuState.LevelOne;
        
            
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
            Vector2 stringSize = m_fontMenu.MeasureString("Level 1");
            int y = 200;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetLevel1));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Level 2");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetLevel2));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            
            stringSize = m_fontMenu.MeasureString("Level 3");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetLevel3));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Level 4");
            y += (int)stringSize.Y;

            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetLevel4));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Level 5");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetLevel5));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Main Menu");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetMenu));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));


            stringSize = m_fontMenu.MeasureString("Quit");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetQuit));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));
            
            //m_inputGamePad = new GamePadInput(PlayerIndex.One);
        }
        public override GameStateEnum processInput(GameTime gameTime)
        {
            m_inputKeyboard.Update(gameTime);
            m_inputMouse.Update(gameTime);
            //m_inputGamePad.Update(gameTime);
            //if return enum changed we'll go to the new view
            GameStateEnum temp = returnEnum;
            returnEnum = GameStateEnum.LevelSelect;
            return temp;
        }
        public override void update(GameTime gameTime)
        {
            int x = 4;
        }
        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
            m_spriteBatch.Begin();

            // I split the first one's parameters on separate lines to help you see them better
            float bottom = DrawMenuItem(
                m_currentSelection == MenuState.LevelOne ? m_fontMenuSelect : m_fontMenu,
                "Level 1",
                200,
                m_currentSelection == MenuState.LevelOne ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(m_currentSelection == MenuState.LevelTwo ? m_fontMenuSelect : m_fontMenu, "Level 2", bottom, m_currentSelection == MenuState.LevelTwo ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(m_currentSelection == MenuState.LevelThree ? m_fontMenuSelect : m_fontMenu, "Level 3", bottom, m_currentSelection == MenuState.LevelThree ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(m_currentSelection == MenuState.LevelFour ? m_fontMenuSelect : m_fontMenu, "Level 4", bottom, m_currentSelection == MenuState.LevelFour ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(m_currentSelection == MenuState.LevelFive ? m_fontMenuSelect : m_fontMenu, "Level 5", bottom, m_currentSelection == MenuState.LevelFive ? Color.Yellow : Color.Blue);
            bottom = DrawMenuItem(m_currentSelection == MenuState.MainMenu ? m_fontMenuSelect : m_fontMenu, "Main Menu", bottom, m_currentSelection == MenuState.MainMenu ? Color.Yellow : Color.Blue);

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

        private void SetLevel1(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.LevelOne;
        }
        private void SetLevel2(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.LevelTwo;
        }
        private void SetLevel3(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.LevelThree;
        }
        private void SetLevel4(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.LevelFour;
        }
        private void SetLevel5(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.LevelFive;
        }
        private void SetMenu(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.MainMenu;
        }
        private void SetQuit(GameTime gametime, float scale)
        {
            m_currentSelection = MenuState.Quit;
        }
        private void OnDown(GameTime gametime, float scale)
        {
            m_currentSelection++;
            if (m_currentSelection > MenuState.Quit)
            {
                m_currentSelection = MenuState.LevelOne;
            }
        }

        private void OnUp(GameTime gametime, float scale)
        {
            m_currentSelection--;
            if (m_currentSelection < MenuState.LevelOne)
            {
                m_currentSelection = MenuState.Quit;
            }
        }

        private void OnEnter(GameTime gametime, float scale)
        {
            switch (m_currentSelection)
            {
                //changes the return enum
                case MenuState.LevelOne:
                    returnEnum =GameStateEnum.GamePlay;
                    break;
                case MenuState.LevelTwo:
                    returnEnum = GameStateEnum.GamePlay;
                    break;
                case MenuState.LevelThree:
                    returnEnum = GameStateEnum.GamePlay;
                    break;

                case MenuState.LevelFour:
                    returnEnum = GameStateEnum.GamePlay;
                    break;
                case MenuState.LevelFive:
                    returnEnum = GameStateEnum.GamePlay;
                    break;

                case MenuState.MainMenu:
                    returnEnum = GameStateEnum.MainMenu;
                    break;
                case MenuState.Quit:
                    returnEnum = GameStateEnum.Exit;
                    break;
            }
        }
        #endregion
    }

}