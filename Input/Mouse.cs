using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Input
{
    public static class Mouse
    {
        private static MouseState PreviousState { get; set; }
        private static MouseState CurrentState { get; set; }
        public static int X
        {
            get { return CurrentState.X; }
            set
            {
                SetPosition(value, CurrentState.Y);
            }
        }
        public static int Y
        {
            get { return CurrentState.Y; }
            set
            {
                SetPosition(CurrentState.X, value);
            }
        }
        public static Point PositionDelta => CurrentState.Position - PreviousState.Position;
        public static int XDelta => CurrentState.X - PreviousState.X;
        public static int YDelta => CurrentState.Y - PreviousState.Y;
        public static bool WasMoved => PositionDelta != Point.Zero;
        public static int ScrollWheel => CurrentState.ScrollWheelValue;
        public static int ScrollWheelDelta => CurrentState.ScrollWheelValue - PreviousState.ScrollWheelValue;
        internal static void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }
        public static bool IsButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return CurrentState.LeftButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return CurrentState.MiddleButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return CurrentState.RightButton == ButtonState.Pressed;
                case MouseButton.XButton1:
                    return CurrentState.XButton1 == ButtonState.Pressed;
                case MouseButton.XButton2:
                    return CurrentState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }
        public static bool IsButtonUp(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return CurrentState.LeftButton == ButtonState.Released;
                case MouseButton.Middle:
                    return CurrentState.MiddleButton == ButtonState.Released;
                case MouseButton.Right:
                    return CurrentState.RightButton == ButtonState.Released;
                case MouseButton.XButton1:
                    return CurrentState.XButton1 == ButtonState.Released;
                case MouseButton.XButton2:
                    return CurrentState.XButton2 == ButtonState.Released;
                default:
                    return false;
            }
        }
        public static bool IsButtonPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return CurrentState.LeftButton == ButtonState.Pressed && PreviousState.LeftButton == ButtonState.Released;
                case MouseButton.Middle:
                    return CurrentState.MiddleButton == ButtonState.Pressed && PreviousState.MiddleButton == ButtonState.Released;
                case MouseButton.Right:
                    return CurrentState.RightButton == ButtonState.Pressed && PreviousState.RightButton == ButtonState.Released;
                case MouseButton.XButton1:
                    return CurrentState.XButton1 == ButtonState.Pressed && PreviousState.XButton1 == ButtonState.Released;
                case MouseButton.XButton2:
                    return CurrentState.XButton2 == ButtonState.Pressed && PreviousState.XButton2 == ButtonState.Released;
                default:
                    return false;
            }
        }
        public static bool IsButtonReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return CurrentState.LeftButton == ButtonState.Released && PreviousState.LeftButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return CurrentState.MiddleButton == ButtonState.Released && PreviousState.MiddleButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return CurrentState.RightButton == ButtonState.Released && PreviousState.RightButton == ButtonState.Pressed;
                case MouseButton.XButton1:
                    return CurrentState.XButton1 == ButtonState.Released && PreviousState.XButton1 == ButtonState.Pressed;
                case MouseButton.XButton2:
                    return CurrentState.XButton2 == ButtonState.Released && PreviousState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }
        public static void SetPosition(int x, int y)
        {
            Mouse.SetPosition(x, y);
            CurrentState = new MouseState(
                x,
                y,
                CurrentState.ScrollWheelValue,
                CurrentState.LeftButton,
                CurrentState.MiddleButton,
                CurrentState.RightButton,
                CurrentState.XButton1,
                CurrentState.XButton2
            );
        }
    }
    public enum MouseButton
    {
        Left,
        Middle,
        Right,
        XButton1,
        XButton2
    }
}
