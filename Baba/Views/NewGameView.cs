﻿using Baba.GameComponents;
using Baba.GameComponents.ConcreteComponents;
using Baba.GameComponents.Systems;
using Baba.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Baba.Views.SavingControls;
using System.Diagnostics;

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
        public RuleSystem ruleSystem;
        private MoveSystem moveSystem;
        private UndoSystem undoSystem;
        private KillSystem killSystem;
        private SinkSystem sinkSystem;
        private WinSystem winSystem;
        private AudioSystem audioSystem;
        private ParticleSystem particleSystem;
        

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
            
            audioSystem = new(this);

            undoSystem.OnUndo += ruleSystem.UpdateRules;
            undoSystem.OnUndo += animationSystem.UpdateAnimations;
            undoSystem.OnUndo += m_renderer.UpdateSprites;

            ruleSystem.onTransformationsFinished += animationSystem.UpdateAnimations;
            ruleSystem.onTransformationsFinished += m_renderer.UpdateSprites;


            loadTextures(contentManager);
            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.Escape, true, new InputDeviceHelper.CommandDelegate(Escape));
            
            m_inputKeyboard.registerCommand(controls.Controls[0], false, new InputDeviceHelper.CommandDelegate(moveUp));
            m_inputKeyboard.registerCommand(controls.Controls[1], false, new InputDeviceHelper.CommandDelegate(moveDown));
            m_inputKeyboard.registerCommand(controls.Controls[2], false, new InputDeviceHelper.CommandDelegate(moveLeft));
            m_inputKeyboard.registerCommand(controls.Controls[3], false, new InputDeviceHelper.CommandDelegate(moveRight));
            m_inputKeyboard.registerCommand(controls.Controls[4], false, new InputDeviceHelper.CommandDelegate(ResetKeyPress));
            m_inputKeyboard.registerCommand(controls.Controls[5], false, new InputDeviceHelper.CommandDelegate(Undo));
            
            particleSystem = new ParticleSystem(this, m_graphics.GraphicsDevice);
            particleSystem.winChangedAudio += audioSystem.PlayChange;
            particleSystem.onFireworks += audioSystem.PlayFirework;
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (state == State.StartParticle)
            {
                audioSystem.PlayVictory();
                // start particle effects for win
                particleSystem.WinLevel();
                state = State.Win;
            }
            else if (state == State.Play || state == State.Win)
            {
                m_inputKeyboard.Update(gameTime);
                //m_inputGamePad.Update(gameTime);
                //if return enum changed we'll go to the new view
            }
            if (returnEnum != GameStateEnum.GamePlay)
            {
                audioSystem.StopBGM();
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
            audioSystem.StartBGM(level[0]);
            particleSystem.Reset();
            transforms = gridMaker.MakeGrid(level[0]);
            
            ruleSystem.UpdateRules();
           
            
            state = State.Play;
        }
        public void resetControls()
        {
            m_inputKeyboard.resetCommands();
            m_inputKeyboard.registerCommand(Keys.Escape, true, new InputDeviceHelper.CommandDelegate(Escape));
            m_inputKeyboard.registerCommand(controls.Controls[0], false, new InputDeviceHelper.CommandDelegate(moveUp));
            m_inputKeyboard.registerCommand(controls.Controls[1], false, new InputDeviceHelper.CommandDelegate(moveDown));
            m_inputKeyboard.registerCommand(controls.Controls[2], false, new InputDeviceHelper.CommandDelegate(moveLeft));
            m_inputKeyboard.registerCommand(controls.Controls[3], false, new InputDeviceHelper.CommandDelegate(moveRight));
            m_inputKeyboard.registerCommand(controls.Controls[4], false, new InputDeviceHelper.CommandDelegate(ResetKeyPress));
            m_inputKeyboard.registerCommand(controls.Controls[5], false, new InputDeviceHelper.CommandDelegate(Undo));
        }

        public override void update(GameTime gameTime)
        {
            animationSystem.Update(gameTime);
            particleSystem.Update(gameTime);
           
        }

        public override void render(GameTime gameTime)
        {
            base.render(gameTime);

            m_renderer.Render();
            particleSystem.Draw();
        }

        public void KillEntities(params Entity[] entities)
        {
            moveSystem.resetControlled();
            for (int i = 0; i < entities.Length; i++)
            {
                particleSystem.ObjectDestroyed(entities[i].transform.position);
                ruleSystem.ReturnComponents(entities[i].RemoveAll<RuleComponent>());
                entities[i].GetComponent<ItemLabel>().item = ItemType.Empty;
                
            }

            animationSystem.UpdateAnimations();
            m_renderer.UpdateSprites();
            ruleSystem.UpdateRules();
            
            audioSystem.PlayHurt();
        }

        private void loadTextures(ContentManager contentManager)
        {
            m_renderer.LoadWords(contentManager);
            m_renderer.LoadItems(contentManager);
        }
        
        
        #region input handlers
        private void Escape(GameTime gameTime, float scale)
        {
            audioSystem.PlayEscape();
            returnEnum = GameStateEnum.LevelSelect;
        }
        private void moveUp(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Up");
                particleSystem.resetEntities();
                ruleSystem.UpdateRules();
               
                checkSystems();
            }
        }
        private void moveDown(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Down");
                particleSystem.resetEntities();
                ruleSystem.UpdateRules();
                
                checkSystems();
            }
        }
        private void moveLeft(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Left");
                particleSystem.resetEntities();
                ruleSystem.UpdateRules();
                
                checkSystems();
            }
        }
        private void moveRight(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                undoSystem.ArrowKeyPress(transforms);
                moveSystem.moveEntity(gameTime, "Right");
                particleSystem.resetEntities();
                ruleSystem.UpdateRules();
               
                checkSystems();
            }
        }
        private void Undo(GameTime gameTime, float scale)
        {
            if (state == State.Play)
            {
                audioSystem.PlayEscape();
                moveSystem.UndoReset();
                undoSystem.UndoKeyPress(transforms);
            }
        }
        private void ResetKeyPress(GameTime gameTime, float scale)
        {
            audioSystem.StopFireworks();
            audioSystem.StopWin();
            audioSystem.PlayEscape();
            reset();
        }

        private void checkSystems()
        {
            audioSystem.PlayMove();
            killSystem.Check(audioSystem);
            sinkSystem.Check(audioSystem);
            if (winSystem.Win())
            {
                

                state = State.StartParticle;
                particleSystem.setIsWin();
               
            }
        }
        #endregion
    }
}
