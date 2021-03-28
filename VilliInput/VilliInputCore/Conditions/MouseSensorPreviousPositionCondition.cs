using System;
using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{
    public class MouseSensorPreviousPositionCondition : LogicCondition
    {

        public MouseSensor Sensor { get; private set; }

        private readonly Rectangle? _parentBoundaries;

        public Rectangle? Boundaries { get; private set; }

        public bool UseRelativeCoordinates { get; private set; }

        public Rectangle? ParentBoundaries => _parentBoundaries ?? Villi.Window.ClientBounds;

        public MouseSensorPreviousPositionCondition(MouseSensor sensor, ValueLogic logicValue, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(InputSource.Mouse, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Sensor = sensor;

            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
            _parentBoundaries = parentBoundaries;

            logicValue.Value.Validate();
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    if (logicValue.Value.Type != Enums.ValueType.Int)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.HorizontalScrollWheel, you must use a ValueType.Int!");
                    }

                    break;
                case MouseSensor.Pointer:
                    if (logicValue.Value.Type != Enums.ValueType.Point)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.Pointer, you must use a ValueType.Point!");
                    }

                    break;
                case MouseSensor.ScrollWheels:
                    if (logicValue.Value.Type != Enums.ValueType.Point)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.ScrollWheels, you must use a ValueType.Point!");
                    }

                    break;
                case MouseSensor.VerticalScrollWheel:
                    if (logicValue.Value.Type != Enums.ValueType.Int)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.VerticalScrollWheel, you must use a ValueType.Int!");
                    }

                    break;
            }
        }

        protected override Value InternalGetValue()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return new Value(Enums.ValueType.Int, valueInt: MouseService.PreviousHorizontalScrollWheelValue);
                case MouseSensor.Pointer:
                    return new Value(Enums.ValueType.Int, valuePoint: MouseService.PreviousCursorPosition);
                case MouseSensor.ScrollWheels:
                    return new Value(Enums.ValueType.Int, valuePoint: MouseService.PreviousScrollPositions);
                case MouseSensor.VerticalScrollWheel:
                    return new Value(Enums.ValueType.Int, valueInt: MouseService.PreviousVerticalScrollWheelValue);
            }

            return new Value(Enums.ValueType.None);
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeSensor(Sensor);
        }

        public override bool IsConsumed()
        {
            return Villi.System.Mouse.IsSensorConsumed(Sensor);
        }

        public override VilliEventArguments GetArguments()
        {
            return new MouseSensorMovementEventArguments()
            {
                Boundaries = this.Boundaries,
                Sensor = this.Sensor,
                Condition = this,
                InputSource = this.InputSource,
                MouseCoordinates = MouseService.CurrentCursorPosition,
                RelativeMouseCoordinates = Helper.ScalePointToChild(MouseService.CurrentCursorPosition, ParentBoundaries ?? Villi.Window.ClientBounds, Boundaries ?? Villi.Window.ClientBounds),
                MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                UseRelativeCoordinates = this.UseRelativeCoordinates,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
                CurrentValue = this.InternalGetValue(),
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || (Villi.IsWindowActive && MouseService.IsMouseInWindow))
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet)
                   && (Boundaries == null || MouseService.CursorInBounds((Rectangle)Boundaries, UseRelativeCoordinates, ParentBoundaries));
        }

    }
}
