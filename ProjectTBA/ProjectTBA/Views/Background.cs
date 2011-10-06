using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;

namespace ProjectTBA.Views
{
    public class Background
    {

        public Texture2D front { get; set; }
        public Texture2D middle { get; set; }
        public Texture2D back { get; set; }
        public Texture2D ground { get; set; }
        public BGType type { get; set; }

        public enum BGType
        {
            ForestTutorial,
            Forest,
            ForestArena
        }

        public Background(BGType type)
        {
            this.type = type;
            SetBackground();
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            switch (type)
            {
                case BGType.ForestTutorial:
                case BGType.Forest:
                    sb.Draw(back, new Rectangle(0, 0, 800, 400), GetBackDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                    sb.Draw(middle, new Rectangle(0, 0, 800, 400), GetMiddleDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9999f);
                    sb.Draw(front, new Rectangle(0, 0, 800, 400), GetFrontDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9998f);
                    sb.Draw(ground, new Rectangle(0, 380, 800, 100), GetGroundDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9997f);
                    break;

                case BGType.ForestArena:
                    sb.Draw(front, new Rectangle(0, 0, 800, 400), GetFrontDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9998f);
                    sb.Draw(ground, new Rectangle(0, 380, 800, 100), GetGroundDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9997f);
                    break;
            }
        }

        public void SetBackground()
        {
            switch (type)
            {
                case BGType.ForestTutorial:
                case BGType.Forest:
                    front = AkumaContentManager.forestBGFrontTex;
                    middle = AkumaContentManager.forestBGMiddleTex;
                    back = AkumaContentManager.forestBGBackTex;
                    ground = AkumaContentManager.forestGroundTex;
                    break;

                case BGType.ForestArena:
                    front = AkumaContentManager.forestBGArenaTex;
                    ground = AkumaContentManager.forestGroundTex;
                    break;

                default:
                    break;
            }
        }

        public Rectangle GetFrontDrawRectangle()
        {
            return new Rectangle((int)Game1.GetInstance().currentLevel.offset.X, 0, 800, 400);
        }

        public Rectangle GetMiddleDrawRectangle()
        {
            return new Rectangle((int)((Game1.GetInstance().currentLevel.offset.X / 800f) * 500f), 0, 1100, 400);
        }

        public Rectangle GetBackDrawRectangle()
        {
            return new Rectangle((int)((Game1.GetInstance().currentLevel.offset.X / 800f) * 200f), 0, 1400, 400);
        }

        public Rectangle GetGroundDrawRectangle()
        {
            return new Rectangle((int)Game1.GetInstance().currentLevel.offset.X, 0, 800, 100);
        }
    }
}
