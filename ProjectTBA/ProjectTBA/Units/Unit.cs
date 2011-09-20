using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Units
{
    public abstract class Unit
    {
        public Game1 game;

        public Unit()
        {
            game = Game1.GetInstance();
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void Attack();

        public abstract void Die();
    }
}
