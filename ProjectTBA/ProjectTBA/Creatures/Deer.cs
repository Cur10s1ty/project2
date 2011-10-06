using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;
using WindowsPhoneParticleEngine;
using WindowsPhoneParticleEngine.Emitters;
using ProjectTBA.PowerUps;

namespace ProjectTBA.Creatures
{
    public class Deer : Creature
    {

        public Texture2D walkTex { get; set; }
        public Texture2D jumpTex { get; set; }
        public Random random { get; set; }
        public int textureWidth = 120;
        public int textureHeight = 100;
        public Vector2 speed;
        public SpriteEffects effects = SpriteEffects.None;
        public int directionCooldown = 100;
        public int directionChanged = 0;

        public Boolean jumping = false;
        public double jumpCount = 0;
        public double walkCount = 0;

        public int life = 80;

        public Boolean onFire = false;
        public ParticleEmitter fireEmitter = null;

        public Deer(Vector2 location)
            : base(location)
        {
            this.walkTex = AkumaContentManager.deerWalkTex;
            this.jumpTex = AkumaContentManager.deerJumpTex;
            this.random = Game1.GetInstance().currentLevel.random;
            this.speed = new Vector2(-2f, 0f);
        }

        public override void Update(GameTime gt)
        {
            if (!jumping && random.Next(100) < 5)
            {
                jumping = true;
            }

            if (random.Next(100) > 95 && directionChanged == 0)
            {
                ChangeDirection();
            }

            if (jumping)
            {
                jumpCount += 0.25;

                if (jumpCount >= 4)
                {
                    jumpCount = 0;
                    jumping = false;
                }
            }
            else
            {
                walkCount += 0.25;

                if (walkCount >= 3)
                {
                    walkCount = 0;
                }
            }

            location += speed;

            if ((location.X <= 5 && effects == SpriteEffects.None) || (location.X + textureWidth >= 1595 && effects == SpriteEffects.FlipHorizontally))
            {
                ChangeDirection();
            }

            if (directionChanged > 0)
            {
                directionChanged--;
            }

            if (onFire && fireEmitter == null)
            {
                LinkedList<Texture2D> textures = new LinkedList<Texture2D>();
                textures.AddLast(AkumaContentManager.fireball2Tex);
                fireEmitter = ParticleEmitterManager.GetInstance().AddEmitter(ParticleEmitterManager.EmitterType.Point, textures, new Vector3(location.X + 60, location.Y + 50, 0.12f), new Vector3(0f, 0f, 0f), 3, 2f, 0.2f, Color.White);
            }

            if (fireEmitter != null)
            {
                fireEmitter.location.X = location.X + 60;
                fireEmitter.location.Y = location.Y + 50;
                life--;
            }

            if (life <= 0)
            {
                fireEmitter.Dispose();
                Game1.GetInstance().currentLevel.creaturesToRemove.AddLast(this);
                Game1.GetInstance().currentLevel.AddTombstone(this);

                if (random.Next(500) < 2)
                {
                    Game1.GetInstance().currentLevel.powerUps.AddLast(new BigFireball(new Vector2(this.location.X + 50, 330)));
                }
            }
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            if (jumping)
            {
                sb.Draw(jumpTex, GetDrawLocation(), GetSourceRectangle((int)Math.Floor(jumpCount)), Color.White, 0f, Vector2.Zero, 1f, effects, 0.12f);
            }
            else
            {
                sb.Draw(walkTex, GetDrawLocation(), GetSourceRectangle((int)Math.Floor(walkCount)), Color.White, 0f, Vector2.Zero, 1f, effects, 0.12f);
            }
        }

        public Vector2 GetDrawLocation()
        {
            return new Vector2(location.X - Game1.GetInstance().currentLevel.offset.X, location.Y);
        }

        public Rectangle GetSourceRectangle(int frame)
        {
            return new Rectangle((textureWidth * frame), 0, textureWidth, 100);
        }

        public void ChangeDirection()
        {
            directionChanged = 100;

            switch (effects)
            {
                case SpriteEffects.None:
                    effects = SpriteEffects.FlipHorizontally;
                    speed.X *= -1;
                    break;

                case SpriteEffects.FlipHorizontally:
                    effects = SpriteEffects.None;
                    speed.X *= -1;
                    break;
            }
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().currentLevel.offset.X,
                (int)location.Y, textureWidth, textureHeight);
        }
    }
}
