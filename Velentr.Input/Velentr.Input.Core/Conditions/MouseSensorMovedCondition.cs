using Microsoft.Xna.Framework;
using Velentr.Input.Conditions.Internal;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;
using ValueType = Velentr.Input.Enums.ValueType;

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
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        public MouseSensorMovedCondition(InputManager manager, MouseSensor sensor, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, boundaries, useRelativeCoordinates, parentBoundaries, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
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
                MouseCoordinates = Manager.Mouse.CurrentCursorPosition,
                RelativeMouseCoordinates = Helper.ScalePointToChild(Manager.Mouse.CurrentCursorPosition, ParentBoundaries, Boundaries),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                CurrentValue = InternalGetValue()
            };
        }

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            Manager.Mouse.ConsumeSensor(Sensor);
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
                    return Manager.Mouse.ScrolledHorizontally;
                case MouseSensor.Pointer:
                    return Manager.Mouse.CursorMoved;
                case MouseSensor.ScrollWheels:
                    return Manager.Mouse.Scrolled;
                case MouseSensor.VerticalScrollWheel:
                    return Manager.Mouse.ScrolledVertically;
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
                    return new Value(ValueType.Int, valueInt: Manager.Mouse.HorizontalScrollDelta);
                case MouseSensor.Pointer:
                    return new Value(ValueType.Int, valuePoint: Manager.Mouse.CursorPositionDelta);
                case MouseSensor.ScrollWheels:
                    return new Value(ValueType.Int, valuePoint: Manager.Mouse.ScrollDelta);
                case MouseSensor.VerticalScrollWheel:
                    return new Value(ValueType.Int, valueInt: Manager.Mouse.VerticalScrollDelta);
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
            return Manager.Mouse.IsSensorConsumed(Sensor);
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
                    return Manager.Mouse.ScrolledHorizontally;
                case MouseSensor.Pointer:
                    return Manager.Mouse.CursorMoved;
                case MouseSensor.ScrollWheels:
                    return Manager.Mouse.Scrolled;
                case MouseSensor.VerticalScrollWheel:
                    return Manager.Mouse.ScrolledVertically;
            }

            return false;
        }

    }

}
