using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectTBA.Units;
using ProjectTBA.Obstacles;
using ProjectTBA.Views;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;
using Projectiles;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Units.Baddies;
using ProjectTBA.Creatures;
using ProjectTBA.PowerUps;

namespace ProjectTBA.Levels
{
    public class Level
    {
        private Demon player;
        public LinkedList<Unit> baddies;
        public LinkedList<Unit> baddiesToRemove;
        public LinkedList<Creature> creatures;
        public LinkedList<Creature> creaturesToRemove;

        public LinkedList<Obstacle> obstacles;
        public LinkedList<Projectile> projectiles;
        private LinkedList<Projectile> deadProjectiles;
        private LinkedList<Tombstone> tombstones;

        public LinkedList<PowerUp> powerUps;
        public LinkedList<PowerUp> powerUpsToRemove;

        // Viewport + Background
        public AkumaViewport viewport;
        public Vector2 offset;
        public Random random;

        public int level;
        public int levelWidth = 1600;
        public int levelHeight = 400;

        public Level(Demon player, int level)
        {
            random = new Random();

            this.player = player;
            this.level = level;

            baddies = new LinkedList<Unit>();
            baddiesToRemove = new LinkedList<Unit>();
            creatures = new LinkedList<Creature>();
            creaturesToRemove = new LinkedList<Creature>();

            obstacles = new LinkedList<Obstacle>();
            projectiles = new LinkedList<Projectile>();
            deadProjectiles = new LinkedList<Projectile>();
            tombstones = new LinkedList<Tombstone>();

            powerUps = new LinkedList<PowerUp>();
            powerUpsToRemove = new LinkedList<PowerUp>();
        }

        public void GenerateLevel(int level)
        {
            switch (level)
            {
                case 1:
                    viewport = new AkumaViewport(this);
                    obstacles.AddLast(new Wall(300, 300, AkumaContentManager.testPlayerTex));
                    baddies.AddLast(new PeasantEnemy(200, 300));
                    break;

                case 2:
                    viewport = new AkumaViewport(this);
                    break;

                case 3:
                    viewport = new AkumaViewport(this);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {

            viewport.Update(gameTime);

            foreach (Unit baddie in baddies)
            {
                baddie.Update(gameTime);
            }

            if (baddiesToRemove.Count > 0)
            {
                foreach (Unit baddie in baddiesToRemove)
                {
                    baddies.Remove(baddie);
                }
            }

            foreach (Creature c in creatures)
            {
                c.Update(gameTime);
            }

            if (creaturesToRemove.Count > 0)
            {
                foreach (Creature c in creaturesToRemove)
                {
                    creatures.Remove(c);
                }

                creaturesToRemove.Clear();
            }


            player.Update(gameTime);

            foreach (Projectile projectile in projectiles)
            {
                if (!projectile.isDead)
                {
                    projectile.Update(gameTime);
                }
                else
                {
                    deadProjectiles.AddLast(projectile);
                }
            }

            if (deadProjectiles.Count > 0)
            {
                foreach (Projectile deadProjectile in deadProjectiles)
                {
                    projectiles.Remove(deadProjectile);
                }
                deadProjectiles.Clear();
            }

            foreach (Obstacle o in obstacles)
            {
                o.Update(gameTime);
            }

            foreach (PowerUp pu in powerUps)
            {
                pu.Update(gameTime);
            }

            if (powerUpsToRemove.Count > 0)
            {
                foreach (PowerUp pu in powerUpsToRemove)
                {
                    powerUps.Remove(pu);
                }

                powerUpsToRemove.Clear();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            viewport.Draw(gameTime, spriteBatch);

            foreach (Unit unit in baddies)
            {
                unit.Draw(gameTime, spriteBatch);
            }

            foreach (Creature c in creatures)
            {
                c.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            foreach (Obstacle o in obstacles)
            {
                o.Draw(gameTime, spriteBatch);
            }

            foreach (Projectile projectile in projectiles)
            {
                if (!projectile.isDead)
                {
                    projectile.Draw(spriteBatch);
                }
            }

            foreach (PowerUp pu in powerUps)
            {
                pu.Draw(gameTime, spriteBatch);
            }

            foreach (Tombstone tombstone in tombstones)
            {
                tombstone.Draw(spriteBatch);
            }
        }

        public void AddTombstone(Unit enemy)
        {
            this.tombstones.AddLast(new Tombstone(enemy));
        }

        public void AddTombstone(Creature creature)
        {
            this.tombstones.AddLast(new Tombstone(creature));
        }
    }
}
