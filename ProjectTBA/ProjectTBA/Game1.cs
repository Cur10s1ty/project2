using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using ProjectTBA.Misc;
using ProjectTBA.Controls;
using ProjectTBA.Units;
using ProjectTBA.Tests;
using ProjectTBA.Obstacles;
using ProjectTBA.Views;
using ProjectTBA.Units.Baddies;
using Projectiles;
using WindowsPhoneParticleEngine;
using ProjectTBA.Levels;
using ProjectTBA.Creatures;
using ProjectTBA.PowerUps;
using ProjectTBA.Menus;

namespace ProjectTBA
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public Controller controller;
        /// <summary>
        /// De demon =D
        /// </summary>
        public Demon player;

        // Test Player
        public TestPlayer testPlayer;
        public GameTime lastGameTime;

        public int screenWidth = 800;
        public int screenHeight = 480;

        // Particles
        public ParticleEmitterManager particleEmitterManager;
        public ParticleEmitter testEmitter;

        private LinkedList<Level> levelList;
        public Level currentLevel;

        public MainMenu mainMenu;

        public enum GameState
        {
            MainMenu,
            Ingame
        }

        public GameState gameState;

        private static Game1 instance;
        public static Game1 GetInstance()
        {
            if (instance == null)
            {
                instance = new Game1();
            }
            return instance;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            // Set default orientation to landscape
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            instance = this;

            TouchPanel.EnabledGestures = GestureType.HorizontalDrag;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = GameState.MainMenu;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            AkumaContentManager.LoadContent();
            particleEmitterManager = ParticleEmitterManager.GetInstance();
            mainMenu = new MainMenu();
        }

        public void LoadIngameContent()
        {
            player = new Demon(0, 291);
            controller = new Controller();

            currentLevel = new Level(player, 1);
            currentLevel.GenerateLevel(currentLevel.level);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            this.lastGameTime = gameTime;

            switch (gameState)
            {
                case GameState.MainMenu:
                    mainMenu.Update(gameTime);
                    break;

                case GameState.Ingame:
                    controller.Update(gameTime);
                    currentLevel.Update(gameTime);

                    particleEmitterManager.Update(gameTime, currentLevel.offset);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, null);

            switch (gameState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(gameTime, spriteBatch);
                    break;

                case GameState.Ingame:
                    currentLevel.Draw(gameTime, spriteBatch);

                    controller.Draw(gameTime, spriteBatch);

                    particleEmitterManager.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds a unit to the list of units
        /// </summary>
        /// <param name="unitToAdd">The unit to add to the baddieslist</param>
        public void AddUnit(Unit unitToAdd)
        {
            currentLevel.baddies.AddLast(unitToAdd);
        }

        /// <summary>
        /// Removes the unit from the list of units
        /// </summary>
        /// <param name="unitToRemove">The baddie that needs to be removed</param>
        public void RemoveUnit(Unit unitToRemove)
        {
            if (currentLevel.baddies.Contains(unitToRemove))
            {
                currentLevel.baddies.Remove(unitToRemove);
            }
        }
    }
}
