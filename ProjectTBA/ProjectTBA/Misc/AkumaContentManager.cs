using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTBA.Misc
{
    public static class AkumaContentManager
    {

        // Player
        public static Texture2D playerTex { get; set; }

        // Misc
        public static Texture2D solidTex { get; set; }
        public static SpriteFont testFont { get; set; }

        // Controls
        public static Texture2D dPadTex { get; set; }
        public static Texture2D buttonTex { get; set; }

        // Backgrounds
        public static Texture2D forestBGFrontTex { get; set; }
        public static Texture2D forestBGMiddleTex { get; set; }
        public static Texture2D forestBGBackTex { get; set; }

        // Platforms
        public static Texture2D forestPlatformTrunkTex { get; set; }
        public static Texture2D forestPlatformLeaf1Tex { get; set; }
        public static Texture2D forestPlatformLeaf2Tex { get; set; }
        public static Texture2D forestPlatformLeaf3Tex { get; set; }

        // Test
        public static Texture2D testPlayerTex { get; set; }
        public static Texture2D testPlatfromTex { get; set; }
        public static Texture2D testEnemyTex { get; set; }

        public static void LoadContent()
        {
            ContentManager content = Game1.GetInstance().Content;

            // Player
            playerTex = content.Load<Texture2D>("Player/Demon");

            // Misc
            solidTex = content.Load<Texture2D>("Misc/Solid");
            testFont = content.Load<SpriteFont>("Misc/TestFont");

            // Controls
            dPadTex = content.Load<Texture2D>("Controls/DPad");
            buttonTex = content.Load<Texture2D>("Controls/ActionButtons");

            // Backgrounds
            forestBGFrontTex = content.Load<Texture2D>("Backgrounds/Forest/Level_1_1");
            forestBGMiddleTex = content.Load<Texture2D>("Backgrounds/Forest/Level_1_2");
            forestBGBackTex = content.Load<Texture2D>("Backgrounds/Forest/Level_1_3");

            // Platforms
            forestPlatformTrunkTex = content.Load<Texture2D>("Platforms/Forest/Trunk");
            forestPlatformLeaf1Tex = content.Load<Texture2D>("Platforms/Forest/Leaf1");
            forestPlatformLeaf2Tex = content.Load<Texture2D>("Platforms/Forest/Leaf2");
            forestPlatformLeaf3Tex = content.Load<Texture2D>("Platforms/Forest/Leaf3");

            // Test
            testPlayerTex = content.Load<Texture2D>("Test/TestPlayer");
            testPlatfromTex = content.Load<Texture2D>("Test/TestPlatform");
            testEnemyTex = content.Load<Texture2D>("Test/TestEnemy");
        }
    }
}
