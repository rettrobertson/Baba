﻿using Baba.GameComponents;
using Baba.GameComponents.ConcreteComponents;
using Baba.GameComponents.Systems;
using Baba.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Baba.Views.SavingControls;

namespace Baba.Views
{
    public class NewGameView : GameStateView
    {
        private KeyboardInput m_inputKeyboard;
        private GameStateEnum returnEnum = GameStateEnum.GamePlay;
        private GridMaker gridMaker;
        private List<Transform> transforms;
        private Entity entity;
        private GameState controls;
        public string[] level { get; set; } = new string[1] { "Level-1" };
        protected SpriteRenderer m_renderer;
        public ComponentRouterSystem router;
        private AnimationSystem animationSystem;
        private RuleSystem ruleSystem;
        private MoveSystem moveSystem;
        private UndoSystem undoSystem;

        public NewGameView(ref GameState controls)
        {
            this.controls = controls;
        }
        public override void loadContent(ContentManager contentManager)
        {
            base.loadContent(contentManager);

            router = new ComponentRouterSystem();
            ruleSystem = new RuleSystem(this);
            animationSystem = new AnimationSystem(this);
            m_renderer = new SpriteRenderer(this, m_graphics.GraphicsDevice);
            gridMaker = new GridMaker();
            moveSystem = new MoveSystem(this);
            undoSystem = new UndoSystem(this);

            undoSystem.OnUndo += ruleSystem.UpdateRules;

            loadTextures(contentManager);
            transforms = gridMaker.MakeGrid(level[0]);
            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Escape, true, new InputDeviceHelper.CommandDelegate(Escape));
            m_inputKeyboard.registerCommand(controls.Controls[0], true, new InputDeviceHelper.CommandDelegate(moveUP));
            m_inputKeyboard.registerCommand(controls.Controls[1], true, new InputDeviceHelper.CommandDelegate(moveDown));
            m_inputKeyboard.registerCommand(controls.Controls[2], true, new InputDeviceHelper.CommandDelegate(moveLeft));
            m_inputKeyboard.registerCommand(controls.Controls[3], true, new InputDeviceHelper.CommandDelegate(moveRight));
            m_inputKeyboard.registerCommand(controls.Controls[4], true, new InputDeviceHelper.CommandDelegate(ResetKeyPress));
            m_inputKeyboard.registerCommand(controls.Controls[5], true, new InputDeviceHelper.CommandDelegate(Undo));


            ruleSystem.UpdateRules();
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
            transforms = gridMaker.MakeGrid(level[0]);
            ruleSystem.Reset();
            animationSystem.Reset();
            moveSystem.Reset();
        }
        public override void update(GameTime gameTime)
        {
            animationSystem.Update(gameTime);
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);

            m_renderer.Render();
            m_renderer.Render();
        }
        private void loadTextures(ContentManager contentManager)
        {
            m_renderer.LoadWords(contentManager);
            m_renderer.LoadItems(contentManager);
        }
        #region input handlers
        private void Escape(GameTime gameTime, float scale)
        {
            returnEnum = GameStateEnum.LevelSelect;
        }
        private void moveUP(GameTime gameTime, float scale)
        {
            moveSystem.moveEntity(gameTime, "Up");
            undoSystem.ArrowKeyPress();
        }
        private void moveDown(GameTime gameTime, float scale)
        {
            moveSystem.moveEntity(gameTime, "Down");
            undoSystem.ArrowKeyPress();
        }
        private void moveLeft(GameTime gameTime, float scale)
        {
            moveSystem.moveEntity(gameTime, "Left");
            undoSystem.ArrowKeyPress();
        }
        private void moveRight(GameTime gameTime, float scale)
        {
            moveSystem.moveEntity(gameTime, "Right");
            undoSystem.ArrowKeyPress();
        }
        private void Undo(GameTime gameTime, float scale)
        {
            undoSystem.UndoKeyPress();
        }
        private void ResetKeyPress(GameTime gameTime, float scale)
        {
            reset();
        }
        #endregion
    }
}
