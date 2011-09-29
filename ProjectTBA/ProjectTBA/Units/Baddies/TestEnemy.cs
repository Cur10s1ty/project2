using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectTBA.Misc;

namespace ProjectTBA.Units.Baddies
{
    public class TestEnemy : DefaultEnemy
    {
        public TestEnemy(float x, float y)
            : base(x, y, AkumaContentManager.testEnemyTex)
        {
        }
    }
}
