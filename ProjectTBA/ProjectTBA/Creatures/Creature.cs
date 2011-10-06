using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Creatures
{
    public abstract class Creature
    {

        public Vector2 location { get; set; }

        public Creature(Vector2 location)
        {
            this.location = location;
        }

        public abstract void Update(GameTime gt);

        public abstract void Draw(GameTime gt, SpriteBatch sb);

        public abstract Rectangle GetRectangle();
    }
}
