using Baba.Style;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace Baba.Views
{
    public class CreditsView : GameStateView
    {
        // very simple, just like the example given
        private SpriteFont m_font;
        private const string MESSAGE = "This game was created by";
        private const string MESSAGE1 = "Jason Crandall";
        private const string MESSAGE2 = "Rett Robertson";
        private const string MESSAGE3 = "And Tyler Totsch";
        private const string MESSAGE4 = "It was also beta tested by";
        private const string MESSAGE5 = "Tiffani Totsch and Annie Crandall";
        


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
            float bottom = DrawMenuItem(
                m_font,
                "Credits",

                200 * scale,
                 Color.Yellow);
            bottom = DrawMenuItem(m_font, MESSAGE, bottom, Color.Pink);
            bottom = DrawMenuItem(m_font, MESSAGE1, bottom, Color.LimeGreen);
            bottom = DrawMenuItem(m_font, MESSAGE2, bottom, Color.Turquoise);
            bottom = DrawMenuItem(m_font, MESSAGE3, bottom, Color.DarkSeaGreen);
            bottom = DrawMenuItem(m_font, MESSAGE4, bottom, Color.Red);
            bottom = DrawMenuItem(m_font, MESSAGE5, bottom, Color.Pink);
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

        public override void update(GameTime gameTime)
        {
        }
    }
}
