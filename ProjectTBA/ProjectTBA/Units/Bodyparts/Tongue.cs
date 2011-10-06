using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProjectTBA.Units.Bodyparts
{
    public class Tongue
    {
        private Demon source;
        private Vector2 location;
        private int maxRange = 100;
        private int speed = 10; 
        private Boolean retracting = false;
        private LinkedList<Texture2D> tongueTextures;

        public Tongue(Demon source)
        {
            this.source = source;
            location = source.location;
        }

        public void Update()
        {
            if (source.spriteEffect == SpriteEffects.None)
            {
                if (retracting)
                {
                    this.location.X -= speed;
                }
                else
                {
                    this.location.X += speed;
                }
            }
            else
            {
                if (retracting)
                {
                    this.location.X += speed;
                }
                else
                {
                    this.location.X -= speed;
                }
            }
            Game1.GetInstance().controller.hasSwiped = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
