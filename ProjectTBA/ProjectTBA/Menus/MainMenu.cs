using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectTBA.Misc;
using Microsoft.Xna.Framework.Input.Touch;

namespace ProjectTBA.Menus
{
    public class MainMenu
    {

        public Texture2D texture { get; set; }

        public MainMenu()
        {
            this.texture = AkumaContentManager.mainMenuTex;
        }

        public void Update(GameTime gt)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                foreach (TouchLocation tl in touchCollection)
                {
                    if (GetStartButtonRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed))
                    {
                        Game1.GetInstance().gameState = Game1.GameState.Ingame;
                        Game1.GetInstance().LoadIngameContent();
                    }
                }
            }
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(texture, new Rectangle(0, 0, 800, 480), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        public Rectangle GetStartButtonRectangle()
        {
            return new Rectangle(290, 295, 274, 66);
        }
    }
}
