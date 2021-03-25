using System;
using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.MouseInput;

namespace VilliInput.Conditions
{
    public class MouseButtonCondition : ButtonCondition
    {
        public MouseButton Button { get; private set; }

        public Rectangle? Boundaries { get; private set; }

        public bool UseRelativeCoordinates { get; private set; }

        public MouseButtonCondition(MouseButton button, bool windowMustBeActive = true, Rectangle? boundaries = null, bool useRelativeCoordinates = false, uint secondsForPressed = 0, uint secondsForReleased = 0) : base(InputSource.Mouse, windowMustBeActive, secondsForPressed, secondsForReleased, true, true, true, true, false, null)
        {
            Button = button;
            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeButton(Button);
        }

        protected override bool ActionValid(bool ignoredConsumed, uint actionTime)
        {
            return (!WindowMustBeActive || (Villi.IsWindowActive && MouseHelpers.IsMouseInWindow))
                   && (ignoredConsumed || Villi.System.Mouse.IsButtonConsumed(Button))
                   && (actionTime == 0 || Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime) >= actionTime)
                   && (Boundaries == null || MouseHelpers.PointerInBoundaries((Rectangle)Boundaries, UseRelativeCoordinates, Villi.Window.ClientBounds));
        }

        protected override bool IsPressed()
        {
            return MouseHelpers.IsPressed(Button);
        }

        protected override bool WasPressed()
        {
            return MouseHelpers.WasPressed(Button);
        }

        protected override bool IsReleased()
        {
            return MouseHelpers.IsReleased(Button);
        }

        protected override bool WasReleased()
        {
            return MouseHelpers.WasReleased(Button);
        }

        internal override VilliEventArguments GetArguments()
        {
            return new MouseButtonEventArguments()
            {
                Boundaries = this.Boundaries,
                Button = this.Button,
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
                WindowMustBeActive = this.WindowMustBeActive,
            };
        }
    }
}
