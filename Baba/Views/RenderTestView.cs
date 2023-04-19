using Baba.GameComponents;
using Baba.GameComponents.ConcreteComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Baba.Views
{
    public class RenderTestView : GameStateView
    {
        public override void loadContent(ContentManager contentManager)
        {
            base.loadContent(contentManager);
            m_renderer.LoadWords(contentManager);
            m_renderer.LoadItems(contentManager);

            Entity test = new Entity();
            test.AddComponent(new ItemLabel(ItemType.Baba));
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            return GameStateEnum.RenderTest;
        }

        public override void update(GameTime gameTime)
        {

        }
    }
}
