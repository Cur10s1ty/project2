using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WindowsPhoneParticleEngine.Particles;

namespace WindowsPhoneParticleEngine
{
    public abstract class ParticleEmitter
    {

        public int total = 1;
        public LinkedList<Particle> particles = new LinkedList<Particle>();
        public LinkedList<Particle> removeParticles = new LinkedList<Particle>();
        public LinkedList<Texture2D> textures = new LinkedList<Texture2D>();
        public Vector3 location { get; set; }
        public Vector3 speed { get; set; }
        public float particleMovementSpeed { get; set; }
        public Color color;
        public float defaultScale = 1f;

        public ParticleEmitter(LinkedList<Texture2D> textures)
        {
            this.particles = new LinkedList<Particle>();
            this.removeParticles = new LinkedList<Particle>();
            this.textures = textures;
        }

        public abstract void SpawnParticle();

        public abstract void Update(GameTime gt, Vector2 offset);

        internal abstract void Draw(GameTime gt, SpriteBatch sb);

        public abstract void Dispose();
    }
}
