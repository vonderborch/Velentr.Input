using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.MouseInput;

namespace VilliInput.Conditions
{
    public class MouseHorizontalScrollWheelDeltaCondition : SensorCondition
    {
        public MouseSensor Sensor { get; private set; }

        public Rectangle? Boundaries { get; private set; }

        public bool UseRelativeCoordinates { get; private set; }

        public MouseHorizontalScrollWheelDeltaCondition(bool windowMustBeActive = true, Rectangle? boundaries = null, bool useRelativeCoordinates = false, uint secondsForPressed = 0, uint secondsForReleased = 0, InputValueLogic? inputValueComparator = null) : base(secondsForPressed, secondsForReleased, windowMustBeActive, inputValueComparator)
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
            return new InputValue(null, MouseHelpers.HorizontalScrollDelta);
        }

        internal override VilliEventArguments GetArguments()
        {
            return new MouseHorizontalScrollWheelDeltaEventArguments()
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
                ScrollDelta = MouseHelpers.HorizontalScrollDelta,
                ScrollValue = MouseHelpers.CurrentHorizontalScrollWheelValue,
                WindowMustBeActive = this.WindowMustBeActive,
            };
        }

        public override bool IsPressed()
        {
            return MouseHelpers.ScrolledHorizontally;
        }

        public override bool IsReleased()
        {
            return !MouseHelpers.ScrolledHorizontally;
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeSensor(Sensor);
        }
    }
}
