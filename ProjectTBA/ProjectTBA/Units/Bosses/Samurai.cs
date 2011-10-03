using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Units
{
    class Samurai : Unit
    {
        public Samurai(float x, float y) 
            : base(x, y)
        {
            game.AddUnit(this);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Attack()
        {
        }

        public override void Die()
        {
            game.RemoveUnit(this);
        }

        public override Rectangle GetFeetHitbox()
        {
            return new Rectangle((int)location.X + 37 - (int)game.offset.X, (int)location.Y + 99, 36, 1);
        }
    }
}
