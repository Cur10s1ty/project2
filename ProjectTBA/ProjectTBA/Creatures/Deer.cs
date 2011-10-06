using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;

namespace ProjectTBA.Creatures
{
    public class Deer : Creature
    {

        public Texture2D walkTex { get; set; }
        public Texture2D jumpTex { get; set; }
        public Random random { get; set; }

        public Boolean jumping { get; set; }
        public double jumpCount { get; set; }

        public Deer(Vector2 location)
            : base(location)
        {
            this.walkTex = AkumaContentManager.deerWalkTex;
            this.jumpTex = AkumaContentManager.deerJumpTex;
            this.random = new Random();
        }

        public override void Update(GameTime gt)
        {
            if (jumping)
            {
                jumpCount += 0.2;


            }
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            if (jumping)
            {
            }
            else
            {
            }
        }
    }
}
