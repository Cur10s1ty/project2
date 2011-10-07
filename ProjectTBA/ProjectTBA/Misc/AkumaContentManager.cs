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
        public static Texture2D demonJumpTex { get; set; }
        public static Texture2D demonWalkTex { get; set; }
        public static Texture2D demonTongueTex { get; set; }
        public static Texture2D tongueSourceTex { get; set; }
        public static Texture2D tonguePieceTex { get; set; }

        // Power-Up
        public static Texture2D fireBallPowerUpTex { get; set; }

        // Menus
        public static Texture2D mainMenuTex { get; set; }

        // Misc
        public static Texture2D solidTex { get; set; }
        public static SpriteFont testFont { get; set; }
        public static Texture2D shurikenTex { get; set; }
        public static Texture2D fireballTex { get; set; }
        public static Texture2D fireball2Tex { get; set; }

        // Controls
        public static Texture2D buttonLeftTex { get; set; }
        public static Texture2D buttonRightTex { get; set; }
        public static Texture2D buttonATex { get; set; }
        public static Texture2D buttonBTex { get; set; }

        // Creatures
        public static Texture2D deerWalkTex { get; set; }
        public static Texture2D deerJumpTex { get; set; }

        // Stones
        public static Texture2D stoneTex { get; set; }
        public static Texture2D stoneTex2 { get; set; }
        public static Texture2D stoneTex3 { get; set; }
        public static Texture2D stoneTex4 { get; set; }

        // Backgrounds
        public static Texture2D forestBGFrontTex { get; set; }
        public static Texture2D forestBGMiddleTex { get; set; }
        public static Texture2D forestBGBackTex { get; set; }
        public static Texture2D forestGroundTex { get; set; }
        public static Texture2D forestBGArenaTex { get; set; }

        // Platforms
        public static Texture2D forestPlatformTrunkTex { get; set; }
        public static Texture2D forestPlatformLeaf1Tex { get; set; }
        public static Texture2D forestPlatformLeaf2Tex { get; set; }
        public static Texture2D forestPlatformLeaf3Tex { get; set; }
        public static Texture2D forestWallTex { get; set; }

        // Baddies
        public static Texture2D baddieVillagerTex { get; set; }
        public static Texture2D baddieVillagerTex2 { get; set; }
        public static Texture2D baddieVillagerTex3 { get; set; }
        public static Texture2D baddieVillagerTex4 { get; set; }
        public static Texture2D baddieVillagerTex5 { get; set; }

        // Boss
        public static Texture2D samuraiTempTex { get; set; }

        // Particles
        public static Texture2D circleParticle { get; set; }

        // Signs
        public static Texture2D signLeftTex { get; set; }
        public static Texture2D signRightTex { get; set; }
        public static Texture2D signJumpATex { get; set; }
        public static Texture2D signEndTex { get; set; }
        public static Texture2D signBTex { get; set; }

        // Test
        public static Texture2D testPlayerTex { get; set; }
        public static Texture2D testPlatfromTex { get; set; }
        public static Texture2D testEnemyTex { get; set; }

        public static void LoadContent()
        {
            ContentManager content = Game1.GetInstance().Content;

            // Player
            demonJumpTex = content.Load<Texture2D>("Player/DemonJump");
            demonWalkTex = content.Load<Texture2D>("Player/DemonWalk");
            demonTongueTex = content.Load<Texture2D>("Player/tongMove");
            tongueSourceTex = content.Load<Texture2D>("Player/tongBack");
            tonguePieceTex = content.Load<Texture2D>("Player/tongFrontPart");

            // Power-Up
            fireBallPowerUpTex = content.Load<Texture2D>("Misc/scroll_vuur");

            // Menus
            mainMenuTex = content.Load<Texture2D>("Menus/beginscherm");

            // Misc
            solidTex = content.Load<Texture2D>("Misc/Solid");
            testFont = content.Load<SpriteFont>("Misc/TestFont");
            shurikenTex = content.Load<Texture2D>("Misc/weapon");
            fireballTex = content.Load<Texture2D>("Misc/vuurbal4");
            fireball2Tex = content.Load<Texture2D>("Misc/vuurbal5");

            // Controls
            buttonLeftTex = content.Load<Texture2D>("Controls/ButtonLeft");
            buttonRightTex = content.Load<Texture2D>("Controls/ButtonRight");
            buttonATex = content.Load<Texture2D>("Controls/ButtonA");
            buttonBTex = content.Load<Texture2D>("Controls/ButtonB");

            // Creatures
            deerWalkTex = content.Load<Texture2D>("Creatures/DeerWalk");
            deerJumpTex = content.Load<Texture2D>("Creatures/DeerJump");

            // Stones
            stoneTex = content.Load<Texture2D>("Baddies/Stones/stone1");
            stoneTex2 = content.Load<Texture2D>("Baddies/Stones/stone2");
            stoneTex3 = content.Load<Texture2D>("Baddies/Stones/stone3");
            stoneTex4 = content.Load<Texture2D>("Baddies/Stones/stone4");

            // Backgrounds
            forestBGFrontTex = content.Load<Texture2D>("Backgrounds/Forest/Level_1_1");
            forestBGMiddleTex = content.Load<Texture2D>("Backgrounds/Forest/Level_1_2");
            forestBGBackTex = content.Load<Texture2D>("Backgrounds/Forest/Level_1_3");
            forestGroundTex = content.Load<Texture2D>("Backgrounds/Forest/ground");
            forestBGArenaTex = content.Load<Texture2D>("Backgrounds/Forest/Battlefieldcollor");

            // Platforms
            forestPlatformTrunkTex = content.Load<Texture2D>("Platforms/Forest/trunk");
            forestPlatformLeaf1Tex = content.Load<Texture2D>("Platforms/Forest/Leaf1");
            forestPlatformLeaf2Tex = content.Load<Texture2D>("Platforms/Forest/Leaf2");
            forestPlatformLeaf3Tex = content.Load<Texture2D>("Platforms/Forest/Leaf3");
            forestWallTex = content.Load<Texture2D>("Platforms/Forest/barricade");

            // Baddies
            baddieVillagerTex = content.Load<Texture2D>("Baddies/civilian");
            baddieVillagerTex2 = content.Load<Texture2D>("Baddies/civilian2");
            baddieVillagerTex3 = content.Load<Texture2D>("Baddies/civilian3");
            baddieVillagerTex4 = content.Load<Texture2D>("Baddies/civilian4");
            baddieVillagerTex5 = content.Load<Texture2D>("Baddies/civilian5");

            // Boss
            samuraiTempTex = content.Load<Texture2D>("Baddies/ninjaWalking");

            // Particles
            circleParticle = content.Load<Texture2D>("Particles/CircleParticle");

            // Signs
            signLeftTex = content.Load<Texture2D>("Signs/linksbord");
            signRightTex = content.Load<Texture2D>("Signs/rechtsbord");
            signJumpATex = content.Load<Texture2D>("Signs/spring_a_bord");
            signEndTex = content.Load<Texture2D>("Signs/eindebord");
            signBTex = content.Load<Texture2D>("Signs/bbord");

            // Test
            testPlayerTex = content.Load<Texture2D>("Test/TestPlayer");
            testPlatfromTex = content.Load<Texture2D>("Test/TestPlatform");
            testEnemyTex = content.Load<Texture2D>("Test/TestEnemy");
        }
    }
}
