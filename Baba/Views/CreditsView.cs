using Baba.Style;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Baba.Views
{
    public class CreditsView : GameStateView
    {
        // very simple, just like the example given
        private SpriteFont m_font;
        private const string MESSAGE = "*This Page* done by Rett Robertson";
        private SoundEffect escape;
        private float fullWidth;
       
        public override void loadContent(ContentManager contentManager)
        {
            base.loadContent(contentManager);
            m_font = AssetManager.GetFont(Fonts.UI); //contentManager.Load<SpriteFont>("Fonts/menu");
            escape = AssetManager.GetSound("escape");
            fullWidth = m_graphics.GraphicsDevice.Viewport.Width;

        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                escape.Play();
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.Credits;
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
            m_spriteBatch.Begin();
            float scale = m_graphics.GraphicsDevice.Viewport.Width / fullWidth;
            Vector2 stringSize = m_font.MeasureString(MESSAGE);
            m_spriteBatch.DrawString(m_font, MESSAGE,
                new Vector2(m_graphics.GraphicsDevice.Viewport.Width / 2 - stringSize.X / 2, m_graphics.GraphicsDevice.Viewport.Height / 2 - stringSize.Y/2), Color.Yellow, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

            m_spriteBatch.End();
        }

        public override void update(GameTime gameTime)
        {
        }
    }
}
