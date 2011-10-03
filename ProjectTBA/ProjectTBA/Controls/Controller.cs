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
            this.dPadTex = AkumaContentManager.dPadTex;
            this.buttonTex = AkumaContentManager.buttonTex;
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
            return new Rectangle(dPad.X + 85, dPad.Y + 5, 70, 70);
        }

        public Rectangle GetButtonDownRectangle()
        {
            Rectangle dPad = GetDPadRectangle();
            return new Rectangle(dPad.X + 85, dPad.Y + 165, 70, 70);
        }

        public Rectangle GetButtonLeftRectangle()
        {
            Rectangle dPad = GetDPadRectangle();
            return new Rectangle(dPad.X + 5, dPad.Y + 85, 70, 70);
        }

        public Rectangle GetButtonRightRectangle()
        {
            Rectangle dPad = GetDPadRectangle();
            return new Rectangle(dPad.X + 165, dPad.Y + 85, 70, 70);
        }

        public Rectangle GetButtonARectangle()
        {
            Rectangle buttons = GetActionButtonRectangle();
            return new Rectangle(buttons.X + 154, buttons.Y + 65, 70, 70);
        }

        public Rectangle GetButtonBRectangle()
        {
            Rectangle buttons = GetActionButtonRectangle();
            return new Rectangle(buttons.X + 16, buttons.Y + 105, 70, 70);
        }
    }
}
