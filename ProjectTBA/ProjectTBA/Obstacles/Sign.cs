using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Levels;

namespace ProjectTBA.Obstacles
{
    public class Sign : Obstacle
    {

        public Boolean isEnd = false;
        public int nextLevelCounter = 0;

        public Sign(int x, int y, Texture2D texture) :
            base(x, y, texture)
        {
        }

        public Sign(int x, int y, Texture2D texture, Boolean isEnd) :
            base(x, y, texture)
        {
            this.isEnd = isEnd;
        }

        public override void Update(GameTime gt)
        {
            if (isEnd)
            {
                if (GetRectangle().Intersects(Game1.GetInstance().player.GetRectangle()))
                {
                    if (nextLevelCounter >= 60)
                    {
                        int currentLevel = Game1.GetInstance().currentLevel.level;
                        Game1.GetInstance().currentLevel = new Level(Game1.GetInstance().player, (currentLevel + 1));
                        Game1.GetInstance().currentLevel.GenerateLevel(Game1.GetInstance().currentLevel.level);
                    }
                    nextLevelCounter++;
                }
            }
        }

        internal override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.002f);
        }
    }
}
