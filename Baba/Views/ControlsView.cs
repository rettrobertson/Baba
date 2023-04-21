using System.Collections.Generic;
using System.Threading;
using Baba.Input;
using DrawingExample.Input;
using Baba.Views.SavingControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Baba.Views
{
    public class ControlsView : GameStateView
    {
        // also very simple, just like the example given
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;

        private KeyboardInput m_inputKeyboard;
        private MouseInput m_inputMouse;
        private GamePadInput m_inputGamePad;
        private bool getNewControl = false;
        GameState controls;
        

        
        public ControlsView(ref GameState  controls)
        {
            this.controls = controls;
        }
        private enum ControlsState
        {
            Up = 0,
            Down = 1,
            Left= 2,
            Right =3 ,
            Reset = 4,
            Undo = 5
        }
        private ControlsState m_currentSelection = ControlsState.Up;

        private const string MESSAGE = "Use arrow keys to play the game, and esc to pause. \n     r will reset the scores on highscores page";

        public override void loadContent(ContentManager contentManager)
        {
            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Down, true, new InputDeviceHelper.CommandDelegate(OnDown));
            m_inputKeyboard.registerCommand(Keys.Up, true, new InputDeviceHelper.CommandDelegate(OnUp));
            m_inputKeyboard.registerCommand(Keys.Enter, true, new InputDeviceHelper.CommandDelegate(OnEnter));
            m_inputKeyboard.registerCommand(Keys.Space, true, new InputDeviceHelper.CommandDelegate(OnEnter));


            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");

            m_inputMouse = new MouseInput();
            Vector2 stringSize = m_fontMenu.MeasureString("Up");
            int y = 200;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetUp));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Down");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetDown));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Left");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetLeft));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Right");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetRight));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Reset");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetReset));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            stringSize = m_fontMenu.MeasureString("Undo");
            y += (int)stringSize.Y;
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), false, Click.Move, new InputDeviceHelper.CommandDelegate(SetUndo));
            m_inputMouse.registerCommand(new ScreenButton((int)(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2), y, (int)stringSize.X, (int)stringSize.Y), true, Click.Left, new InputDeviceHelper.CommandDelegate(OnEnter));

            m_inputGamePad = new GamePadInput(PlayerIndex.One);
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (getNewControl)
            {
                SaveData savedControls = new SaveData();
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    savedControls = new SaveData();
                    int selection = (int)m_currentSelection;
                    controls.Controls[selection] = keys[0];
                    savedControls.saveSomething(controls);
                    getNewControl = false;
                }


            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }
            
            m_inputKeyboard.Update(gameTime);
            m_inputMouse.Update(gameTime);
            m_inputGamePad.Update(gameTime);
            //if return enum changed we'll go to the new view
            
            
           
            

            return GameStateEnum.Controls;
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
            m_spriteBatch.Begin();
            if (!getNewControl)
            {
                float bottom = DrawMenuItem(
                              m_currentSelection == ControlsState.Up ? m_fontMenuSelect : m_fontMenu,
                              $"Up {controls.Controls[0]} ",
                              200,
                              m_currentSelection == ControlsState.Up ? Color.Yellow : Color.Blue);
                bottom = DrawMenuItem(m_currentSelection == ControlsState.Down ? m_fontMenuSelect : m_fontMenu, $"Down {controls.Controls[1]}", bottom, m_currentSelection == ControlsState.Down ? Color.Yellow : Color.Blue);
                bottom = DrawMenuItem(m_currentSelection == ControlsState.Left ? m_fontMenuSelect : m_fontMenu, $"Left {controls.Controls[2]}", bottom, m_currentSelection == ControlsState.Left ? Color.Yellow : Color.Blue);
                bottom = DrawMenuItem(m_currentSelection == ControlsState.Right ? m_fontMenuSelect : m_fontMenu, $"Right {controls.Controls[3]}", bottom, m_currentSelection == ControlsState.Right ? Color.Yellow : Color.Blue);
                bottom = DrawMenuItem(m_currentSelection == ControlsState.Reset ? m_fontMenuSelect : m_fontMenu, $"Reset {controls.Controls[4]}", bottom, m_currentSelection == ControlsState.Reset ? Color.Yellow : Color.Blue);


                DrawMenuItem(m_currentSelection == ControlsState.Undo ? m_fontMenuSelect : m_fontMenu, $"Undo {controls.Controls[5]}", bottom, m_currentSelection == ControlsState.Undo ? Color.Yellow : Color.Blue);
            }
            else
            {
                m_spriteBatch.DrawString(m_fontMenu, "Enter the new they key for this command", new Vector2(1920 / 2 - 400, 1080 / 2), Color.Yellow);
            }
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

        public override void update(GameTime gameTime)
        {
        }

        #region input Handlers
        // if statements on down and up to ensure wraparound

        private void SetUp(GameTime gametime, float scale)
        {
            m_currentSelection = ControlsState.Up;
        }
        private void SetDown(GameTime gametime, float scale)
        {
            m_currentSelection = ControlsState.Down;
        }
        private void SetLeft(GameTime gametime, float scale)
        {
            m_currentSelection = ControlsState.Left;
        }


        private void SetRight(GameTime gametime, float scale)
        {
            m_currentSelection = ControlsState.Right;
        }
        private void SetReset(GameTime gametime, float scale)
        {
            m_currentSelection = ControlsState.Reset;
        }
        private void SetUndo(GameTime gametime, float scale)
        {
            m_currentSelection = ControlsState.Undo;
        }
        private void OnDown(GameTime gametime, float scale)
        {
            m_currentSelection++;
            if (m_currentSelection > ControlsState.Undo)
            {
                m_currentSelection = ControlsState.Up;
            }
        }

        private void OnUp(GameTime gametime, float scale)
        {
            m_currentSelection--;
            if (m_currentSelection < ControlsState.Up)
            {
                m_currentSelection = ControlsState.Undo;
            }
        }

        private void OnEnter(GameTime gametime, float scale)
        {
            
            getNewControl = true;
        }
        #endregion
    }
}
