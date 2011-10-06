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

        public Game1 game { get; set; }
        public Texture2D texture { get; set; }
        public Vector2 location;
        public float movementSpeed;

        private DateTime currentDT;
        private DateTime previousDT;
        private long elapsedMilliseconds;

        public Boolean jumping;
        public Boolean jumpUp;
        public int maxJumpHeight;
        public int stopJumpOn;
        public int floorHeight;

        public Boolean falling;

        public TestPlayer(float x, float y)
        {
            this.game = Game1.GetInstance();
            this.texture = AkumaContentManager.demonWalkTex;
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
            currentDT = DateTime.Now;

            if (previousDT == null)
            {
                previousDT = currentDT;
            }

            long delta = 0;
            if (previousDT.Millisecond > currentDT.Millisecond)
            {
                delta += 1000 - previousDT.Millisecond;
                delta += currentDT.Millisecond;
            }
            else
            {
                delta += currentDT.Millisecond - previousDT.Millisecond;
            }
            elapsedMilliseconds += delta;

            if (elapsedMilliseconds >= 40)
            {

                CheckBottomCollision();

                if (ControllerState.IsButtonPressed(ControllerState.Buttons.RIGHT))
                {
                    if (location.X + texture.Width < 1600)
                    {
                        location.X += movementSpeed;
                    }
                    else
                    {
                        location.X = 1600 - texture.Width;
                    }

                    if (game.currentLevel.offset.X + movementSpeed < 800)
                    {
                        if (location.X + game.currentLevel.offset.X + texture.Width > 600)
                        {
                            game.currentLevel.offset.X += movementSpeed;
                        }
                    }
                    else
                    {
                        game.currentLevel.offset.X = 800;
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

                    if (game.currentLevel.offset.X - movementSpeed > 0)
                    {
                        if (location.X - game.currentLevel.offset.X < 200)
                        {
                            game.currentLevel.offset.X -= movementSpeed;
                        }
                    }
                    else
                    {
                        game.currentLevel.offset.X = 0;
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

                elapsedMilliseconds = 0;
            }

            previousDT = currentDT;
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)game.currentLevel.offset.X, (int)location.Y, texture.Width, texture.Height);
        }

        public Rectangle GetFeetHitbox()
        {
            return new Rectangle((int)location.X + 37, (int)location.Y + 84, 48, 1);
        }

        private void CheckBottomCollision()
        {
            Boolean abovePlatform = false;

            if (!jumpUp)
            {
                foreach (Obstacle o in game.currentLevel.obstacles)
                {
                    if (o is Platform)
                    {
                        Platform p = (Platform)o;

                        if (GetFeetHitbox().X + GetFeetHitbox().Width > p.bounds.X && GetFeetHitbox().X < p.bounds.X + p.bounds.Width)
                        {
                            if (GetFeetHitbox().Y < p.bounds.Y)
                            {
                                abovePlatform = true;

                                if (jumping && !falling)
                                {
                                    stopJumpOn = p.bounds.Y - texture.Height;
                                }
                                else
                                {
                                    stopJumpOn = p.bounds.Y - texture.Height;
                                    falling = true;
                                }
                            }
                        }
                    }
                }
            }

            if (!abovePlatform && location.Y != floorHeight)
            {
                stopJumpOn = floorHeight;
                falling = true;
            }
        }
    }
}
