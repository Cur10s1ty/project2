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

        public static TimeSpan TIMESTEP = TimeSpan.FromTicks(333333);

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

            player = new Demon(357, 380);
            controller = new Controller();

            currentLevel = new Level(player);

            currentLevel.obstacles.AddLast(new Platform(600, 400, AkumaContentManager.forestPlatformTrunkTex, false));
            currentLevel.obstacles.AddLast(new Platform(480, 300, AkumaContentManager.forestPlatformLeaf1Tex, false));

            currentLevel.baddies.AddLast(new TestEnemy(100, 380));
            //baddies.AddLast(new TestEnemy(200, 380));
            //baddies.AddLast(new TestEnemy(150, 380));

            //baddies.AddLast(new TestEnemy(50, 380));
            //baddies.AddLast(new TestEnemy(25, 380));
            //baddies.AddLast(new TestEnemy(225, 380));

            //LinkedList<Texture2D> textures = new LinkedList<Texture2D>();
            //textures.AddLast(AkumaContentManager.circleParticle);
            //this.testEmitter = particleEmitterManager.AddEmitter(ParticleEmitterManager.EmitterType.Point, textures, new Vector3(400f, 240f, 0.002f), new Vector3(0, 0, 0), 1, 2f, Color.Red);
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

            controller.Update(gameTime);
            currentLevel.Update(gameTime);

            particleEmitterManager.Update(gameTime, currentLevel.offset);

            particleEmitterManager.Update(gameTime, offset);

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

            currentLevel.Draw(gameTime, spriteBatch);
            
            controller.Draw(gameTime, spriteBatch);

            particleEmitterManager.Draw(gameTime, spriteBatch);

            particleEmitterManager.Draw(gameTime, spriteBatch);

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
