using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Tests;
using ProjectTBA.Levels;

namespace ProjectTBA.Views
{
    public class AkumaViewport
    {

        public Game1 game { get; set; }
        public Background bg { get; set; }
        public Vector2 screenSize { get; set; }

        public AkumaViewport(Level level)
        {
            game = Game1.GetInstance();

            switch (level.level)
            {
                case 1:
                    bg = new Background(Background.BGType.Forest);
                    break;

                case 2:
                    bg = new Background(Background.BGType.Forest);
                    break;

                case 3:
                    break;
            }

            level.offset = new Vector2(0, 0);
            screenSize = new Vector2(800, 480);
        }

        public void Update(GameTime gt)
        {
            
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            bg.Draw(gt, sb);
        }
    }
}
