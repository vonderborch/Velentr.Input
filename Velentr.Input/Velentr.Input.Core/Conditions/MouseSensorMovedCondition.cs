using Microsoft.Xna.Framework;
using Velentr.Input.Conditions.Internal;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when a sensor on the Mouse has moved.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.MouseBooleanCondition" />
    public class MouseSensorMovedCondition : MouseBooleanCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseSensorMovedCondition"/> class.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        public MouseSensorMovedCondition(MouseSensor sensor, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(boundaries, useRelativeCoordinates, parentBoundaries, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Sensor = sensor;
        }

        /// <summary>
        /// Gets the sensor.
        /// </summary>
        /// <value>
        /// The sensor.
        /// </value>
        public MouseSensor Sensor { get; }

        /// <summary>
        /// Gets the arguments to provide to events that are fired.
        /// </summary>
        /// <returns></returns>
        public override ConditionEventArguments GetArguments()
        {
            return new MouseSensorMovementEventArguments
            {
                Boundaries = Boundaries,
                Sensor = Sensor,
                Condition = this,
                InputSource = InputSource,
                MouseCoordinates = MouseService.CurrentCursorPosition,
                RelativeMouseCoordinates = Helper.ScalePointToChild(MouseService.CurrentCursorPosition, ParentBoundaries, Boundaries),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                CurrentValue = InternalGetValue()
            };
        }

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            VelentrInput.System.Mouse.ConsumeSensor(Sensor);
        }

        /// <summary>
        /// Currents the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool CurrentStateValid()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return MouseService.ScrolledHorizontally;
                case MouseSensor.Pointer:
                    return MouseService.CursorMoved;
                case MouseSensor.ScrollWheels:
                    return MouseService.Scrolled;
                case MouseSensor.VerticalScrollWheel:
                    return MouseService.ScrolledVertically;
            }

            return false;
        }

        /// <summary>
        /// Internals the get value.
        /// </summary>
        /// <returns></returns>
        protected override Value InternalGetValue()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return new Value(ValueType.Int, valueInt: MouseService.HorizontalScrollDelta);
                case MouseSensor.Pointer:
                    return new Value(ValueType.Int, valuePoint: MouseService.CursorPositionDelta);
                case MouseSensor.ScrollWheels:
                    return new Value(ValueType.Int, valuePoint: MouseService.ScrollDelta);
                case MouseSensor.VerticalScrollWheel:
                    return new Value(ValueType.Int, valueInt: MouseService.VerticalScrollDelta);
            }

            return new Value(ValueType.None);
        }

        /// <summary>
        /// Determines whether the input is consumed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the input is consumed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsConsumed()
        {
            return VelentrInput.System.Mouse.IsSensorConsumed(Sensor);
        }

        /// <summary>
        /// Previouses the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool PreviousStateValid()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return MouseService.ScrolledHorizontally;
                case MouseSensor.Pointer:
                    return MouseService.CursorMoved;
                case MouseSensor.ScrollWheels:
                    return MouseService.Scrolled;
                case MouseSensor.VerticalScrollWheel:
                    return MouseService.ScrolledVertically;
            }

            return false;
        }

    }

}
