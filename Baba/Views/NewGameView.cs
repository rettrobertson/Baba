using Baba.GameComponents;
using Baba.GameComponents.ConcreteComponents;
using Baba.GameComponents.Systems;
using Baba.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Baba.Views.SavingControls;
namespace Baba.Views
{
    public class NewGameView : GameStateView
    {
        private enum State{
            StartParticle,
            Win,
            Play
        }
        private State state;
        private KeyboardInput m_inputKeyboard;
        private GameStateEnum returnEnum = GameStateEnum.GamePlay;
        private GridMaker gridMaker;
        private List<Transform> transforms = new List<Transform>();
        private GameState controls;
        public string[] level { get; set; } = new string[1] { "Level-1" };
        protected SpriteRenderer m_renderer;
        public ComponentRouterSystem router;
        private AnimationSystem animationSystem;
        private RuleSystem ruleSystem;
        private MoveSystem moveSystem;
        private UndoSystem undoSystem;
        private KillSystem killSystem;
        private SinkSystem sinkSystem;
        private WinSystem winSystem;
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
            killSystem = new(this);
            sinkSystem = new(this);
            winSystem = new(this);

            undoSystem.OnUndo += ruleSystem.UpdateRules;
            undoSystem.OnUndo += animationSystem.UpdateAnimations;
            undoSystem.OnUndo += m_renderer.UpdateSprites;

            ruleSystem.onTransformationsFinished += animationSystem.UpdateAnimations;
            ruleSystem.onTransformationsFinished += m_renderer.UpdateSprites;

            loadTextures(contentManager);
            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Escape, true, new InputDeviceHelper.CommandDelegate(Escape));
            m_inputKeyboard.registerCommand(controls.Controls[0], true, new InputDeviceHelper.CommandDelegate(moveUp));
            m_inputKeyboard.registerCommand(controls.Controls[1], true, new InputDeviceHelper.CommandDelegate(moveDown));
            m_inputKeyboard.registerCommand(controls.Controls[2], true, new InputDeviceHelper.CommandDelegate(moveLeft));
            m_inputKeyboard.registerCommand(controls.Controls[3], true, new InputDeviceHelper.CommandDelegate(moveRight));
            m_inputKeyboard.registerCommand(controls.Controls[4], true, new InputDeviceHelper.CommandDelegate(ResetKeyPress));
            m_inputKeyboard.registerCommand(controls.Controls[5], true, new InputDeviceHelper.CommandDelegate(Undo));
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (state == State.StartParticle)
            {
                // start particle effects for win
                state = State.Win;
            }
            else if (state == State.Play || state == State.Win)
            {
                m_inputKeyboard.Update(gameTime);
                //m_inputGamePad.Update(gameTime);
                //if return enum changed we'll go to the new view
            }
            GameStateEnum temp = returnEnum;
            returnEnum = GameStateEnum.GamePlay;
            return temp;

        }
        public override void reset()
        {
            ruleSystem.Reset();
            animationSystem.Reset();
            moveSystem.Reset();
            undoSystem.Reset();
            m_renderer.Reset();
            killSystem.Reset();
            sinkSystem.Reset();
            winSystem.Reset();

            m_inputKeyboard.resetCommands();
            m_inputKeyboard.registerCommand(Keys.Escape, true, new InputDeviceHelper.CommandDelegate(Escape));
            m_inputKeyboard.registerCommand(controls.Controls[0], true, new InputDeviceHelper.CommandDelegate(moveUp));
            m_inputKeyboard.registerCommand(controls.Controls[1], true, new InputDeviceHelper.CommandDelegate(moveDown));
            m_inputKeyboard.registerCommand(controls.Controls[2], true, new InputDeviceHelper.CommandDelegate(moveLeft));
            m_inputKeyboard.registerCommand(controls.Controls[3], true, new InputDeviceHelper.CommandDelegate(moveRight));
            m_inputKeyboard.registerCommand(controls.Controls[4], true, new InputDeviceHelper.CommandDelegate(ResetKeyPress));
            m_inputKeyboard.registerCommand(controls.Controls[5], true, new InputDeviceHelper.CommandDelegate(Undo));

            transforms = gridMaker.MakeGrid(level[0]);
            ruleSystem.UpdateRules();
            state = State.Play;
        }
        public override void update(GameTime gameTime)
        {
            animationSystem.Update(gameTime);
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);

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
        private void moveUp(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Up");
                checkSystems();
                ruleSystem.UpdateRules();
            }
        }
        private void moveDown(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Down");
                checkSystems();
                ruleSystem.UpdateRules();
            }
        }
        private void moveLeft(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Left");
                checkSystems();
                ruleSystem.UpdateRules();
            }
        }
        private void moveRight(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Right");
                checkSystems();
                ruleSystem.UpdateRules();
            }
        }
        private void Undo(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.UndoKeyPress(transforms);
            }
        }
        private void ResetKeyPress(GameTime gameTime, float scale)
        {
            reset();
        }

        private void checkSystems()
        {
            killSystem.Check();
            sinkSystem.Check();
            if (winSystem.Win())
            {
                state = State.StartParticle;
            }
        }
        #endregion
    }
}
