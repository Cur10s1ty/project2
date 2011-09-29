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
        public LinkedList<Unit> baddies;
        /// <summary>
        /// De demon =D
        /// </summary>
        public Demon player;

        // Viewport + Background
        public AkumaViewport viewport;
        public Vector2 offset;

        // Test Player
        public TestPlayer testPlayer;
        public LinkedList<Obstacle> obstacles;

        public Random random;

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
            //Console.Out.WriteLine(graphics.PreferredBackBufferFormat);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            // Set default orientation to landscape
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;

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
            AkumaContentManager.LoadContent();

            // TODO: Add your initialization logic here
            baddies = new LinkedList<Unit>();
            player = new Demon(370, 380);
            obstacles = new LinkedList<Obstacle>();

            // Test Player
            //testPlayer = new TestPlayer(370, 380);
            viewport = new AkumaViewport();

            random = new Random();

            obstacles.AddLast(new Platform(360, 240, AkumaContentManager.testPlatfromTex, false));
            baddies.AddLast(new TestEnemy(100, 380));
            baddies.AddLast(new TestEnemy(200, 380));
            baddies.AddLast(new TestEnemy(150, 380));

            baddies.AddLast(new TestEnemy(50, 380));
            baddies.AddLast(new TestEnemy(25, 380));
            baddies.AddLast(new TestEnemy(225, 380));

            controller = new Controller();
            
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
            viewport = new AkumaViewport();
            obstacles.AddLast(new Platform(360, 240, AkumaContentManager.forestPlatformTrunkTex, false));
            obstacles.AddLast(new Platform(480, 140, AkumaContentManager.forestPlatformLeaf1Tex, false));
            obstacles.AddLast(new Platform(1200, 300, AkumaContentManager.forestPlatformLeaf2Tex, false));
            obstacles.AddLast(new Platform(20, 400, AkumaContentManager.forestPlatformLeaf3Tex, false));
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

            // TODO: Add your update logic here
            viewport.Update(gameTime);

            controller.Update(gameTime);
            player.Update(gameTime);

            foreach (Unit baddie in baddies)
            {
                baddie.Update(gameTime);
            }

            // Test Player
            //testPlayer.Update(gameTime);

            foreach (Obstacle o in obstacles)
            {
                o.Update(gameTime);
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

            viewport.Draw(gameTime, spriteBatch);

            foreach (Unit unit in baddies)
            {
                unit.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);
            // Test Player
            //testPlayer.Draw(gameTime, spriteBatch);

            foreach (Obstacle o in obstacles)
            {
                o.Draw(gameTime, spriteBatch);
            }

            controller.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds a unit to the list of units
        /// </summary>
        /// <param name="unitToAdd">The unit to add to the baddieslist</param>
        public void AddUnit(Unit unitToAdd)
        {
            this.baddies.AddLast(unitToAdd);
        }

        /// <summary>
        /// Removes the unit from the list of units
        /// </summary>
        /// <param name="unitToRemove">The baddie that needs to be removed</param>
        public void RemoveUnit(Unit unitToRemove)
        {
            if(baddies.Contains(unitToRemove)) 
            {
                baddies.Remove(unitToRemove);
            }
        }
    }
}
