using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneParticleEngine.Particles;

namespace WindowsPhoneParticleEngine.Emitters
{
    public class PointEmitter : ParticleEmitter
    {
        
        public PointEmitter(LinkedList<Texture2D> textures, Vector3 location, int particlesPerFrame, float particleMovementSpeed, Color color) :
            base(textures)
        {
            this.location = location;
            this.total = particlesPerFrame;
            this.particleMovementSpeed = particleMovementSpeed;
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

            foreach (Particle p in particles)
            {
                p.Update(gt, offset);
            }

            foreach (Particle p in removeParticles)
            {
                particles.Remove(p);
            }

            removeParticles.Clear();
        }

        internal override void Draw(GameTime gt, SpriteBatch sb)
        {
            foreach (Particle p in particles)
            {
                p.Draw(gt, sb);
            }
        }
    }
}
