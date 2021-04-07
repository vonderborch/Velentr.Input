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
    /// An input condition that is valid when the Previous Position of a Mouse sensor meets a certain condition specified by the logicValue parameter.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.LogicCondition" />
    public class MouseSensorPreviousPositionCondition : LogicCondition
    {
        /// <summary>
        /// The parent boundaries
        /// </summary>
        private readonly Rectangle? _parentBoundaries;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseSensorPreviousPositionCondition"/> class.
        /// </summary>
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="logicValue">The logic value.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        /// <exception cref="System.Exception">
        /// logicValue contains an invalid type for MouseSensor.HorizontalScrollWheel, you must use a ValueType.Int!
        /// or
        /// logicValue contains an invalid type for MouseSensor.Pointer, you must use a ValueType.Point!
        /// or
        /// logicValue contains an invalid type for MouseSensor.ScrollWheels, you must use a ValueType.Point!
        /// or
        /// logicValue contains an invalid type for MouseSensor.VerticalScrollWheel, you must use a ValueType.Int!
        /// </exception>
        public MouseSensorPreviousPositionCondition(InputManager manager, MouseSensor sensor, ValueLogic logicValue, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.Mouse, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
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

            if (manager.Settings.ThrowWhenCreatingConditionIfNoServiceEnabled && manager.Mouse == null)
            {
                throw new Exception(Constants.NoEngineConfiguredError);
            }
        }

        /// <summary>
        /// Gets the sensor.
        /// </summary>
        /// <value>
        /// The sensor.
        /// </value>
        public MouseSensor Sensor { get; }

        /// <summary>
        /// Gets the boundaries.
        /// </summary>
        /// <value>
        /// The boundaries.
        /// </value>
        public Rectangle? Boundaries { get; }

        /// <summary>
        /// Gets a value indicating whether [use relative coordinates].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use relative coordinates]; otherwise, <c>false</c>.
        /// </value>
        public bool UseRelativeCoordinates { get; }

        /// <summary>
        /// Gets the parent boundaries.
        /// </summary>
        /// <value>
        /// The parent boundaries.
        /// </value>
        public Rectangle? ParentBoundaries => _parentBoundaries ?? Manager.Window.ClientBounds;

        /// <summary>
        /// Internals the get value.
        /// </summary>
        /// <returns></returns>
        protected override Value InternalGetValue()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return new Value(ValueType.Int, valueInt: Manager.Mouse.PreviousHorizontalScrollWheelValue);
                case MouseSensor.Pointer:
                    return new Value(ValueType.Int, valuePoint: Manager.Mouse.PreviousCursorPosition);
                case MouseSensor.ScrollWheels:
                    return new Value(ValueType.Int, valuePoint: Manager.Mouse.PreviousScrollPositions);
                case MouseSensor.VerticalScrollWheel:
                    return new Value(ValueType.Int, valueInt: Manager.Mouse.PreviousVerticalScrollWheelValue);
            }

            return new Value(ValueType.None);
        }

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            Manager.Mouse.ConsumeSensor(Sensor);
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
                RelativeMouseCoordinates = Helper.ScalePointToChild(Manager.Mouse.CurrentCursorPosition, ParentBoundaries ?? Manager.Window.ClientBounds, Boundaries ?? Manager.Window.ClientBounds),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
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
            return (!WindowMustBeActive || Manager.IsWindowActive && Manager.Mouse.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime) >= milliSecondsForConditionMet)
                   && (MilliSecondsForTimeOut == 0 || Helper.ElapsedMilliSeconds(LastFireTime, Manager.CurrentTime) >= MilliSecondsForTimeOut)
                   && (Boundaries == null || Manager.Mouse.CursorInBounds((Rectangle) Boundaries, UseRelativeCoordinates, ParentBoundaries));
        }

    }

}
