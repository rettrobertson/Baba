using Baba.Input;
using DrawingExample.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using Baba.GameComponents;

namespace Baba.Views
{
    public class LevelSelectView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private KeyboardInput m_inputKeyboard;
        private MouseInput m_inputMouse;
        private GameStateEnum returnEnum = GameStateEnum.LevelSelect;
        public string[] level { get; set; }
        private List<String> MenuState = new List<String>();
        private List<Action<GameTime, float>> lambdaList = new List<Action<GameTime, float>>();
        private int m_currentSelectionInt;
        private string m_currentSelection;
        
            
        public override void loadContent(ContentManager contentManager)
        {
            GridMaker gridMaker = new GridMaker();
            MenuState = gridMaker.getLevels();
            base.loadContent(contentManager);
            //Keyboard input for up down and enter
            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Down, true, new InputDeviceHelper.CommandDelegate(OnDown));
            m_inputKeyboard.registerCommand(Keys.Up, true, new InputDeviceHelper.CommandDelegate(OnUp));
            m_inputKeyboard.registerCommand(Keys.Enter, true, new InputDeviceHelper.CommandDelegate(OnEnter));
            m_inputKeyboard.registerCommand(Keys.Space, true, new InputDeviceHelper.CommandDelegate(OnEnter));
            m_inputKeyboard.registerCommand(Keys.Escape, true, new InputDeviceHelper.CommandDelegate(Escape));
            m_inputKeyboard.registerCommand(Keys.W, true, new InputDeviceHelper.CommandDelegate(OnUp));
            m_inputKeyboard.registerCommand(Keys.S, true, new InputDeviceHelper.CommandDelegate(OnDown));


            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
            m_inputMouse = new MouseInput();
            
            int y = 200;
            m_currentSelectionInt= 0;
            m_currentSelection = MenuState[0];
            for (int i = 0; i < MenuState.Count;i++)
            {
                Vector2 stringSize = m_fontMenu.MeasureString(MenuState[i]);
                if (i != 0)
                {
                    y += (int)stringSize.Y;
                }
                int x = i;
                lambdaList.Add( (gameTime, val) => {
                    m_currentSelectionInt = x;
                    m_currentSelection = MenuState[m_currentSelectionInt];
                });
                m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(lambdaList[i]));
                m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));
            }
           
            

           
            
            
           /* stringSize = m_fontMenu.MeasureString("Main Menu");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetMenu));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));


            stringSize = m_fontMenu.MeasureString("Quit");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetQuit));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));
            */
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
        }
        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
            m_spriteBatch.Begin();

            // I split the first one's parameters on separate lines to help you see them better
            
            float bottom = 200;
            for(int i = 0; i < MenuState.Count; i++)
            {
                bottom = DrawMenuItem(
                m_currentSelection == MenuState[i] ? m_fontMenuSelect : m_fontMenu,
                MenuState[i],
                bottom,
                m_currentSelection == MenuState[i] ? Color.Yellow : Color.Gainsboro);
            }
          
            /*bottom = DrawMenuItem(m_currentSelection == MenuState.MainMenu ? m_fontMenuSelect : m_fontMenu, "Main Menu", bottom, m_currentSelection == MenuState.MainMenu ? Color.Yellow : Color.Blue);

            DrawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.Yellow : Color.Blue);*/

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
        public void LevelInfo(string[] level)
        {
            this.level = level;
        }
        #region input Handlers
        // if statements on down and up to ensure wraparound

      
      
        private void Escape(GameTime gametime, float scale)
        {

            returnEnum = GameStateEnum.MainMenu;
        }
       
        private void OnDown(GameTime gametime, float scale)
        {
            m_currentSelectionInt++;
            if (m_currentSelectionInt > MenuState.Count - 1)
            {
                m_currentSelectionInt = 0;
                
            }
            m_currentSelection = MenuState[m_currentSelectionInt];
        }

        private void OnUp(GameTime gametime, float scale)
        {
            m_currentSelectionInt--;
            if (m_currentSelectionInt < 0)
            {
                m_currentSelectionInt = MenuState.Count - 1;
                
            }
            m_currentSelection = MenuState[m_currentSelectionInt];

        }

        private void OnEnter(GameTime gametime, float scale)
        {  
            returnEnum =GameStateEnum.GamePlay;
            level[0] = MenuState[m_currentSelectionInt];
        }
        #endregion
    }

}