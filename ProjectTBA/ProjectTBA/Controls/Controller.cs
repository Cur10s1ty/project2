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
        public Texture2D buttonLeftTex { get; set; }
        public Texture2D buttonRightTex { get; set; }
        public Texture2D buttonATex { get; set; }
        public Texture2D buttonBTex { get; set; }
        public Boolean hasSwiped { get; set; }

        public Controller()
        {
            ControllerState.Initialize();

            this.game = Game1.GetInstance();
            this.buttonLeftTex = AkumaContentManager.buttonLeftTex;
            this.buttonRightTex = AkumaContentManager.buttonRightTex;
            this.buttonATex = AkumaContentManager.buttonATex;
            this.buttonBTex = AkumaContentManager.buttonBTex;
        }

        public void Update(GameTime gt)
        {
            ControllerState.Update(gt);
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                foreach (TouchLocation tl in touchCollection)
                {
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

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                switch (gesture.GestureType)
                {
                    case GestureType.HorizontalDrag:
                        if (gesture.Position.X - gesture.Position2.X > 400 && !hasSwiped && (!game.player.jumping && !game.player.isWalking))
                        {
                            game.player.ResetTongue();
                            hasSwiped = true;
                            game.player.isTongueActive = true;
                        }
                        break;
                    default: 
                        break;
                }
            }
        }

        internal void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Draw(buttonLeftTex, GetButtonLeftRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            sb.Draw(buttonRightTex, GetButtonRightRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f); 
            sb.Draw(buttonATex, GetButtonARectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            sb.Draw(buttonBTex, GetButtonBRectangle(), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        public Rectangle GetButtonLeftRectangle()
        {
            return new Rectangle(5, 200, 80, 80);
        }

        public Rectangle GetButtonRightRectangle()
        {
            return new Rectangle(715, 200, 80, 80);
        }

        public Rectangle GetButtonBRectangle()
        {
            return new Rectangle(305, 395, 80, 80);
        }

        public Rectangle GetButtonARectangle()
        {
            return new Rectangle(415, 395, 80, 80);
        }
    }
}
