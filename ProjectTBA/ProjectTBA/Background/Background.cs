using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;

namespace ProjectTBA.Background
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
            sb.Draw(TBAContentManager.forestBGBackTex, new Rectangle(0, 0, 1600, 480), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            sb.Draw(TBAContentManager.forestBGMiddleTex, new Rectangle(0, 0, 1600, 480), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9999f);
            sb.Draw(TBAContentManager.forestBGFrontTex, new Rectangle(0, 0, 1600, 480), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9998f);
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
    }
}
