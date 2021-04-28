using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Velentr.Input.Helpers;

namespace Velentr.Input.Mouse
{

    /// <summary>
    /// The default XNA-based Mouse Engine for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.Mouse.MouseEngine" />
    public class XnaMouseEngine : MouseEngine
    {

        /// <summary>
        /// The button mapping
        /// </summary>
        internal Dictionary<MouseButton, Func<MouseState, ButtonState>> ButtonMapping = new Dictionary<MouseButton, Func<MouseState, ButtonState>>(Enum.GetNames(typeof(MouseButton)).Length)
        {
            {MouseButton.LeftButton, state => state.LeftButton},
            {MouseButton.MiddleButton, state => state.MiddleButton},
            {MouseButton.RightButton, state => state.RightButton},
            {MouseButton.XButton1, state => state.XButton1},
            {MouseButton.XButton2, state => state.XButton2}
        };

        /// <summary>
        /// Gets the state of the previous.
        /// </summary>
        /// <value>
        /// The state of the previous.
        /// </value>
        public MouseState PreviousState { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public MouseState CurrentState { get; private set; }

        /// <summary>
        /// Sets up the InputEngine
        /// </summary>
        protected override void SetupInternal()
        {
            PreviousState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            CurrentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        /// <summary>
        /// Updates the InputEngine.
        /// </summary>
        public override void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }


#if MONOGAME
        /// <summary>
        /// Gets the current horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The current horizontal scroll wheel value.
        /// </value>
        public override int CurrentHorizontalScrollWheelValue => CurrentState.HorizontalScrollWheelValue;
        
        /// <summary>
        /// Gets the previous horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The previous horizontal scroll wheel value.
        /// </value>
        public override int PreviousHorizontalScrollWheelValue => PreviousState.HorizontalScrollWheelValue;
#else        
        /// <summary>
        /// Gets the current horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The current horizontal scroll wheel value.
        /// </value>
        public override int CurrentHorizontalScrollWheelValue => CurrentState.ScrollWheelValue;

        /// <summary>
        /// Gets the previous horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The previous horizontal scroll wheel value.
        /// </value>
        public override int PreviousHorizontalScrollWheelValue => PreviousState.ScrollWheelValue;
#endif

        /// <summary>
        /// Gets the current vertical scroll wheel value.
        /// </summary>
        /// <value>
        /// The current vertical scroll wheel value.
        /// </value>
        public override int CurrentVerticalScrollWheelValue => CurrentState.ScrollWheelValue;

        /// <summary>
        /// Gets the previous vertical scroll wheel value.
        /// </summary>
        /// <value>
        /// The previous vertical scroll wheel value.
        /// </value>
        public override int PreviousVerticalScrollWheelValue => PreviousState.ScrollWheelValue;

        /// <summary>
        /// Gets the horizontal scroll delta.
        /// </summary>
        /// <value>
        /// The horizontal scroll delta.
        /// </value>
        public override int HorizontalScrollDelta => CurrentHorizontalScrollWheelValue - PreviousHorizontalScrollWheelValue;

        /// <summary>
        /// Gets the vertical scroll delta.
        /// </summary>
        /// <value>
        /// The vertical scroll delta.
        /// </value>
        public override int VerticalScrollDelta => CurrentVerticalScrollWheelValue - PreviousVerticalScrollWheelValue;

        /// <summary>
        /// Gets the scroll delta.
        /// </summary>
        /// <value>
        /// The scroll delta.
        /// </value>
        public override Point ScrollDelta => new Point(HorizontalScrollDelta, VerticalScrollDelta);

        /// <summary>
        /// Gets the current scroll positions.
        /// </summary>
        /// <value>
        /// The current scroll positions.
        /// </value>
        public override Point CurrentScrollPositions => new Point(CurrentHorizontalScrollWheelValue, CurrentVerticalScrollWheelValue);

        /// <summary>
        /// Gets the previous scroll positions.
        /// </summary>
        /// <value>
        /// The previous scroll positions.
        /// </value>
        public override Point PreviousScrollPositions => new Point(PreviousHorizontalScrollWheelValue, PreviousVerticalScrollWheelValue);

#if MONOGAME
        /// <summary>
        /// Gets the current cursor position.
        /// </summary>
        /// <value>
        /// The current cursor position.
        /// </value>
        public override Point CurrentCursorPosition => CurrentState.Position;
        
        /// <summary>
        /// Gets the previous cursor position.
        /// </summary>
        /// <value>
        /// The previous cursor position.
        /// </value>
        public override Point PreviousCursorPosition => PreviousState.Position;
#else        
        /// <summary>
        /// Gets the current cursor position.
        /// </summary>
        /// <value>
        /// The current cursor position.
        /// </value>
        public override Point CurrentCursorPosition => new Point(CurrentState.X, CurrentState.Y);

        /// <summary>
        /// Gets the previous cursor position.
        /// </summary>
        /// <value>
        /// The previous cursor position.
        /// </value>
        public override Point PreviousCursorPosition => new Point(PreviousState.X, PreviousState.Y);
#endif

        /// <summary>
        /// Gets the cursor position delta.
        /// </summary>
        /// <value>
        /// The cursor position delta.
        /// </value>
        public override Point CursorPositionDelta => CurrentCursorPosition - PreviousCursorPosition;

        /// <summary>
        /// Gets a value indicating whether [scrolled vertically].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [scrolled vertically]; otherwise, <c>false</c>.
        /// </value>
        public override bool ScrolledVertically => VerticalScrollDelta != 0;

        /// <summary>
        /// Gets a value indicating whether [scrolled horizontally].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [scrolled horizontally]; otherwise, <c>false</c>.
        /// </value>
        public override bool ScrolledHorizontally => HorizontalScrollDelta != 0;

        /// <summary>
        /// Gets a value indicating whether this <see cref="MouseEngine" /> is scrolled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if scrolled; otherwise, <c>false</c>.
        /// </value>
        public override bool Scrolled => ScrolledVertically || ScrolledHorizontally;

        /// <summary>
        /// Gets a value indicating whether [cursor moved].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cursor moved]; otherwise, <c>false</c>.
        /// </value>
        public override bool CursorMoved => CursorPositionDelta != Point.Zero;

        /// <summary>
        /// Gets a value indicating whether this instance is mouse in window.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mouse in window; otherwise, <c>false</c>.
        /// </value>
        public override bool IsMouseInWindow => Manager.IsWindowActive && Helper.CoordinateInRectangle(CurrentCursorPosition, Manager.Window.ClientBounds);

        /// <summary>
        /// Sets the mouse coordinates.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void SetMouseCoordinates(int x, int y)
        {
            Microsoft.Xna.Framework.Input.Mouse.SetPosition(Manager.CenterCoordinates.X, Manager.CenterCoordinates.Y);
        }

        /// <summary>
        /// Determines whether the specified button is pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button is pressed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsPressed(MouseButton button)
        {
            return ButtonMapping[button](CurrentState) == ButtonState.Pressed;
        }

        /// <summary>
        /// Determines whether the specified button was pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button was pressed; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasPressed(MouseButton button)
        {
            return ButtonMapping[button](PreviousState) == ButtonState.Pressed;
        }

        /// <summary>
        /// Determines whether the specified button is released.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button is released; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsReleased(MouseButton button)
        {
            return ButtonMapping[button](CurrentState) == ButtonState.Released;
        }

        /// <summary>
        /// Determines whether the specified button was released.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button was released; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasReleased(MouseButton button)
        {
            return ButtonMapping[button](PreviousState) == ButtonState.Released;
        }

        /// <summary>
        /// Cursors the in bounds.
        /// </summary>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="scaleCoordinatesToArea">if set to <c>true</c> [scale coordinates to area].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <returns></returns>
        public override bool CursorInBounds(Rectangle boundaries, bool scaleCoordinatesToArea = false, Rectangle? parentBoundaries = null)
        {
            return Helper.CoordinateInRectangle(
                scaleCoordinatesToArea
                    ? Helper.ScalePointToChild(CurrentCursorPosition, parentBoundaries ?? Manager.Window.ClientBounds, boundaries)
                    : CurrentCursorPosition,
                boundaries
            );
        }
    }
}
