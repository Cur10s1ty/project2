using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using WindowsPhoneParticleEngine.Emitters;

namespace WindowsPhoneParticleEngine
{
    public class ParticleEmitterManager
    {

        public LinkedList<ParticleEmitter> emitters = new LinkedList<ParticleEmitter>();

        public enum EmitterType
        {
            Point
        }

        private static ParticleEmitterManager instance = null;

        public static ParticleEmitterManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ParticleEmitterManager();
            }
            return instance;
        }

        public void Update(GameTime gt, Vector2 offset)
        {
            foreach (ParticleEmitter pe in emitters)
            {
                pe.Update(gt, offset);
            }
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            foreach (ParticleEmitter pe in emitters)
            {
                pe.Draw(gt, sb);
            }
        }

        public void AddEmitter(EmitterType type, LinkedList<Texture2D> textures, Vector3 location, int particlesPerFrame, float particleMovementSpeed, Color color)
        {
            switch (type)
            {
                case EmitterType.Point:
                    emitters.AddLast(new PointEmitter(textures, location, particlesPerFrame, particleMovementSpeed, color));
                    break;

                default:
                    break;
            }
        }
    }
}
