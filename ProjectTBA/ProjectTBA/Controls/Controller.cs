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

        public Controller()
        {
            ControllerState.Initialize();

            this.game = Game1.GetInstance();
            this.dPadTex = TBAContentManager.dPadTex;
            this.buttonTex = TBAContentManager.buttonTex;
        }

        public void Update(GameTime gt)
        {
            ControllerState.Update(gt);
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                foreach (TouchLocation tl in touchCollection)
                {
                    if (GetButtonUpRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        ControllerState.pressedButtons.AddLast(ControllerState.Buttons.UP);
                    }
                    if (GetButtonDownRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        ControllerState.pressedButtons.AddLast(ControllerState.Buttons.DOWN);
                    }
                    if (GetButtonLeftRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        ControllerState.pressedButtons.AddLast(ControllerState.Buttons.LEFT);
                    }
                    if (GetButtonRightRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        ControllerState.pressedButtons.AddLast(ControllerState.Buttons.RIGHT);
                    }
                    if (GetButtonARectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        ControllerState.pressedButtons.AddLast(ControllerState.Buttons.A);
                    }
                    if (GetButtonBRectangle().Contains((int)tl.Position.X, (int)tl.Position.Y) && (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved))
                    {
                        ControllerState.pressedButtons.AddLast(ControllerState.Buttons.B);
                    }
                }
            }
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(dPadTex, GetDPadRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            sb.Draw(buttonTex, GetActionButtonRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
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
            return new Rectangle(buttons.X + 106, buttons.Y + 36, 40, 40);
        }

        public Rectangle GetButtonBRectangle()
        {
            Rectangle buttons = GetActionButtonRectangle();
            return new Rectangle(buttons.X + 13, buttons.Y + 74, 40, 40);
        }
    }
}
