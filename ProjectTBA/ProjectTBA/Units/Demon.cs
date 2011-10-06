using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Controls;
using ProjectTBA.Misc;
using ProjectTBA.Obstacles;
using System.Diagnostics;
using Projectiles;
using WindowsPhoneParticleEngine;
using ProjectTBA.Projectiles;
using ProjectTBA.Units.Bodyparts;

namespace ProjectTBA.Units
{
    public class Demon : Unit
    {

        public Texture2D jumpTex;
        public Texture2D walkTex;

        public Boolean isAttacking = false;
        public Boolean isWalking = false;

        public Vector2 speed;

        public int jumpSpeed = 8;
        public double walkFrames = 0;
        public double attackFrames = 0;

        public int maxJumpHeight;

        private enum HitState
        {
            hitLeft, hitRight
        }

        public Boolean isHit = false;
        private Boolean isInvunerable = false;
        private HitState currentHitState;
        private int hitHeight = 9;
        private int hitTimestamp;
        private int health = 5;
        private int fireballCooldown = 0;
        private Tongue tongue;

        public LinkedList<Projectile> fireballs;
        public LinkedList<Projectile> fireballsToRemove;

        public int textureWidth = 86;
        public int textureHeight = 109;

        /// <summary>
        /// The player =D
        /// </summary>
        public Demon(float x, float y)
            : base(x, y)
        {
            this.jumpTex = AkumaContentManager.demonJumpTex;
            this.walkTex = AkumaContentManager.demonWalkTex;
            this.jumping = false;
            this.falling = false;
            this.maxJumpHeight = 40;
            this.stopJumpOn = (int)y;
            this.floorHeight = (int)y;
            this.speed = new Vector2(0, 0);
            this.fireballs = new LinkedList<Projectile>();
            this.fireballsToRemove = new LinkedList<Projectile>();
            this.tongue = new Tongue(this);
        }

        public override void Attack()
        {
            
            SpawnFireball();
        }

        public override void Die()
        {
        }

        private void PerformSpecialAttack()
        {
            SpawnHugeFireball();
        }

        public override void Update(GameTime gameTime)
        {
            if (fireballCooldown > 0)
            {
                fireballCooldown--;
            }

            SetCollisionHeight();

            foreach (Projectile fireball in fireballs)
            {
                if (fireball.isDead)
                {
                    fireballsToRemove.AddLast(fireball);
                }
                else
                {
                    fireball.Update(gameTime);
                }
            }

            if (fireballsToRemove.Count > 0)
            {
                foreach (Projectile fireballToRemove in fireballsToRemove)
                {
                    this.fireballs.Remove(fireballToRemove);
                }
            }

            if (ControllerState.IsButtonPressed(ControllerState.Buttons.B))
            {
                if (fireballCooldown < 1)
                {
                    Attack();
                    fireballCooldown = 30;
                }
            }

            if (isHit)
            {
                UpdateHit();
                //isHit = false;
                return;
            }

            if (isInvunerable)
            {
                UpdateInvunerableState();
                return;
            }

            if (ControllerState.IsButtonPressed(ControllerState.Buttons.RIGHT))
            {
                if (location.X + textureWidth + movementSpeed < 1600)
                {
                    speed.X = movementSpeed;
                }
                else
                {
                    speed.X = 1600 - textureWidth - location.X;
                }
            }

            if (ControllerState.IsButtonPressed(ControllerState.Buttons.LEFT))
            {
                if (location.X - movementSpeed > 0)
                {
                    speed.X = -movementSpeed;
                }
                else
                {
                    speed.X = -location.X;
                }
            }

            if (game.controller.hasSwiped)
            {
                tongue.Update();
            }

            ApplySpeed();

            if (jumping && !falling)
            {
                CalculateJump();
                jumpCount += 0.15;
            }

            if (falling && !jumping)
            {
                CalculateFall();
                fallCount += 1;
            }

            if (!jumping && !falling)
            {
                if (ControllerState.IsButtonPressed(ControllerState.Buttons.A))
                {
                    jumping = true;
                    jumpUp = true;
                }
            }
        }

        public override void Draw(GameTime gt, SpriteBatch spriteBatch)
        {
            if (!jumping && !isWalking && !isAttacking)
            {
                spriteBatch.Draw(walkTex, GetDrawLocation(), GetSourceRectangle(0), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
            }
            else if (!jumping && isWalking)
            {
                if (walkFrames > 2)
                {
                    walkFrames = 0;
                }

                spriteBatch.Draw(walkTex, GetDrawLocation(), GetSourceRectangle(1 + (int)Math.Floor(walkFrames)), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
                walkFrames += 0.2;
            }
            else if (!jumping && !falling && isAttacking)
            {
                if (attackFrames > 2)
                {
                    isAttacking = false;
                    attackFrames = 0;
                }

                spriteBatch.Draw(texture, GetDrawLocation(), GetSourceRectangle(4 + (int)Math.Floor(attackFrames)), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
                attackFrames += 0.2;
            }
            else if (jumping)
            {
                spriteBatch.Draw(jumpTex, GetDrawLocation(), GetSourceRectangle(1), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
            }

            foreach (Projectile fireball in fireballs)
            {
                fireball.Draw(spriteBatch);
            }

            spriteBatch.Draw(AkumaContentManager.solidTex, GetFeetHitbox(), null, Color.CornflowerBlue, 0f, Vector2.Zero, SpriteEffects.None, 0.05f);
        }

        public Vector2 GetDrawLocation()
        {
            return new Vector2(location.X - game.currentLevel.offset.X, location.Y - game.currentLevel.offset.Y);
        }

        public Rectangle GetSourceRectangle(int frame)
        {
            return new Rectangle((textureWidth * frame), 0, textureWidth, textureHeight);
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().currentLevel.offset.X,
                (int)location.Y, 86, texture.Height);
        }

        public void CalculateJump()
        {
            int value = jumpSpeed - (int)Math.Floor(jumpCount * 2);

            if (value <= 0)
            {
                jumpUp = false;
            }

            if (location.Y - value > stopJumpOn)
            {
                speed.Y = stopJumpOn - location.Y;
                jumping = false;
                jumpSpeed = 8;
                jumpCount = 0.0;
            }
            else
            {
                speed.Y = -value;
            }
        }

        public void CalculateFall()
        {
            //y = 2^(0.25x)
            int value = (int)Math.Pow(2, ((0.25 * fallCount)));

            if (location.Y + value > stopJumpOn)
            {
                speed.Y = stopJumpOn - location.Y;
                falling = false;
                fallCount = 0.0;
            }
            else
            {
                speed.Y = value;
            }
        }

        public void ApplySpeed()
        {
            location += speed;

            if (speed.X > 0)
            {
                spriteEffect = SpriteEffects.None;
            }
            if (speed.X < 0)
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            if ((speed.X > 0 || speed.X < 0) && speed.Y == 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            if (speed.X > 0)
            {
                if (game.currentLevel.offset.X + movementSpeed < 800)
                {
                    if (location.X - game.currentLevel.offset.X + textureWidth > 600f)
                    {
                        game.currentLevel.offset.X += movementSpeed;
                    }
                }
                else
                {
                    game.currentLevel.offset.X = 800;
                }
            }
            else if (speed.X < 0)
            {
                if (game.currentLevel.offset.X - movementSpeed > 0)
                {
                    if (location.X - game.currentLevel.offset.X < 200f)
                    {
                        game.currentLevel.offset.X -= movementSpeed;
                    }
                }
                else
                {
                    game.currentLevel.offset.X = 0;
                }
            }

            speed = new Vector2(0, 0);
        }

        public void Hit(Projectile source)
        {
            if (isHit || isInvunerable)
            {
                return;
            }

            isHit = true;
            jumping = false;
            falling = false;
            fallCount = 0.0;
            jumpCount = 0.0;

            if (source.location.X > this.location.X)
            {
                //hit left
                this.currentHitState = HitState.hitLeft;
            }
            else
            {
                //hit right
                this.currentHitState = HitState.hitRight;
            }
        }

        public void Hit(Unit source)
        {
            if (isHit || isInvunerable)
            {
                return;
            }

            isHit = true;
            jumping = false;
            falling = false;
            fallCount = 0.0;
            jumpCount = 0.0;

            if (source.location.X > this.location.X)
            {
                //hit left
                this.currentHitState = HitState.hitLeft;
            }
            else
            {
                //hit right
                this.currentHitState = HitState.hitRight;
            }
        }

        private void UpdateHit()
        {
            switch (this.currentHitState)
            {
                case HitState.hitLeft:
                    if (this.location.X > 5)
                    {
                        this.location.X -= 5;

                        if (game.currentLevel.offset.X - 5 > 0)
                        {
                            if (location.X - game.currentLevel.offset.X < 200f)
                            {
                                game.currentLevel.offset.X -= 5;
                            }
                        }
                        else
                        {
                            game.currentLevel.offset.X = 0;
                        }
                    }
                    break;
                case HitState.hitRight:
                    if (this.location.X < game.currentLevel.levelWidth)
                    {
                        this.location.X += 5;

                        if (game.currentLevel.offset.X + 5 < 800)
                        {
                            if (location.X - game.currentLevel.offset.X + textureWidth > 600f)
                            {
                                game.currentLevel.offset.X += 5;
                            }
                        }
                        else
                        {
                            game.currentLevel.offset.X = 800;
                        }
                    }
                    break;
                default: break;
            }


            if (this.location.Y - hitHeight - 1 > 380)
            {
                this.location.Y -= 380 - this.location.Y;
                hitHeight = 8;
                isHit = false;
                isInvunerable = true;
                hitTimestamp = (int)game.lastGameTime.TotalGameTime.TotalMilliseconds;
            }
            else
            {
                this.location.Y -= hitHeight;
                hitHeight--;
            }
        }

        private void UpdateInvunerableState()
        {
            if (game.lastGameTime.TotalGameTime.TotalMilliseconds - this.hitTimestamp > 300)
            {
                isInvunerable = false;
            }
        }

        public override Rectangle GetFeetHitbox()
        {
            switch (spriteEffect)
            {
                case SpriteEffects.None:
                    if (!jumping && !isWalking && !isAttacking)
                    {
                        return new Rectangle((int)location.X + 41 - (int)game.currentLevel.offset.X, (int)location.Y + 105, 38, 1);
                    }
                    else if (!jumping && isWalking)
                    {
                        return new Rectangle((int)location.X + 41 - (int)game.currentLevel.offset.X, (int)location.Y + 105, 38, 1);
                    }
                    else if (!jumping && !falling && isAttacking)
                    {
                        return new Rectangle((int)location.X + 32 - (int)game.currentLevel.offset.X, (int)location.Y + 99, 33, 1);
                    }
                    else if (jumping)
                    {
                        return new Rectangle((int)location.X + 34 - (int)game.currentLevel.offset.X, (int)location.Y + 103, 36, 1);
                    }
                    break;

                case SpriteEffects.FlipHorizontally:
                    if (!jumping && !isWalking && !isAttacking)
                    {
                        return new Rectangle((int)location.X + 10 - (int)game.currentLevel.offset.X, (int)location.Y + 105, 38, 1);
                    }
                    else if (!jumping && isWalking)
                    {
                        return new Rectangle((int)location.X + 10 - (int)game.currentLevel.offset.X, (int)location.Y + 105, 38, 1);
                    }
                    else if (!jumping && !falling && isAttacking)
                    {
                        return new Rectangle((int)location.X + 12 - (int)game.currentLevel.offset.X, (int)location.Y + 103, 36, 1);
                    }
                    else if (jumping)
                    {
                        return new Rectangle((int)location.X + 12 - (int)game.currentLevel.offset.X, (int)location.Y + 103, 36, 1);
                    }
                    break;
            }

            return new Rectangle();
        }

        public void SpawnFireball()
        {
            this.fireballs.AddLast(new Fireball(location.X, location.Y + textureHeight / 2, (spriteEffect == SpriteEffects.None) ? false : true, 1f));
        }

        public void SpawnHugeFireball()
        {
            this.fireballs.AddLast(new Fireball(location.X, location.Y + textureHeight / 2, (spriteEffect == SpriteEffects.None) ? false : true, 4f));
            this.Hit(fireballs.ElementAt(fireballs.Count - 1));
        }

        public override void SetCollisionHeight()
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
                                    stopJumpOn = p.GetTopHitbox().Y - textureHeight;
                                }
                                else if (!jumping)
                                {
                                    stopJumpOn = p.GetTopHitbox().Y - textureHeight;
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
    }
}
