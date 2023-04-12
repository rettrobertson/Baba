using Baba.GameComponents;
using Baba.GameComponents.ConcreteComponents;
using Baba.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Baba.Views
{
    public class NewGameView : GameStateView
    {
        private KeyboardInput m_inputKeyboard;
        private GameStateEnum returnEnum = GameStateEnum.GamePlay;
        private GridMaker gridMaker;
        private List<Transform> transforms;
        public string[] level { get; set; } = new string[1] { "Level-1" };

        public override void loadContent(ContentManager contentManager)
        {
            base.loadContent(contentManager);
            gridMaker = new GridMaker();
            transforms = gridMaker.MakeGrid("levels-all.bbiy", level[0]);

            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Escape, true, new InputDeviceHelper.CommandDelegate(Escape));
            loadTextures(contentManager);

        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            m_inputKeyboard.Update(gameTime);
            //m_inputGamePad.Update(gameTime);
            //if return enum changed we'll go to the new view
            GameStateEnum temp = returnEnum;
            returnEnum = GameStateEnum.GamePlay;
            return temp;
        }
        public override void reset()
        {
            transforms = gridMaker.MakeGrid("levels-all.bbiy", level[0]);
        }
        public override void update(GameTime gameTime)
        {
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);
        }
        private void loadTextures(ContentManager contentManager)
        {

        }
        #region input handlers
        private void Escape(GameTime gameTime, float scale)
        {
            returnEnum = GameStateEnum.LevelSelect;
        }
        #endregion
    }
}
