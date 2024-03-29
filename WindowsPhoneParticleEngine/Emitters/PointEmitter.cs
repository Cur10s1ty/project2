using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneParticleEngine.Particles;
using System.Diagnostics;

namespace WindowsPhoneParticleEngine.Emitters
{
    public class PointEmitter : ParticleEmitter
    {
        
        public PointEmitter(LinkedList<Texture2D> textures, Vector3 location, Vector3 speed, int particlesPerFrame, Vector2 particleMovementSpeed, float defaultScale, int lifetime, Color color) :
            base(textures)
        {
            this.location = location;
            this.speed = speed;
            this.total = particlesPerFrame;
            this.particleMovementSpeed = particleMovementSpeed;
            this.defaultScale = defaultScale;
            this.lifetime = lifetime;
            this.color = color;
        }

        public override void SpawnParticle()
        {
            particles.AddLast(new Particle(this));
        }

        public override void Update(GameTime gt, Vector2 offset)
        {
            for (int i = 0; i < total; i++)
            {
                SpawnParticle();
            }

            location += speed;

            foreach (Particle p in particles)
            {
                p.Update(gt, offset);
            }

            foreach (Particle p in removeParticles)
            {
                particles.Remove(p);
            }

            removeParticles.Clear();

            if (total == 0 && particles.Count == 0)
            {
                ParticleEmitterManager.GetInstance().removeEmitters.AddLast(this);
            }

            if (lifetime == 0)
            {
                this.Dispose();
            }

            if (lifetime != -1)
            {
                lifetime--;
            }
        }

        internal override void Draw(GameTime gt, SpriteBatch sb)
        {
            foreach (Particle p in particles)
            {
                p.Draw(gt, sb);
            }
        }

        public override void Dispose()
        {
            this.total = 0;
        }
    }
}
