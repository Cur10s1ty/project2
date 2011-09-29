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
            calculateJump();
            jumpCount += 0.1;
        }

        public override void Attack()
        {
            //throw new NotImplementedException();
        }

        public override void Die()
        {
            game.RemoveUnit(this);
        }
    }
}
