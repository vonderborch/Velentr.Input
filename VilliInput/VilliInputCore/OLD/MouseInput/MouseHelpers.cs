using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VilliInput.OLD.MouseInput
{
    public static class MouseHelpers
    {
        internal static Dictionary<MouseButton, Func<MouseState, Microsoft.Xna.Framework.Input.ButtonState>> ButtonMapping = new Dictionary<MouseButton, Func<MouseState, Microsoft.Xna.Framework.Input.ButtonState>>(Enum.GetNames(typeof(MouseButton)).Length)
        {
            {MouseButton.LeftButton, state => state.LeftButton},
            {MouseButton.MiddleButton, state => state.MiddleButton},
            {MouseButton.RightButton, state => state.RightButton},
            {MouseButton.XButton1, state => state.XButton1},
            {MouseButton.XButton2, state => state.XButton2},
        };

        /// <summary>
        /// Returns whether the button was just pressed for the first time
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is pressed, false otherwise</returns>
        public static bool Pressed(MouseButton button)
        {
            return IsPressed(button) && WasReleased(button);
        }

        /// <summary>
        /// Returns whether the button is still pressed between two frames
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is still pressed, false otherwise</returns>
        public static bool Held(MouseButton button)
        {
            return IsPressed(button) && WasPressed(button);
        }

        /// <summary>
        /// Returns whether the button was just released for the first time
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is released, false otherwise</returns>
        public static bool Released(MouseButton button)
        {
            return WasPressed(button) && IsReleased(button);
        }

        /// <summary>
        /// Returns whether the button is currently pressed
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is pressed, false otherwise</returns>
        public static bool IsPressed(MouseButton button)
        {
            return ButtonMapping[button](MouseService.CurrentState) == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        }

        /// <summary>
        /// Returns whether the button is currently released
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is released, false otherwise</returns>
        public static bool IsReleased(MouseButton button)
        {
            return ButtonMapping[button](MouseService.CurrentState) == Microsoft.Xna.Framework.Input.ButtonState.Released;
        }

        /// <summary>
        /// Returns whether the button was pressed
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is pressed, false otherwise</returns>
        public static bool WasPressed(MouseButton button)
        {
            return ButtonMapping[button](MouseService.PreviousState) == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        }

        /// <summary>
        /// Returns whether the button was released
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>True if the button is released, false otherwise</returns>
        public static bool WasReleased(MouseButton button)
        {
            return ButtonMapping[button](MouseService.PreviousState) == Microsoft.Xna.Framework.Input.ButtonState.Released;
        }

        public static int CurrentHorizontalScrollWheelValue => MouseService.CurrentState.HorizontalScrollWheelValue;

        public static int PreviousHorizontalScrollWheelValue => MouseService.PreviousState.HorizontalScrollWheelValue;

        public static int CurrentVerticalScrollWheelValue => MouseService.CurrentState.ScrollWheelValue;

        public static int PreviousVerticalScrollWheelValue => MouseService.PreviousState.ScrollWheelValue;

        /// <summary>
        /// Gets the vertical scroll delta.
        /// </summary>
        /// <value>
        /// The vertical scroll delta.
        /// </value>
        public static int VerticalScrollDelta => MouseService.CurrentState.ScrollWheelValue - MouseService.PreviousState.ScrollWheelValue;

        /// <summary>
        /// Gets the horizontal scroll delta.
        /// </summary>
        /// <value>
        /// The horizontal scroll delta.
        /// </value>
        public static int HorizontalScrollDelta => MouseService.CurrentState.HorizontalScrollWheelValue - MouseService.PreviousState.HorizontalScrollWheelValue;

        public static Point ScrollDelta => new Point(HorizontalScrollDelta, VerticalScrollDelta);

        public static Vector2 ScrollDeltaVector => ScrollDelta.ToVector2();

        public static Point CurrentCoordinates => MouseService.CurrentState.Position;

        public static Point PreviousCoordinates => MouseService.PreviousState.Position;

        public static Point PointerDelta => MouseService.CurrentState.Position - MouseService.PreviousState.Position;

        public static Vector2 PointerDeltaVector => PointerDelta.ToVector2();

        public static bool ScrolledVertically => VerticalScrollDelta != 0;

        public static bool ScrolledHorizontally => HorizontalScrollDelta != 0;

        public static bool Scrolled => ScrollDelta != Point.Zero;

        public static bool PointerMoved => PointerDelta != Point.Zero;

        public static bool IsMouseInWindow => Villi.IsWindowActive && Helpers.CoordinateInRectangle(CurrentCoordinates, Villi.Window.ClientBounds);

        public static bool PointerInBoundaries(Rectangle boundaries, bool scaleCoordinatesToArea = false, Rectangle? parentBoundaries = null)
        {
            return Helpers.CoordinateInRectangle(
                scaleCoordinatesToArea ?
                    Helpers.ScalePointToChild(CurrentCoordinates, parentBoundaries ?? Villi.Window.ClientBounds, boundaries)
                    : CurrentCoordinates,
                boundaries
            );
        }
    }
}
