using System;
using Microsoft.Xna.Framework;
using Velentr.Input.Conditions.Internal;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;
using ValueType = Velentr.Input.Enums.ValueType;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when the Delta of a Mouse sensor meets a certain condition specified by the logicValue parameter.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.LogicCondition" />
    public class MouseSensorDeltaCondition : LogicCondition
    {
        /// <summary>
        /// The parent boundaries
        /// </summary>
        private readonly Rectangle? _parentBoundaries;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseSensorDeltaCondition"/> class.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="logicValue">The logic value.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <exception cref="System.Exception">
        /// logicValue contains an invalid type for MouseSensor.HorizontalScrollWheel, you must use a ValueType.Int!
        /// or
        /// logicValue contains an invalid type for MouseSensor.Pointer, you must use a ValueType.Point!
        /// or
        /// logicValue contains an invalid type for MouseSensor.ScrollWheels, you must use a ValueType.Point!
        /// or
        /// logicValue contains an invalid type for MouseSensor.VerticalScrollWheel, you must use a ValueType.Int!
        /// </exception>
        public MouseSensorDeltaCondition(MouseSensor sensor, ValueLogic logicValue, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(InputSource.Mouse, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Sensor = sensor;

            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
            _parentBoundaries = parentBoundaries;

            logicValue.Value.Validate();
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    if (logicValue.Value.Type != ValueType.Int)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.HorizontalScrollWheel, you must use a ValueType.Int!");
                    }

                    break;
                case MouseSensor.Pointer:
                    if (logicValue.Value.Type != ValueType.Point)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.Pointer, you must use a ValueType.Point!");
                    }

                    break;
                case MouseSensor.ScrollWheels:
                    if (logicValue.Value.Type != ValueType.Point)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.ScrollWheels, you must use a ValueType.Point!");
                    }

                    break;
                case MouseSensor.VerticalScrollWheel:
                    if (logicValue.Value.Type != ValueType.Int)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.VerticalScrollWheel, you must use a ValueType.Int!");
                    }

                    break;
            }
        }

        /// <summary>
        /// Gets the sensor.
        /// </summary>
        /// <value>
        /// The sensor.
        /// </value>
        public MouseSensor Sensor { get; }

        public Rectangle? Boundaries { get; }

        public bool UseRelativeCoordinates { get; }

        public Rectangle? ParentBoundaries => _parentBoundaries ?? VelentrInput.Window.ClientBounds;

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
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            VelentrInput.System.Mouse.ConsumeSensor(Sensor);
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
                RelativeMouseCoordinates = Helper.ScalePointToChild(MouseService.CurrentCursorPosition, ParentBoundaries ?? VelentrInput.Window.ClientBounds, Boundaries ?? VelentrInput.Window.ClientBounds),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                CurrentValue = InternalGetValue()
            };
        }

        /// <summary>
        /// Checks to see if the input is valid.
        /// </summary>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns></returns>
        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || VelentrInput.IsWindowActive && MouseService.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime) >= milliSecondsForConditionMet)
                   && (Boundaries == null || MouseService.CursorInBounds((Rectangle) Boundaries, UseRelativeCoordinates, ParentBoundaries));
        }

    }

}
