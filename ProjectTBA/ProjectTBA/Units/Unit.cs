using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Obstacles;

namespace ProjectTBA.Units
{
    public abstract class Unit
    {
        public Game1 game;

        public Vector2 location;
        protected float movementSpeed;
        protected double jumpCount = 0;
        protected Boolean jumping;

        protected int stopJumpOn;
        protected int floorHeight;

        public Texture2D texture { get; set; }

        public Unit(float x, float y)
        {
            this.game = Game1.GetInstance();

            this.location = new Vector2(x, y);
            this.movementSpeed = 4f;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().offset.X, 
                (int)location.Y, texture.Width, texture.Height);
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void Attack();

        public abstract void Die();

        protected void calculateJump()
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


        public Rectangle GetFeetHitbox()
        {
            return new Rectangle((int)location.X + 37, (int)location.Y + 84, 48, 1);
        }

        protected void SetCollisionHeight()
        {
            foreach (Obstacle o in Game1.GetInstance().obstacles)
            {
                if (o is Platform)
                {
                    switch (((Platform)o).GetPositionRelativeToUnit(this))
                    {
                        // Directly above
                        case 1:
                            stopJumpOn = o.bounds.Y - texture.Height;
                            break;

                        // Directly below
                        case 2:
                            break;

                        // Same X and Y
                        case 3:
                            break;

                        // Left above
                        case 4:
                            if (!jumping)
                            {
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Left below
                        case 5:
                            if (!jumping)
                            {
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Left, same Y
                        case 6:
                            if (!jumping)
                            {
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Right above
                        case 7:
                            if (!jumping)
                            {
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Right below
                        case 8:
                            if (!jumping)
                            {
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Right, same Y
                        case 9:
                            if (!jumping)
                            {
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Off screen
                        case 0:
                            break;
                    }
                }
            }
        }
    }
}
