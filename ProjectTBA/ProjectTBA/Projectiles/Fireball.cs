using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projectiles;
using ProjectTBA.Misc;
using Microsoft.Xna.Framework;
using ProjectTBA.Units;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Projectiles
{
    public class Fireball : Projectile
    {
        private int lifeTime = 100;
        private SpriteEffects spriteEffect;
        private float scale = 1f;
        private Boolean moveLeft;

        public Fireball(float x, float y, Boolean moveLeft, float scale)
            : base(x, y)
        {
            this.scale = scale;
            this.moveLeft = moveLeft;
            if (moveLeft)
            {
                this.xSpeed = -5;
                spriteEffect = SpriteEffects.FlipHorizontally;                
            }
            else
            {
                this.location.X += 86;
                spriteEffect = SpriteEffects.None;
                this.xSpeed = 5;
            }
            this.texture = AkumaContentManager.fireballTex;
        }

        public override void Update(GameTime gameTime)
        {
            if (lifeTime < 0)
            {
                this.isDead = true;
                return;
            }

            foreach (Unit baddie in game.currentLevel.baddies)
            {
                //todo current dirty check on ALL the baddies
                if (this.GetRectangle().Intersects(baddie.GetRectangle()))
                {
                    baddie.Hit(this);
                    //TODO lol instakill
                    this.isDead = true;
                }
            }

            this.location.X += this.xSpeed;

            lifeTime--;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(texture, this.location, new Rectangle(0, 0, 100, 100), Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, spriteEffect, 0.1f);
        }

        public override string ToString()
        {
            return "Fireball";
        }
    }
}
