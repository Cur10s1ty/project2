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

        public int maxJumpHeight;

        public Boolean falling;

        /// <summary>
        /// The player =D
        /// </summary>
        public Demon(float x, float y)
            : base(x,y)
        {
            this.texture = AkumaContentManager.playerTex;
            this.jumping = false;
            this.maxJumpHeight = 40;
            this.stopJumpOn = (int)y;
            this.floorHeight = (int)y;
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
                if (location.X + texture.Width < 1600)
                {
                    location.X += movementSpeed;
                }

                if (location.X - Game1.GetInstance().offset.X > 600 && Game1.GetInstance().offset.X < 800)
                {
                    Game1.GetInstance().offset.X += movementSpeed;
                }
            }
            if (ControllerState.IsButtonPressed(ControllerState.Buttons.LEFT))
            {
                if (location.X - movementSpeed > 0)
                {
                    location.X -= movementSpeed;
                }
                else
                {
                    location.X = 0;
                }

                if (Game1.GetInstance().offset.X - movementSpeed > 0)
                {
                    if (location.X - Game1.GetInstance().offset.X < 200)
                    {
                        Game1.GetInstance().offset.X -= movementSpeed;
                    }
                }
                else
                {
                    Game1.GetInstance().offset.X = 0;
                }
            }

            if (jumping)
            {
                calculateJump();
                jumpCount += 0.1;
            }

            if (!jumping)
            {
                if (ControllerState.IsButtonPressed(ControllerState.Buttons.A))
                {
                    jumping = true;
                }
            }
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        private void AlignOffset()
        {
            
        }
    }
}
