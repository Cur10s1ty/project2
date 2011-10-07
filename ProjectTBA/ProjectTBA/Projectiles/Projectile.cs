using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA;

namespace Projectiles
{
    public abstract class Projectile
    {
        public Vector2 location;
        public Texture2D texture;
        protected int frameAmount = 1;
        private int atFrame = 0;
        public Boolean isDead = false;
        public float xSpeed = 0;
        public float ySpeed = 0;
        protected Game1 game;
        public int damage = 30;

        public Projectile(float x, float y)
        {
            this.location = new Vector2(x, y);
            this.game = Game1.GetInstance();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.location.X - game.player.location.X > -50 && this.location.X - game.player.location.X < 50)
            {
                if (game.player.GetRectangle().Intersects(this.GetRectangle()) && !game.player.isHit)
                {
                    game.player.Hit(this);
                }
            }

            if (this.location.Y > 450)
            {
                this.isDead = true;
                this.location.X = -100;
                this.location.Y = -100;
            }
            else
            {
                this.location.X += xSpeed;
                this.location.Y += ySpeed;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (atFrame == frameAmount - 1)
            {
                atFrame = 0;
            }
            else
            {
                atFrame++;
            }

            spriteBatch.Draw(texture, this.location, new Rectangle(0,0,31,30), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.1f);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)location.X - (int)Game1.GetInstance().currentLevel.offset.X,
                (int)location.Y, texture.Width, texture.Height);
        }

        public override string ToString()
        {
            return "Default Projectile";
        }
    }
}
