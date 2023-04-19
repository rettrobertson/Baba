
using System.Threading;

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
        protected GameComponents.Systems.SpriteRenderer m_renderer;

        protected Texture2D m_texBackground;
        protected Rectangle m_recBackground;
        
       

        public void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            m_graphics = graphics;
            m_spriteBatch = new SpriteBatch(graphicsDevice);

            
            
            

            
            m_renderer = new GameComponents.Systems.SpriteRenderer(graphicsDevice);
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
