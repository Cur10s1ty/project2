using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ProjectTBA.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace ProjectTBA.Controls
{
    public class Controller
    {

        public Game1 game { get; set; }
        public Texture2D dPadTex { get; set; }
        public Texture2D buttonTex { get; set; }

        public SpriteFont testFont { get; set; }
        public String text { get; set; }

        public Controller()
        {
            this.game = Game1.GetInstance();
            this.dPadTex = TBAContentManager.dPadTex;
            this.buttonTex = TBAContentManager.buttonTex;

            this.testFont = TBAContentManager.testFont;
            this.text = "No Input Detected\r\n";
        }

        public void Update(GameTime gt)
        {
            text = "";
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                foreach (TouchLocation tl in touchCollection)
                {
                    if (GetButtonUpRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        text += "Detected: UP\r\n";
                    }
                    if (GetButtonDownRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        text += "Detected: DOWN\r\n";
                    }
                    if (GetButtonLeftRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        text += "Detected: LEFT\r\n";
                    }
                    if (GetButtonRightRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        text += "Detected: RIGHT\r\n";
                    }
                    if (GetButtonARectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && tl.State == TouchLocationState.Pressed)
                    {
                        text += "Detected: A\r\n";
                    }
                    if (GetButtonBRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && tl.State == TouchLocationState.Pressed)
                    {
                        text += "Detected: B\r\n";
                    }
                    if (text == "")
                    {
                        text += "No Input Detected\r\n";
                    }
                }
            }
            else
            {
                text += "No Input Detected\r\n";
            }
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(dPadTex, GetDPadRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            sb.Draw(buttonTex, GetActionButtonRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            //sb.Draw(TBAContentManager.solidTex, GetButtonUpRectangle(), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            //sb.Draw(TBAContentManager.solidTex, GetButtonDownRectangle(), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            //sb.Draw(TBAContentManager.solidTex, GetButtonLeftRectangle(), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            //sb.Draw(TBAContentManager.solidTex, GetButtonRightRectangle(), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

            //sb.Draw(TBAContentManager.solidTex, GetButtonARectangle(), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
            //sb.Draw(TBAContentManager.solidTex, GetButtonBRectangle(), null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

            Vector2 size = testFont.MeasureString(text);
            sb.DrawString(testFont, text, new Vector2((game.graphics.PreferredBackBufferWidth / 2) - (size.X / 2), (game.graphics.PreferredBackBufferHeight / 2) - (size.Y / 2)), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public Rectangle GetDPadRectangle()
        {
            return new Rectangle(0, game.graphics.PreferredBackBufferHeight - dPadTex.Height, dPadTex.Width, dPadTex.Height);
        }

        public Rectangle GetActionButtonRectangle()
        {
            return new Rectangle(game.graphics.PreferredBackBufferWidth - buttonTex.Width, game.graphics.PreferredBackBufferHeight - buttonTex.Height, buttonTex.Width, buttonTex.Height);
        }

        public Rectangle GetButtonUpRectangle()
        {
            Rectangle dPad = GetDPadRectangle();
            return new Rectangle(dPad.X + 60, dPad.Y + 2, 40, 40);
        }

        public Rectangle GetButtonDownRectangle()
        {
            Rectangle dPad = GetDPadRectangle();
            return new Rectangle(dPad.X + 60, dPad.Y + 118, 40, 40);
        }

        public Rectangle GetButtonLeftRectangle()
        {
            Rectangle dPad = GetDPadRectangle();
            return new Rectangle(dPad.X + 2, dPad.Y + 60, 40, 40);
        }

        public Rectangle GetButtonRightRectangle()
        {
            Rectangle dPad = GetDPadRectangle();
            return new Rectangle(dPad.X + 118, dPad.Y + 60, 40, 40);
        }

        public Rectangle GetButtonARectangle()
        {
            Rectangle buttons = GetActionButtonRectangle();
            return new Rectangle(buttons.X + 13, buttons.Y + 74, 40, 40);
        }

        public Rectangle GetButtonBRectangle()
        {
            Rectangle buttons = GetActionButtonRectangle();
            return new Rectangle(buttons.X + 106, buttons.Y + 36, 40, 40);
        }
    }
}
