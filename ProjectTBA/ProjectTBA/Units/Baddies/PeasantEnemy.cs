using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Units.Baddies
{
    public class PeasantEnemy : DefaultEnemy
    {
        public PeasantEnemy(float x, float y)
            : base(x, y, null)
        {
        }
    }
}
