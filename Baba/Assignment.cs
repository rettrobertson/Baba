using Baba.Views;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Xml;
using Baba.Views.SavingControls;
using BreakoutGame.Engine;

namespace Baba
{
    public class Assignment : Game
    {
        private IGameState m_currentState;
        private GameStateEnum m_nextStateEnum = GameStateEnum.MainMenu;
        private Dictionary<GameStateEnum, IGameState> m_states;

        private int height;
        private int width;
        private GraphicsDeviceManager m_graphics;
        protected GameState controls;

        public Assignment()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            m_graphics.PreferredBackBufferWidth = width;
            m_graphics.PreferredBackBufferHeight = height;

            m_graphics.ApplyChanges();
            
            LevelSelectView temp2 = new LevelSelectView();

            
            SaveData savedControls = new SaveData();
            savedControls.loadSomething();
            while (savedControls.getIsLoading()) { }
            controls = savedControls.m_loadedState;
            if (controls == null)
            {
                Keys[] controlList = new Keys[6];
                controlList[0] = Keys.W;
                controlList[1] = Keys.A;
                controlList[2] = Keys.S;
                controlList[3] = Keys.D;
                controlList[4] = Keys.R;
                controlList[5] = Keys.Z;

                controls = new GameState(controlList);
            }
            NewGameView temp = new NewGameView(ref controls);
            temp2.LevelInfo(temp.level);
            m_states = new Dictionary<GameStateEnum, IGameState>
            {
                { GameStateEnum.MainMenu, new MainMenuView() },
                { GameStateEnum.GamePlay, temp },
                { GameStateEnum.Controls, new ControlsView(ref controls) },
                { GameStateEnum.Credits, new CreditsView() },
                { GameStateEnum.LevelSelect, temp2 },
                { GameStateEnum.RenderTest, new RenderTestView() }
            };

            foreach (var item in m_states)
            {
                item.Value.initialize(this.GraphicsDevice, m_graphics);
            }

            m_currentState = m_states[m_nextStateEnum];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            AssetManager.LoadContent(Content);
            // box is an static class to tell where the balls' bounds are
            foreach (var item in m_states)
            {
                item.Value.loadContent(this.Content);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            GameStateEnum temp = m_nextStateEnum;
            m_nextStateEnum = m_currentState.processInput(gameTime);
            if (m_nextStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }
            if (m_nextStateEnum == GameStateEnum.GamePlay && temp != GameStateEnum.GamePlay)
            {
                m_states[GameStateEnum.GamePlay].reset();
            }
            m_currentState.update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_currentState.render(gameTime);
            m_currentState = m_states[m_nextStateEnum];

            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}