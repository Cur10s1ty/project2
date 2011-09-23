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

        public enum BGType
        {
            Forest
        }

        public Background(BGType type)
        {
            SetBackground(type);
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(TBAContentManager.forestBGBackTex, new Rectangle(0, 0, 800, 480), GetBackDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            sb.Draw(TBAContentManager.forestBGMiddleTex, new Rectangle(0, 0, 800, 480), GetMiddleDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9999f);
            sb.Draw(TBAContentManager.forestBGFrontTex, new Rectangle(0, 0, 800, 480), GetFrontDrawRectangle(), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9998f);
        }

        public void SetBackground(BGType type)
        {
            switch (type)
            {
                case BGType.Forest:
                    front = TBAContentManager.forestBGFrontTex;
                    middle = TBAContentManager.forestBGMiddleTex;
                    back = TBAContentManager.forestBGBackTex;
                    break;

                default:
                    break;
            }
        }

        public Rectangle GetFrontDrawRectangle()
        {
            return new Rectangle((int)Game1.GetInstance().offset.X, 0, 800, 480);
        }

        public Rectangle GetMiddleDrawRectangle()
        {
            return new Rectangle((int)((Game1.GetInstance().offset.X / 800f) * 500f), 0, 1100, 480);
        }

        public Rectangle GetBackDrawRectangle()
        {
            return new Rectangle((int)((Game1.GetInstance().offset.X / 800f) * 200f), 0, 1400, 480);
        }
    }
}
