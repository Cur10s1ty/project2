using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Obstacles
{
    public abstract class Obstacle
    {

        public Rectangle bounds { get; set; }
        public Texture2D texture { get; set; }

        public Obstacle(int x, int y, Texture2D texture)
        {
            this.texture = texture;
            this.bounds = new Rectangle(x, y, texture.Width, texture.Height);
        }

        public abstract void Update(GameTime gt);

        internal abstract void Draw(GameTime gt, SpriteBatch sb);

        public Rectangle GetRectangle()
        {
            return new Rectangle(bounds.X - (int)Game1.GetInstance().offset.X, bounds.Y, bounds.Width, bounds.Height);
        }
    }
}
