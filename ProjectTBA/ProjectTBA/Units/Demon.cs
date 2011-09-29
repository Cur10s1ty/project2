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

namespace ProjectTBA.Units
{
    public class Demon : Unit
    {
        public Boolean isJumping = false;
        public Boolean isAttacking = false;
        public Boolean isWalking = false;

        public Vector2 speed;

        public SpriteEffects spriteEffect = SpriteEffects.None;
        public int textureWidth = 86;
        public double walkFrames = 0;

        public int maxJumpHeight;

        /// <summary>
        /// The player =D
        /// </summary>
        public Demon(float x, float y)
            : base(x,y)
        {
            this.texture = AkumaContentManager.playerTex;
            this.jumping = false;
            this.falling = false;
            this.maxJumpHeight = 40;
            this.stopJumpOn = (int)y;
            this.floorHeight = 380;
            this.speed = new Vector2(0, 0);
        }

        public override void Attack()
        {
        }

        public override void Die()
        {
        }

        private void PerformSpecialAttack()
        {
        }

        public override void Update(GameTime gt)
        {
            SetCollisionHeight();

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

                if (game.offset.X + movementSpeed < 800)
                {
                    if (location.X + game.offset.X + textureWidth > 600)
                    {
                        game.offset.X += movementSpeed;
                    }
                }
                else
                {
                    game.offset.X = 800;
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

                if (game.offset.X - movementSpeed > 0)
                {
                    if (location.X - game.offset.X < 200)
                    {
                        game.offset.X -= movementSpeed;
                    }
                }
                else
                {
                    game.offset.X = 0;
                }
            }

            if (jumping && !falling)
            {
                CalculateJump();
                jumpCount += 0.1;
            }

            if (falling && !jumping)
            {
                CalculateFall();
                fallCount += 1;
            }

            ApplySpeed();
            speed = new Vector2(0, 0);

            if (!jumping && !falling)
            {
                if (ControllerState.IsButtonPressed(ControllerState.Buttons.A))
                {
                    jumping = true;
                    jumpUp = true;
                }
            }
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            if (!jumping && !isWalking)
            {
                sb.Draw(texture, GetDrawLocation(), GetSourceRectangle(0), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
            }
            else if (!jumping && isWalking)
            {
                if (walkFrames > 2)
                {
                    walkFrames = 0;
                }

                sb.Draw(texture, GetDrawLocation(), GetSourceRectangle(1 + (int)Math.Floor(walkFrames)), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
                walkFrames += 0.2;
            }
            else if (jumping)
            {
                sb.Draw(texture, GetDrawLocation(), GetSourceRectangle(3), Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0.1f);
            }
        }

        public Vector2 GetDrawLocation()
        {
            Vector2 loc = new Vector2();
            loc.X = location.X - game.offset.X;
            loc.Y = location.Y - game.offset.Y;
            return loc;
        }

        public Rectangle GetSourceRectangle(int frame)
        {
            return new Rectangle((86 * frame), 0, 86, 100);
        }

        public void CalculateJump()
        {
            //y = 0.15x^2 - 1.5x + 5.5
            int value = (int)(Math.Pow((0.15 * jumpCount), 2) - (1.5 * jumpCount) + 5.5);

            if (value == 0)
            {
                jumpUp = false;
            }

            if (location.Y - value > stopJumpOn)
            {
                speed.Y = stopJumpOn - location.Y;
                jumping = false;
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
        }
    }
}
