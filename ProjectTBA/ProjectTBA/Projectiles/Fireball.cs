using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projectiles;
using ProjectTBA.Misc;
using Microsoft.Xna.Framework;
using ProjectTBA.Units;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneParticleEngine;

namespace ProjectTBA.Projectiles
{
    public class Fireball : Projectile
    {
        private int lifeTime = 100;
        private SpriteEffects spriteEffect;
        private float scale = 1f;
        private Boolean moveLeft;

        private ParticleEmitter emitter = null;
        private ParticleEmitter emitter2 = null;

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

            LinkedList<Texture2D> textures = new LinkedList<Texture2D>();
            textures.AddLast(AkumaContentManager.circleParticle);
            this.emitter = ParticleEmitterManager.GetInstance().AddEmitter(ParticleEmitterManager.EmitterType.Point, textures, new Vector3(location.X, location.Y, 0.11f), new Vector3(xSpeed, 0f, 0f), 1 * (int)scale, 3f, scale, Color.Red);
            this.emitter2 = ParticleEmitterManager.GetInstance().AddEmitter(ParticleEmitterManager.EmitterType.Point, textures, new Vector3(location.X, location.Y, 0.11f), new Vector3(xSpeed, 0f, 0f), 2 * (int)scale, 3f, scale, Color.Orange);
        }

        public override void Update(GameTime gameTime)
        {
            if (lifeTime < 0)
            {
                this.isDead = true;
                this.emitter.Dispose();
                this.emitter2.Dispose();
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
                    this.emitter.Dispose();
                    this.emitter2.Dispose();
                }
            }

            this.location.X += this.xSpeed;

            lifeTime--;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(texture, GetDrawLocation(), new Rectangle(0, 0, 100, 100), Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, spriteEffect, 0.1f);
        }

        public Vector2 GetDrawLocation()
        {
            return new Vector2(location.X - game.currentLevel.offset.X, location.Y);
        }

        public override string ToString()
        {
            return "Fireball";
        }
    }
}
