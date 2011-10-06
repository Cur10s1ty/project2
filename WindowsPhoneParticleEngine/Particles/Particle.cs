using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsPhoneParticleEngine.Particles
{
    public class Particle
    {

        public ParticleEmitter emitter { get; set; }
        public Texture2D texture { get; set; }
        public Vector3 position { get; set; }
        public Vector3 speed { get; set; }
        public float rotation { get; set; }
        public float rotationSpeed { get; set; }
        public Color color { get; set; }
        public float scale { get; set; }
        public float scaleDecreaseStep { get; set; }
        public int lifetime { get; set; }
        private Vector2 offset { get; set; }

        public Random random { get; set; }

        public Particle(ParticleEmitter emitter)
        {
            this.random = new Random();

            this.emitter = emitter;
            this.texture = emitter.textures.ElementAt(random.Next(emitter.textures.Count));
            this.position = emitter.location;

            this.speed = new Vector3(emitter.particleMovementSpeed * (float)(random.NextDouble() * 2 - 1),
                                    emitter.particleMovementSpeed * (float)(random.NextDouble() * 2 - 1), 0f);

            this.rotation = 0f;
            this.rotationSpeed = 0.1f * (float)(random.NextDouble() * 2 - 1);

            this.color = emitter.color;
            this.scale = (float)random.NextDouble() * emitter.defaultScale;
            this.scaleDecreaseStep = scale / 10f;
            this.lifetime = 20 + random.Next(40);
        }

        public void Update(GameTime gt, Vector2 offset)
        {
            lifetime--;
            position += speed;
            rotation += rotationSpeed;
            this.offset = offset;

            if (lifetime < 10)
            {
                scale -= scaleDecreaseStep;
            }

            if (lifetime <= 0)
            {
                emitter.removeParticles.AddLast(this);
            }
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            Rectangle sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = new Vector2(0, 0);

            sb.Draw(texture, new Vector2(position.X - offset.X, position.Y), sourceRect, color, rotation, origin, scale, SpriteEffects.None, position.Z);
        }
    }
}
