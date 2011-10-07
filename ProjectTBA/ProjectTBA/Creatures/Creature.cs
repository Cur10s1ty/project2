using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Units.Bodyparts;

namespace ProjectTBA.Creatures
{
    public abstract class Creature
    {

        public Vector2 location;
        public Boolean stuck = false;
        public Tongue tongue;
        public Vector2 speed;

        public Creature(Vector2 location)
        {
            this.location = location;
        }

        public abstract void Update(GameTime gt);

        public abstract void Draw(GameTime gt, SpriteBatch sb);

        public abstract Rectangle GetRectangle();

        public void Stick(Tongue tongue)
        {
            this.tongue = tongue;
            stuck = true;
            if (tongue.source.location.X < this.location.X)
            {
                this.speed.X = -7;
            }
            else
            {
                this.speed.X = 7;
            }
        }

        public void Unstick()
        {
            stuck = false;
        }
    }
}
