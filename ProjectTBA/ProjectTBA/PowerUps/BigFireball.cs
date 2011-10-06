using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.PowerUps
{
    public class BigFireball : PowerUp
    {

        public Vector2 startLocation { get; set; }
        public Boolean goUp { get; set; }

        public BigFireball(Vector2 location)
            : base(location)
        {
            this.startLocation = location;
            this.texture = AkumaContentManager.fireBallPowerUpTex;
            this.goUp = true;
        }

        public override void Update(GameTime gt)
        {
            if (goUp)
            {
                if (startLocation.Y - location.Y > 10)
                {
                    goUp = false;
                }

                location.Y -= 0.2f;
            }
            else
            {
                if (location.Y - startLocation.Y > 10)
                {
                    goUp = true;
                }

                location.Y += 0.2f;
            }
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, GetDrawRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1001f);
        }
    }
}
