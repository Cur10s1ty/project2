using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Misc;

namespace ProjectTBA.Units.Baddies
{
    public class PeasantEnemy : DefaultEnemy
    {
        public PeasantEnemy(float x, float y)
            : base(x, y, null)
        {
            int chance = Game1.GetInstance().currentLevel.random.Next(5);
            switch (chance)
            {
                case 0:
                    this.texture = AkumaContentManager.baddieVillagerTex;
                    break;
                case 1:
                    this.texture = AkumaContentManager.baddieVillagerTex2;
                    break;
                case 2:
                    this.texture = AkumaContentManager.baddieVillagerTex3;
                    break;
                case 3:
                    this.texture = AkumaContentManager.baddieVillagerTex4;
                    break;
                case 4:
                    this.texture = AkumaContentManager.baddieVillagerTex5;
                    break;
                default:
                    this.texture = AkumaContentManager.baddieVillagerTex;
                    break;
            }
        }
    }
}
