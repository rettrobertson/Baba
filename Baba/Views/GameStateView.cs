
using System.Threading;
using Baba.GameComponents.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Baba.Views
{
    public abstract class GameStateView : IGameState
    {
        protected GraphicsDeviceManager m_graphics;
        protected SpriteBatch m_spriteBatch;
        protected SpriteRenderer m_renderer;
        public ComponentRouterSystem router;

        protected Texture2D m_texBackground;
        protected Rectangle m_recBackground;
        
       

        public void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            m_graphics = graphics;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            router = new ComponentRouterSystem();
            m_renderer = new SpriteRenderer(this, graphicsDevice);
        }
        public virtual void loadContent(ContentManager contentManager)
        {

        }
        public abstract GameStateEnum processInput(GameTime gameTime);
        public virtual void render(GameTime gameTime)
        {
            m_renderer.Render();
        }
        public abstract void update(GameTime gameTime);
        public virtual void reset() { }

    }
}
