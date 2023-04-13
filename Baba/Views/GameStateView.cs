using Engine.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Baba.Views
{
    public abstract class GameStateView : IGameState
    {
        protected GraphicsDeviceManager m_graphics;
        protected SpriteBatch m_spriteBatch;

        protected Texture2D m_texBackground;
        protected Rectangle m_recBackground;
        protected GameState controls;

        public void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            m_graphics = graphics;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            SaveData savedControls = new SaveData();
            savedControls.loadSomething();
            while(savedControls.getIsLoading()) { }
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

        }
        public virtual void loadContent(ContentManager contentManager)
        {

        }
        public abstract GameStateEnum processInput(GameTime gameTime);
        public virtual void render(GameTime gameTime)
        {
        }
        public abstract void update(GameTime gameTime);
        public virtual void reset() { }

    }
}
