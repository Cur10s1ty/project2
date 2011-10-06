using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;

namespace ProjectTBA.Obstacles
{
    public class Wall : Obstacle
    {
        
        public Wall(int x, int y, Texture2D texture) :
            base(x, y, texture)
        {
        }

        public override void Update(GameTime gt)
        {
        }

        internal override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            sb.Draw(AkumaContentManager.solidTex, GetRectangle(), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }

        public Rectangle GetHitbox()
        {
            return new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }
    }
}
