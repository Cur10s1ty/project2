using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Misc;
using Projectiles;

namespace ProjectTBA.Projectiles
{
    public class Shuriken : Projectile
    {
        public Shuriken(float x, float y, float xSpeed, float ySpeed)
            : base(x, y)
        {
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            frameAmount = 2;
            this.texture = AkumaContentManager.shurikenTex;
        }
    }
}
