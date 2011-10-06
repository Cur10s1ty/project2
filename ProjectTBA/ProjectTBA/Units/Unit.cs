using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Obstacles;
using System.Diagnostics;
using Projectiles;

namespace ProjectTBA.Units
{
    public abstract class Unit
    {
        public Game1 game;

        public Vector2 location;
        public float movementSpeed;
        protected double jumpCount = 0;
        protected double fallCount = 0;
        public Boolean jumping;
        protected Boolean jumpUp;
        public Boolean falling;
        protected Boolean isDead = false;
        protected int health = 50;

        public int stopJumpOn;
        protected int floorHeight;

        public Texture2D texture { get; set; }

        public SpriteEffects spriteEffect = SpriteEffects.None;

        public Unit(float x, float y)
        {
            this.game = Game1.GetInstance();

            this.location = new Vector2(x, y);
            this.movementSpeed = 4f;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void Attack();

        public abstract void Die();

        public virtual void Hit(Object from)
        {
            if (from is Projectile && !isDead)
            {
                this.health -= ((Projectile)from).damage;
                if (health < 0)
                {
                    Die();
                }
            }
        }

        public abstract Rectangle GetFeetHitbox();

        public virtual void SetCollisionHeight()
        {
            Boolean abovePlatform = false;

            if (!jumpUp)
            {
                foreach (Obstacle o in game.currentLevel.obstacles)
                {
                    if (o is Platform)
                    {
                        Platform p = (Platform)o;

                        if (GetFeetHitbox().X + GetFeetHitbox().Width > p.GetTopHitbox().X && GetFeetHitbox().X < p.GetTopHitbox().X + p.GetTopHitbox().Width)
                        {
                            if (GetFeetHitbox().Y <= p.GetTopHitbox().Y)
                            {
                                abovePlatform = true;

                                if (jumping && !falling)
                                {
                                    stopJumpOn = p.GetTopHitbox().Y - texture.Height;
                                }
                                else if (!jumping)
                                {
                                    stopJumpOn = p.GetTopHitbox().Y - texture.Height;
                                    falling = true;
                                }
                            }
                        }
                    }
                }
            }

            if (!jumping && !abovePlatform && location.Y != floorHeight)
            {
                stopJumpOn = floorHeight;
                falling = true;
            }
        }

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().currentLevel.offset.X,
                (int)location.Y, texture.Width, texture.Height);
        }
    }
}
