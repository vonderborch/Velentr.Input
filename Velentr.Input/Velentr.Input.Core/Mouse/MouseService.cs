using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.Helpers;

namespace Velentr.Input.Mouse
{

    /// <summary>
    /// Defines what methods must be available at a minimum to support Mouse inputs
    /// </summary>
    /// <seealso cref="Velentr.Input.InputService" />
    public abstract class MouseService : InputService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        protected MouseService(InputManager inputManager) : base(inputManager)
        {
            Source = InputSource.Mouse;
        }

        public MouseEngine Engine { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether [reset mouse coords to center of screen].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [reset mouse coords to center of screen]; otherwise, <c>false</c>.
        /// </value>
        public bool ResetMouseCoordsToCenterOfScreen { get; set; } = false;

        /// <summary>
        /// Gets the current horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The current horizontal scroll wheel value.
        /// </value>
        public abstract int CurrentHorizontalScrollWheelValue { get; }

        /// <summary>
        /// Gets the previous horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The previous horizontal scroll wheel value.
        /// </value>
        public abstract int PreviousHorizontalScrollWheelValue { get; }

        /// <summary>
        /// Gets the current vertical scroll wheel value.
        /// </summary>
        /// <value>
        /// The current vertical scroll wheel value.
        /// </value>
        public abstract int CurrentVerticalScrollWheelValue { get; }

        /// <summary>
        /// Gets the previous vertical scroll wheel value.
        /// </summary>
        /// <value>
        /// The previous vertical scroll wheel value.
        /// </value>
        public abstract int PreviousVerticalScrollWheelValue { get; }

        /// <summary>
        /// Gets the horizontal scroll delta.
        /// </summary>
        /// <value>
        /// The horizontal scroll delta.
        /// </value>
        public abstract int HorizontalScrollDelta { get; }

        /// <summary>
        /// Gets the vertical scroll delta.
        /// </summary>
        /// <value>
        /// The vertical scroll delta.
        /// </value>
        public abstract int VerticalScrollDelta { get; }

        /// <summary>
        /// Gets the scroll delta.
        /// </summary>
        /// <value>
        /// The scroll delta.
        /// </value>
        public abstract Point ScrollDelta { get; }

        /// <summary>
        /// Gets the current scroll positions.
        /// </summary>
        /// <value>
        /// The current scroll positions.
        /// </value>
        public abstract Point CurrentScrollPositions { get; }

        /// <summary>
        /// Gets the previous scroll positions.
        /// </summary>
        /// <value>
        /// The previous scroll positions.
        /// </value>
        public abstract Point PreviousScrollPositions { get; }

        /// <summary>
        /// Gets the current cursor position.
        /// </summary>
        /// <value>
        /// The current cursor position.
        /// </value>
        public abstract Point CurrentCursorPosition { get; }

        /// <summary>
        /// Gets the previous cursor position.
        /// </summary>
        /// <value>
        /// The previous cursor position.
        /// </value>
        public abstract Point PreviousCursorPosition { get; }

        /// <summary>
        /// Gets the cursor position delta.
        /// </summary>
        /// <value>
        /// The cursor position delta.
        /// </value>
        public abstract Point CursorPositionDelta { get; }

        /// <summary>
        /// Gets a value indicating whether [scrolled vertically].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [scrolled vertically]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool ScrolledVertically { get; }

        /// <summary>
        /// Gets a value indicating whether [scrolled horizontally].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [scrolled horizontally]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool ScrolledHorizontally { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MouseService"/> is scrolled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if scrolled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool Scrolled { get; }

        /// <summary>
        /// Gets a value indicating whether [cursor moved].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cursor moved]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CursorMoved { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is mouse in window.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mouse in window; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsMouseInWindow { get; }

        /// <summary>
        /// Consumes the button.
        /// </summary>
        /// <param name="button">The button.</param>
        public abstract void ConsumeButton(MouseButton button);

        /// <summary>
        /// Consumes the sensor.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        public abstract void ConsumeSensor(MouseSensor sensor);

        /// <summary>
        /// Determines whether [is button consumed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if [is button consumed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsButtonConsumed(MouseButton button);

        /// <summary>
        /// Determines whether [is sensor consumed] [the specified sensor].
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <returns>
        ///   <c>true</c> if [is sensor consumed] [the specified sensor]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsSensorConsumed(MouseSensor sensor);

        /// <summary>
        /// Determines whether the specified button is pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button is pressed; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsPressed(MouseButton button);

        /// <summary>
        /// Determines whether the specified button was pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button was pressed; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasPressed(MouseButton button);

        /// <summary>
        /// Determines whether the specified button is released.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button is released; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsReleased(MouseButton button);

        /// <summary>
        /// Determines whether the specified button was released.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if the specified button was released; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasReleased(MouseButton button);

        /// <summary>
        /// Checks if the cursor is within the specified boundaries.
        /// </summary>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="scaleCoordinatesToArea">if set to <c>true</c> [scale coordinates to area].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <returns></returns>
        public bool CursorInBounds(Rectangle boundaries, bool scaleCoordinatesToArea = false, Rectangle? parentBoundaries = null)
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
