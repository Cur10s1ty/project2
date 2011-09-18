using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Misc
{
    public static class TBAContentManager
    {

        // Misc
        public static Texture2D solidTex { get; set; }
        public static SpriteFont testFont { get; set; }

        // Controls
        public static Texture2D dPadTex { get; set; }
        public static Texture2D buttonTex { get; set; }

        public static void Initialize()
        {
            ContentManager content = Game1.GetInstance().Content;

            // Misc
            solidTex = content.Load<Texture2D>("Misc/Solid");
            testFont = content.Load<SpriteFont>("Misc/TestFont");

            // Controls
            dPadTex = content.Load<Texture2D>("Controls/DPad");
            buttonTex = content.Load<Texture2D>("Controls/ActionButtons");
        }
    }
}
