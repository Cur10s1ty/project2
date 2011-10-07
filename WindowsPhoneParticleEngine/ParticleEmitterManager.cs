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
        public LinkedList<ParticleEmitter> removeEmitters = new LinkedList<ParticleEmitter>();

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

            foreach (ParticleEmitter pe in removeEmitters)
            {
                emitters.Remove(pe);
            }

            removeEmitters.Clear();
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            foreach (ParticleEmitter pe in emitters)
            {
                pe.Draw(gt, sb);
            }
        }

        public ParticleEmitter AddEmitter(EmitterType type, LinkedList<Texture2D> textures, Vector3 location, Vector3 speed, int particlesPerFrame, Vector2 particleMovementSpeed, float defaultScale, int lifetime, Color color)
        {
            ParticleEmitter emitter = null;

            switch (type)
            {
                case EmitterType.Point:
                    emitter = new PointEmitter(textures, location, speed, particlesPerFrame, particleMovementSpeed, defaultScale, lifetime, color);
                    emitters.AddLast(emitter);
                    break;

                default:
                    break;
            }

            return emitter;
        }
    }
}
