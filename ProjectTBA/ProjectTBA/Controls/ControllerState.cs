using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectTBA.Controls
{
    public static class ControllerState
    {

        public static LinkedList<Buttons> pressedButtons { get; set; }

        public enum Buttons
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            A,
            B
        }

        public static void Initialize()
        {
            pressedButtons = new LinkedList<Buttons>();
        }

        public static void Update(GameTime gt)
        {
            pressedButtons = new LinkedList<Buttons>();
        }

        public static Boolean IsButtonPressed(Buttons button)
        {
            foreach (Buttons type in pressedButtons)
            {
                if (button == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static Boolean IsButtonReleased(Buttons button)
        {
            foreach (Buttons type in pressedButtons)
            {
                if (button == type)
                {
                    return false;
                }
            }

            return true;
        }

        public static LinkedList<Buttons> GetPressedButtons()
        {
            return pressedButtons;
        }
    }
}
