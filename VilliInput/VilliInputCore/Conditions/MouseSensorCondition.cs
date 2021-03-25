using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.MouseInput;

namespace VilliInput.Conditions
{
    public class MouseSensorCondition : SensorCondition
    {
        public MouseSensor Sensor { get; private set; }

        public Rectangle? Boundaries { get; private set; }

        public bool UseRelativeCoordinates { get; private set; }

        public MouseSensorCondition(bool windowMustBeActive = true, Rectangle? boundaries = null, bool useRelativeCoordinates = false, uint secondsForPressed = 0, uint secondsForReleased = 0, InputValueLogic? inputValueComparator = null) : base(InputSource.Mouse, windowMustBeActive, secondsForPressed, secondsForReleased, inputValueComparator)
        {
            Sensor = MouseSensor.HorizontalScrollWheel;

            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
        }

        protected override bool ActionValid(bool ignoredConsumed, uint actionTime)
        {
            return (!WindowMustBeActive || (Villi.IsWindowActive && MouseHelpers.IsMouseInWindow))
                   && (ignoredConsumed || Villi.System.Mouse.IsSensorConsumed(Sensor))
                   && (actionTime == 0 || Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime) >= actionTime)
                   && (Boundaries == null || MouseHelpers.PointerInBoundaries((Rectangle)Boundaries, UseRelativeCoordinates, Villi.Window.ClientBounds));
        }

        public override InputValue GetInputValue()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return new InputValue(valueInt: MouseHelpers.HorizontalScrollDelta);
                case MouseSensor.VerticalScrollWheel:
                    return new InputValue(valueInt: MouseHelpers.VerticalScrollDelta);
                case MouseSensor.Pointer:
                    return new InputValue(valuePoint: MouseHelpers.PointerDelta, valueVector2: MouseHelpers.PointerDeltaVector);
            }

            return new InputValue();
        }

        internal override VilliEventArguments GetArguments()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                case MouseSensor.VerticalScrollWheel:
                    var delta = Sensor == MouseSensor.HorizontalScrollWheel
                        ? MouseHelpers.HorizontalScrollDelta
                        : MouseHelpers.VerticalScrollDelta;
                    var value = Sensor == MouseSensor.HorizontalScrollWheel
                        ? MouseHelpers.CurrentHorizontalScrollWheelValue
                        : MouseHelpers.CurrentVerticalScrollWheelValue;

                    return new MouseScrollWheelDeltaEventArguments()
                    {
                        Boundaries = this.Boundaries,
                        Sensor = this.Sensor,
                        ConditionSource = this,
                        InputSource = this.Source,
                        MouseCoordinates = MouseHelpers.CurrentCoordinates,
                        RelativeMouseCoordinates = Helpers.ScalePointToChild(MouseHelpers.CurrentCoordinates, Villi.Window.ClientBounds, Boundaries ?? Villi.Window.ClientBounds),
                        SecondsForPressed = this.SecondsForPressed,
                        SecondsForReleased = this.SecondsForReleased,
                        UseRelativeCoordinates = this.UseRelativeCoordinates,
                        ConditionState = this.CurrentState,
                        ConditionStateStartTime = this.CurrentStateStart,
                        ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                        ScrollDelta = delta,
                        ScrollValue = value,
                        WindowMustBeActive = this.WindowMustBeActive,
                    };
                case MouseSensor.Pointer:
                    return new MousePointerDeltaEventArguments()
                    {
                        Boundaries = this.Boundaries,
                        Sensor = this.Sensor,
                        ConditionSource = this,
                        InputSource = this.Source,
                        MouseCoordinates = MouseHelpers.CurrentCoordinates,
                        RelativeMouseCoordinates = Helpers.ScalePointToChild(MouseHelpers.CurrentCoordinates, Villi.Window.ClientBounds, Boundaries ?? Villi.Window.ClientBounds),
                        SecondsForPressed = this.SecondsForPressed,
                        SecondsForReleased = this.SecondsForReleased,
                        UseRelativeCoordinates = this.UseRelativeCoordinates,
                        ConditionState = this.CurrentState,
                        ConditionStateStartTime = this.CurrentStateStart,
                        ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                        Delta = MouseHelpers.PointerDelta,
                        DeltaVector = MouseHelpers.PointerDeltaVector,
                        WindowMustBeActive = this.WindowMustBeActive,
                    };
            }

            return null;
        }

        public override bool IsPressed()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return MouseHelpers.ScrolledHorizontally;
                case MouseSensor.VerticalScrollWheel:
                    return MouseHelpers.ScrolledVertically;
                case MouseSensor.Pointer:
                    return MouseHelpers.PointerMoved;
            }

            return false;
        }

        public override bool IsReleased()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return !MouseHelpers.ScrolledHorizontally;
                case MouseSensor.VerticalScrollWheel:
                    return !MouseHelpers.ScrolledVertically;
                case MouseSensor.Pointer:
                    return !MouseHelpers.PointerMoved;
            }

            return false;
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeSensor(Sensor);
        }
    }
}
