using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Input
{
    public static class Keyboard
    {
        private static KeyboardState PreviousState { get; set; }
        private static KeyboardState CurrentState { get; set; }
        internal static void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }
        public static bool IsKeyDown(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }
        public static bool IsKeyUp(Keys key)
        {
            return CurrentState.IsKeyUp(key);
        }
        public static bool IsKeyPressed(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
        }
        public static bool IsKeyReleased(Keys key)
        {
            return CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
        }
    }
}
