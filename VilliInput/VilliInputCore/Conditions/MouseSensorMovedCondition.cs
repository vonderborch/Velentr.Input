using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{

    public class MouseSensorMovedCondition : MouseBooleanCondition
    {

        public MouseSensorMovedCondition(MouseSensor sensor, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(boundaries, useRelativeCoordinates, parentBoundaries, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Sensor = sensor;
        }

        public MouseSensor Sensor { get; }

        public override VilliEventArguments GetArguments()
        {
            return new MouseSensorMovementEventArguments
            {
                Boundaries = Boundaries,
                Sensor = Sensor,
                Condition = this,
                InputSource = InputSource,
                MouseCoordinates = MouseService.CurrentCursorPosition,
                RelativeMouseCoordinates = Helper.ScalePointToChild(MouseService.CurrentCursorPosition, ParentBoundaries ?? Villi.Window.ClientBounds, Boundaries ?? Villi.Window.ClientBounds),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                CurrentValue = InternalGetValue()
            };
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeSensor(Sensor);
        }

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

        public override bool IsConsumed()
        {
            return Villi.System.Mouse.IsSensorConsumed(Sensor);
        }

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
