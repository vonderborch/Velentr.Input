using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Helpers;

namespace Velentr.Input.Mouse
{

    /// <summary>
    /// The default Mouse Service implementation for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.Mouse.MouseService" />
    public class DefaultMouseService : MouseService
    {

        /// <summary>
        /// The button last consumed
        /// </summary>
        internal Dictionary<MouseButton, ulong> ButtonLastConsumed = new Dictionary<MouseButton, ulong>(Enum.GetNames(typeof(MouseButton)).Length);

        /// <summary>
        /// The sensor last consumed
        /// </summary>
        internal Dictionary<MouseSensor, ulong> SensorLastConsumed = new Dictionary<MouseSensor, ulong>(Enum.GetNames(typeof(MouseSensor)).Length);

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMouseService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        public DefaultMouseService(InputManager inputManager) : base(inputManager)
        {

        }

        /// <summary>
        /// Gets the current horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The current horizontal scroll wheel value.
        /// </value>
        public override int CurrentHorizontalScrollWheelValue => Engine.CurrentHorizontalScrollWheelValue;

        /// <summary>
        /// Gets the previous horizontal scroll wheel value.
        /// </summary>
        /// <value>
        /// The previous horizontal scroll wheel value.
        /// </value>
        public override int PreviousHorizontalScrollWheelValue => Engine.PreviousHorizontalScrollWheelValue;

        /// <summary>
        /// Gets the current vertical scroll wheel value.
        /// </summary>
        /// <value>
        /// The current vertical scroll wheel value.
        /// </value>
        public override int CurrentVerticalScrollWheelValue => Engine.CurrentVerticalScrollWheelValue;

        /// <summary>
        /// Gets the previous vertical scroll wheel value.
        /// </summary>
        /// <value>
        /// The previous vertical scroll wheel value.
        /// </value>
        public override int PreviousVerticalScrollWheelValue => Engine.PreviousVerticalScrollWheelValue;

        /// <summary>
        /// Gets the horizontal scroll delta.
        /// </summary>
        /// <value>
        /// The horizontal scroll delta.
        /// </value>
        public override int HorizontalScrollDelta => Engine.HorizontalScrollDelta;

        /// <summary>
        /// Gets the vertical scroll delta.
        /// </summary>
        /// <value>
        /// The vertical scroll delta.
        /// </value>
        public override int VerticalScrollDelta => Engine.VerticalScrollDelta;

        /// <summary>
        /// Gets the scroll delta.
        /// </summary>
        /// <value>
        /// The scroll delta.
        /// </value>
        public override Point ScrollDelta => Engine.ScrollDelta;

        /// <summary>
        /// Gets the current scroll positions.
        /// </summary>
        /// <value>
        /// The current scroll positions.
        /// </value>
        public override Point CurrentScrollPositions => Engine.CurrentScrollPositions;

        /// <summary>
        /// Gets the previous scroll positions.
        /// </summary>
        /// <value>
        /// The previous scroll positions.
        /// </value>
        public override Point PreviousScrollPositions => Engine.PreviousScrollPositions;

        /// <summary>
        /// Gets the current cursor position.
        /// </summary>
        /// <value>
        /// The current cursor position.
        /// </value>
        public override Point CurrentCursorPosition => Engine.CurrentCursorPosition;

        /// <summary>
        /// Gets the previous cursor position.
        /// </summary>
        /// <value>
        /// The previous cursor position.
        /// </value>
        public override Point PreviousCursorPosition => Engine.PreviousCursorPosition;

        /// <summary>
        /// Gets the cursor position delta.
        /// </summary>
        /// <value>
        /// The cursor position delta.
        /// </value>
        public override Point CursorPositionDelta => Engine.CursorPositionDelta;

        /// <summary>
        /// Gets a value indicating whether [scrolled vertically].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [scrolled vertically]; otherwise, <c>false</c>.
        /// </value>
        public override bool ScrolledVertically => Engine.ScrolledVertically;

        /// <summary>
        /// Gets a value indicating whether [scrolled horizontally].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [scrolled horizontally]; otherwise, <c>false</c>.
        /// </value>
        public override bool ScrolledHorizontally => Engine.ScrolledHorizontally;

        /// <summary>
        /// Gets a value indicating whether this <see cref="MouseService" /> is scrolled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if scrolled; otherwise, <c>false</c>.
        /// </value>
        public override bool Scrolled => Engine.Scrolled;

        /// <summary>
        /// Gets a value indicating whether [cursor moved].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cursor moved]; otherwise, <c>false</c>.
        /// </value>
        public override bool CursorMoved => Engine.CursorMoved;

        /// <summary>
        /// Gets a value indicating whether this instance is mouse in window.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is mouse in window; otherwise, <c>false</c>.
        /// </value>
        public override bool IsMouseInWindow => Engine.IsMouseInWindow;

        /// <summary>
        /// Sets up the input service.
        /// </summary>
        /// <param name="engine">The engine to setup the input service with.</param>
        protected override void SetupInternal(InputEngine engine)
        {
            Engine = (MouseEngine) engine;
        }

        /// <summary>
        /// Updates the input service.
        /// </summary>
        public override void Update()
        {
            Engine?.Update();
            if (ResetMouseCoordsToCenterOfScreen)
            {
                Engine?.SetMouseCoordinates(Manager.CenterCoordinates.X, Manager.CenterCoordinates.Y);
            }
        }

        /// <summary>
        /// Consumes the button.
        /// </summary>
        /// <param name="button">The button.</param>
        public override void ConsumeButton(MouseButton button)
        {
            ButtonLastConsumed[button] = Manager.CurrentFrame;
        }

        /// <summary>
        /// Consumes the sensor.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        public override void ConsumeSensor(MouseSensor sensor)
        {
            SensorLastConsumed[sensor] = Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is button consumed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>
        ///   <c>true</c> if [is button consumed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsButtonConsumed(MouseButton button)
        {
            if (ButtonLastConsumed.TryGetValue(button, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is sensor consumed] [the specified sensor].
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <returns>
        ///   <c>true</c> if [is sensor consumed] [the specified sensor]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsSensorConsumed(MouseSensor sensor)
        {
            if (SensorLastConsumed.TryGetValue(sensor, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
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
            return Engine.IsPressed(button);
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
            return Engine.WasPressed(button);
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
            return Engine.IsReleased(button);
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
            return Engine.WasReleased(button);
        }

    }

}
