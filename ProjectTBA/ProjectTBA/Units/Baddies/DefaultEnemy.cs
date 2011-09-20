using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Units.Baddies
{
    public class DefaultEnemy : Unit
    {
        public DefaultEnemy()
            : base()
        {
            game.AddUnitToUnits(this);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Attack()
        {
            //throw new NotImplementedException();
        }

        public override void Die()
        {
            game.RemoveUnitFromUnits(this);
        }
    }
}
