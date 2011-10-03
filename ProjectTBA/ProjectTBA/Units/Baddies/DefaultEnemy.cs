using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Units.Baddies
{
    public class DefaultEnemy : Unit
    {
        private Boolean idle = true;
        private Boolean moving = false;
        private Boolean moveLeft = true;
        private Boolean attacking = false;
        private int lastTotalGameTime = 0;

        public DefaultEnemy(float x, float y, Texture2D texture)
            : base(x, y)
        {
            game.AddUnit(this);
            this.texture = texture;

            this.stopJumpOn = (int)y;
            this.floorHeight = (int)y;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GetRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        public override void Update(GameTime gameTime)
        {
            SetCollisionHeight();

            if (attacking)
            {
                Attack();
            }

            if (jumping)
            {
                Jump();
            }

            if (falling)
            {
                Fall();
            }

            if (moving)
            {
                Move();
            }

            if (gameTime.TotalGameTime.TotalMilliseconds - lastTotalGameTime < 200)
            {
                return;
            }
            else
            {
                lastTotalGameTime = (int)gameTime.TotalGameTime.TotalMilliseconds;
            }

            int chance = game.random.Next(10);

            if (this.GetRectangle().Intersects(game.player.GetRectangle()) && !jumping)
            {
                attacking = true;
            }

            if (idle)
            {
                if (chance < 2)
                {
                    idle = false;
                    moving = true;
                }
            }
            else if (moving)
            {
                if (chance < 2)
                {
                    idle = true;
                    moving = false;
                }
                else if (chance < 4 && !jumping)
                {
                    moveLeft = !moveLeft;
                }
            }

            if (!jumping)
            {
                if (chance < 2 && game.player.location.Y < this.location.Y)
                {
                    jumping = true;
                }
            }
        }

        private void Move()
        {
            if (moveLeft)
            {
                if (this.location.X < 10)
                {
                    moveLeft = false;
                    return;
                }
                else
                {
                    this.location.X -= movementSpeed;
                }
            }
            else
            {
                if (this.location.X + this.texture.Width > 780)
                {
                    moveLeft = true;
                    return;
                }
                else
                {
                    this.location.X += movementSpeed;
                }
            }
        }

        private void Jump()
        {
            CalculateJump();
            jumpCount += 0.1;
        }

        protected void CalculateJump()
        {
            //y = 0.15x^2 - 1.5x + 5.5
            int value = (int)(Math.Pow((0.15 * jumpCount), 2) - (1.5 * jumpCount) + 5.5);

            if (location.Y - value > stopJumpOn)
            {
                location.Y = stopJumpOn;
                jumping = false;
                jumpCount = 0.0;
            }
            else
            {
                location.Y -= value;
            }
        }

        private void Fall()
        {
            CalculateFall();
            fallCount++;
        }

        public void CalculateFall()
        {
            //y = 2^(0.25x)
            int value = (int)Math.Pow(2, ((0.25 * fallCount)));

            if (location.Y + value > stopJumpOn)
            {
                location.Y = stopJumpOn;
                falling = false;
                fallCount = 0.0;
            }
            else
            {
                location.Y += value;
            }
        }

        public override void Attack()
        {
            //throw new NotImplementedException();
        }

        public override void Die()
        {
            game.RemoveUnit(this);
        }

        public override Rectangle GetFeetHitbox()
        {
            return new Rectangle((int)location.X - (int)game.offset.X, (int)location.Y + 99, 25, 1);
        }
    }
}
