using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;
using System.Diagnostics;
using ProjectTBA.Creatures;

namespace ProjectTBA.Units.Bodyparts
{
    public class Tongue
    {
        public Demon source;
        private Vector2 location;
        private int maxRange = 250;
        private int speed = 7;
        private int distanceTravelled = 0;
        public Boolean retracting = false;
        private LinkedList<Texture2D> tongueTextures;
        private Texture2D pieceTex;
        private Texture2D sourceTex;
        private Game1 game;
        
        public Tongue(Demon source)
        {
            tongueTextures = new LinkedList<Texture2D>();
            pieceTex = AkumaContentManager.tonguePieceTex;
            sourceTex = AkumaContentManager.tongueSourceTex;
            tongueTextures.AddFirst(sourceTex);
            this.source = source;
            location = source.location;
            location.Y = source.location.Y + source.walkTex.Height - 34;
            game = source.game;
        }

        public void Update()
        {
            foreach (Unit enemy in game.currentLevel.baddies)
            {
                if (enemy.GetRectangle().Intersects(this.GetRectangle()))
                {
                    enemy.movementSpeed = 1f;
                    source.ResetTongue();
                    retracting = true;
                    break;
                }
            }

            foreach (Creature creature in game.currentLevel.creatures)
            {
                if (creature.GetRectangle().Intersects(this.GetRectangle()))
                {
                    creature.Stick(this);
                    retracting = true;
                    break;
                }
            }


            if (source.spriteEffect == SpriteEffects.None)
            {
                if (retracting)
                {
                    if (distanceTravelled == 0)
                    {
                        Game1.GetInstance().controller.hasSwiped = false;
                        retracting = false;
                        source.isTongueActive = false;
                    }
                    else
                    {
                        this.location.X -= speed;
                        distanceTravelled -= speed;
                        tongueTextures.RemoveLast();
                    }
                }
                else
                {
                    if (distanceTravelled >= maxRange)
                    {
                        retracting = true;
                    }
                    else
                    {
                        this.location.X += speed;
                        distanceTravelled += speed;
                        NewTonguePiece();
                    }
                }

            }
            else
            {
                if (retracting)
                {
                    if (distanceTravelled == 0)
                    {
                        Game1.GetInstance().controller.hasSwiped = false;
                        retracting = false;
                        source.isTongueActive = false;
                    }
                    else
                    {
                        this.location.X += speed;
                        distanceTravelled -= speed;
                        tongueTextures.RemoveLast();
                    }
                }
                else
                {
                    if (distanceTravelled >= maxRange)
                    {
                        retracting = true;
                    }
                    else
                    {
                        this.location.X -= speed;
                        distanceTravelled += speed;
                        NewTonguePiece();
                    }
                }
            }
        }

        private void NewTonguePiece()
        {
            Texture2D tempTex;
            tempTex = AkumaContentManager.tonguePieceTex;
            tongueTextures.AddLast(tempTex);
        }

        public void Reset()
        {
            this.location = source.GetDrawLocation();
            location.Y = source.location.Y + source.walkTex.Height - 34;
            distanceTravelled = 0;
            retracting = false;
            source.isTongueActive = false;
            tongueTextures.Clear();
            tongueTextures.AddLast(sourceTex);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (source.spriteEffect == SpriteEffects.None)
            {

                int i = 0;
                foreach (Texture2D tex in tongueTextures)
                {
                    if (i != 0)
                    {
                        spriteBatch.Draw(tex, new Rectangle((int)location.X + 86 - (i * 7), (int)location.Y + 3, tex.Width, tex.Height), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(tex, new Rectangle((int)location.X + 86 - (i * 7), (int)location.Y, tex.Width, tex.Height), Color.White);
                    }
                    i++;
                }
            }
            else
            {

                int i = 0;
                foreach (Texture2D tex in tongueTextures)
                {
                    if (i != 0)
                    {
                        spriteBatch.Draw(tex,
                            new Vector2((int)location.X + (i * 7), (int)location.Y + 3),
                            new Rectangle(0, 0, tex.Width, tex.Height),
                            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0.1f);
                    }
                    else
                    {
                        spriteBatch.Draw(tex, 
                            new Vector2((int)location.X + (i * 7), (int)location.Y), 
                            new Rectangle(0,0,tex.Width, tex.Height),
                            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0.1f);
                    }
                    i++;
                }
            }
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle((int) location.X + 14, (int) location.Y, 13, 8);
        }
    }
}
