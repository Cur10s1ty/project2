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
using WindowsPhoneParticleEngine;

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
        private int unitsToSpawn = 12;

        public Level(Demon player, int level)
        {
            random = new Random();

            this.player = player;
            player.location = new Vector2(0, 291);
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
                    this.levelWidth = 1600;
                    viewport = new AkumaViewport(this);

                    obstacles.AddLast(new Sign(37, 21, AkumaContentManager.signEndTex, true));
                    obstacles.AddLast(new Sign(55, 300, AkumaContentManager.signRightTex));
                    obstacles.AddLast(new Sign(1267, 300, AkumaContentManager.signJumpATex));
                    obstacles.AddLast(new Sign(1483, 300, AkumaContentManager.signLeftTex));

                    obstacles.AddLast(new Platform(46, 106, AkumaContentManager.forestPlatformTrunkTex, false));
                    obstacles.AddLast(new Platform(487, 174, AkumaContentManager.forestPlatformTrunkTex, false));

                    obstacles.AddLast(new Platform(1008, 238, AkumaContentManager.forestPlatformLeaf1Tex, false));
                    obstacles.AddLast(new Platform(1294, 286, AkumaContentManager.forestPlatformLeaf1Tex, false));

                    obstacles.AddLast(new Platform(170, 182, AkumaContentManager.forestPlatformLeaf2Tex, false));
                    obstacles.AddLast(new Platform(674, 194, AkumaContentManager.forestPlatformLeaf2Tex, false));
                    obstacles.AddLast(new Platform(821, 245, AkumaContentManager.forestPlatformLeaf2Tex, false));
                    obstacles.AddLast(new Platform(1233, 101, AkumaContentManager.forestPlatformLeaf2Tex, false));
                    obstacles.AddLast(new Platform(1398, 189, AkumaContentManager.forestPlatformLeaf2Tex, false));

                    obstacles.AddLast(new Platform(311, 121, AkumaContentManager.forestPlatformLeaf3Tex, false));

                    creatures.AddLast(new Deer(new Vector2(500, 300)));
                    creatures.AddLast(new Deer(new Vector2(1300, 300)));

                    baddies.AddLast(new PeasantEnemy(300, 300));
                    break;

                case 2:
                    this.levelWidth = 1600;
                    viewport = new AkumaViewport(this);

                    obstacles.AddLast(new Platform(107, 335, AkumaContentManager.forestPlatformTrunkTex, false));
                    obstacles.AddLast(new Platform(265, 296, AkumaContentManager.forestPlatformTrunkTex, false));
                    obstacles.AddLast(new Platform(912, 337, AkumaContentManager.forestPlatformTrunkTex, false));
                    obstacles.AddLast(new Platform(1041, 294, AkumaContentManager.forestPlatformTrunkTex, false));
                    obstacles.AddLast(new Platform(1279, 342, AkumaContentManager.forestPlatformTrunkTex, false));
                    obstacles.AddLast(new Platform(1503, 178, AkumaContentManager.forestPlatformTrunkTex, false));

                    obstacles.AddLast(new Platform(326, 265, AkumaContentManager.forestPlatformLeaf1Tex, false));
                    obstacles.AddLast(new Platform(451, 207, AkumaContentManager.forestPlatformLeaf1Tex, false));
                    obstacles.AddLast(new Platform(570, 277, AkumaContentManager.forestPlatformLeaf1Tex, false));
                    obstacles.AddLast(new Platform(1371, 268, AkumaContentManager.forestPlatformLeaf1Tex, false));
                    obstacles.AddLast(new Platform(1443, 230, AkumaContentManager.forestPlatformLeaf1Tex, false));

                    obstacles.AddLast(new Wall(341, 298, AkumaContentManager.forestWallTex));
                    obstacles.AddLast(new Wall(578, 298, AkumaContentManager.forestWallTex));
                    obstacles.AddLast(new Wall(992, 298, AkumaContentManager.forestWallTex));

                    creatures.AddLast(new Deer(new Vector2(500, 300)));
                    creatures.AddLast(new Deer(new Vector2(1300, 300)));
                    break;

                case 3:
                    this.levelWidth = 800;
                    viewport = new AkumaViewport(this);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {

            viewport.Update(gameTime);
            if (level == 3)
            {
                if (baddies.Count < 6 && unitsToSpawn > 0)
                {
                    baddies.AddLast(new PeasantEnemy(300, 300));
                    unitsToSpawn--;
                }
                else if (baddies.Count == 0 && unitsToSpawn <= 0)
                {
                    baddies.AddLast(new Samurai(300, 300));
                }
            }

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
