using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Misc;
using ProjectTBA.Controls;
using ProjectTBA.Obstacles;
using ProjectTBA.Views;
using System.Diagnostics;

namespace ProjectTBA.Tests
{
    public class TestPlayer
    {

        public Texture2D texture { get; set; }
        public Vector2 location;
        public float movementSpeed;

        public Boolean jumping;
        public Boolean jumpUp;
        public int maxJumpHeight;
        public int stopJumpOn;
        public int floorHeight;

        public Boolean falling;

        public TestPlayer(float x, float y)
        {
            this.texture = TBAContentManager.testPlayerTex;
            this.location = new Vector2(x, y);
            this.movementSpeed = 8f;

            this.jumping = false;
            this.jumpUp = false;
            this.maxJumpHeight = 40;
            this.stopJumpOn = (int)y;
            this.floorHeight = (int)y;
        }

        public void Update(GameTime gt)
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
                if (jumpUp)
                {
                    if (location.Y - movementSpeed < maxJumpHeight)
                    {
                        location.Y = maxJumpHeight;
                        jumpUp = false;
                    }
                    else
                    {
                        location.Y -= movementSpeed;
                    }
                }
                else
                {
                    if (location.Y + movementSpeed > stopJumpOn)
                    {
                        location.Y = stopJumpOn;
                        stopJumpOn = floorHeight;
                        jumping = false;
                    }
                    else
                    {
                        location.Y += movementSpeed;
                    }
                }
            }
            else if (falling)
            {
                if (location.Y + movementSpeed > stopJumpOn)
                {
                    location.Y = stopJumpOn;
                    stopJumpOn = floorHeight;
                    falling = false;
                }
                else
                {
                    location.Y += movementSpeed;
                }
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

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().offset.X, (int)location.Y, texture.Width, texture.Height);
        }

        private void SetCollisionHeight()
        {
            foreach (Obstacle o in Game1.GetInstance().obstacles)
            {
                if (o is Platform)
                {
                    switch (((Platform)o).GetPositionRelativeToPlayer())
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
                                falling = true;
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Left below
                        case 5:
                            if (!jumping)
                            {
                                falling = true;
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Left, same Y
                        case 6:
                            if (!jumping)
                            {
                                falling = true;
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Right above
                        case 7:
                            if (!jumping)
                            {
                                falling = true;
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Right below
                        case 8:
                            if (!jumping)
                            {
                                falling = true;
                                stopJumpOn = floorHeight;
                            }
                            break;

                        // Right, same Y
                        case 9:
                            if (!jumping)
                            {
                                falling = true;
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

        private void AlignOffset()
        {
            
        }
    }
}
