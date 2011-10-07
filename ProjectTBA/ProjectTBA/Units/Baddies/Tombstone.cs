using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Misc;
using ProjectTBA.Levels;
using ProjectTBA.Creatures;

namespace ProjectTBA.Units.Baddies
{
    public class Tombstone
    {
        private Vector2 location;
        private Texture2D texture;
        private float scale = 0.4f;
        private Level currentLevel;

        public Tombstone(Unit source)
        {
            currentLevel = Game1.GetInstance().currentLevel;
            int chance = currentLevel.random.Next(4);

            switch (chance)
            {
                case 0:
                    this.texture = AkumaContentManager.stoneTex;
                    break;
                case 1:
                    this.texture = AkumaContentManager.stoneTex2;
                    break;
                case 2:
                    this.texture = AkumaContentManager.stoneTex3;
                    break;
                case 3:
                    this.texture = AkumaContentManager.stoneTex4;
                    break;
                default:
                    this.texture = AkumaContentManager.stoneTex;
                    break;
            }

            if (source is DefaultEnemy)
            {
                DefaultEnemy enemy = (DefaultEnemy)source;
                if (enemy.falling || enemy.jumping)
                {
                    location.X = enemy.location.X;
                    location.Y = enemy.stopJumpOn + enemy.texture.Height - texture.Height * scale;
                }
                else
                {
                    location.X = enemy.location.X;
                    location.Y = currentLevel.levelHeight - texture.Height * scale - 2;
                }
            }
        }

        public Tombstone(Creature source)
        {
            currentLevel = Game1.GetInstance().currentLevel;
            int chance = currentLevel.random.Next(4);

            switch (chance)
            {
                case 0:
                    this.texture = AkumaContentManager.stoneTex;
                    break;
                case 1:
                    this.texture = AkumaContentManager.stoneTex2;
                    break;
                case 2:
                    this.texture = AkumaContentManager.stoneTex3;
                    break;
                case 3:
                    this.texture = AkumaContentManager.stoneTex4;
                    break;
                default:
                    this.texture = AkumaContentManager.stoneTex;
                    break;
            }

            if (source is Deer)
            {
                Deer deer = (Deer)source;
                location.X = deer.location.X;
                location.Y = currentLevel.levelHeight - texture.Height * scale - 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                new Vector2((int)location.X - currentLevel.offset.X, (int)location.Y),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0.1001f);
        }
    }
}
