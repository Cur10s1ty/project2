using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProjectTBA.PowerUps
{
    public abstract class PowerUp
    {

        public Texture2D texture { get; set; }
        public Vector2 location;

        public PowerUp(Vector2 location)
        {
            this.location = location;
        }

        public abstract void Update(GameTime gt);

        public abstract void Draw(GameTime gt, SpriteBatch sb);

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height);
        }

        public virtual Rectangle GetDrawRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().currentLevel.offset.X, (int)location.Y, texture.Width, texture.Height);
        }
    }
}
