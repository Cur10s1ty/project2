using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Units;
using ProjectTBA.Tests;

namespace ProjectTBA.Obstacles
{
    public class Platform : Obstacle
    {

        public Boolean bottomCollision;

        public Platform(int x, int y, Texture2D texture, Boolean bottomCollision) :
            base(x, y, texture)
        {
            this.bottomCollision = bottomCollision;
        }

        public override void Update(GameTime gt)
        {
        }

        internal override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
        }

        public Rectangle GetTopHitbox()
        {
            return new Rectangle(bounds.X + (int)Game1.GetInstance().offset.X, bounds.Y, bounds.Width, 1);
        }

        /// <summary>
        /// Get the position relative to the player
        /// 1: Directly above the obstacle
        /// 2: Directly below the obstacle
        /// 3: On the same X and Y as the obstacle
        /// 4: Left and above the obstacle
        /// 5: Left and below the obstacle
        /// 6: Left and on the same Y as the obstacle
        /// 7: Right and above the obstacle
        /// 8: Right and above the obstacle
        /// 9: Right and on the same Y as the obstacle
        /// 0: Off the screen
        /// </summary>
        /// <returns>
        /// An integer
        /// </returns>
        // TODO: TestPlayer ==> Unit (when units have locations)
        public int GetPositionRelativeToUnit(Unit unit)
        {
            Rectangle location = unit.GetFeetHitbox();

            // Check if player is on the same X as the obstacle
            if (location.X < bounds.X + bounds.Width && location.X + location.Width > bounds.X)
            {
                // Check if the unit is above, below or over the obstacle
                if (unit.location.Y + unit.texture.Height < bounds.Y)
                {
                    return 1;
                }
                else if (unit.location.Y < bounds.Y + bounds.Height)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else if (location.X + location.Width < bounds.X)
            {
                // Check if the unit is above, below or over the obstacle
                if (unit.location.Y + unit.texture.Height < bounds.Y)
                {
                    return 4;
                }
                else if (unit.location.Y > bounds.Y + bounds.Height)
                {
                    return 5;
                }
                else
                {
                    return 6;
                }
            }
            else if (location.X > bounds.X + bounds.Width)
            {
                // Check if the unit is above, below or over the obstacle
                if (unit.location.Y + unit.texture.Height < bounds.Y)
                {
                    return 7;
                }
                else if (unit.location.Y > bounds.Y + bounds.Height)
                {
                    return 8;
                }
                else
                {
                    return 9;
                }
            }

            return 0;
        }
    }
}
