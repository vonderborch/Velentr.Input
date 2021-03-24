using Microsoft.Xna.Framework;
using VilliInput.EventArguments;
using VilliInput.MouseInput;

namespace VilliInput.Conditions
{
    public class MouseButtonCondition : InputCondition
    {
        public MouseButton Button { get; private set; }

        public Rectangle? Boundaries { get; private set; }

        public bool UseRelativeCoordinates { get; private set; }

        public uint SecondsForPressed { get; private set; }

        public uint SecondsForReleased { get; private set; }

        public MouseButtonCondition(MouseButton button, bool windowMustBeActive = true, Rectangle? boundaries = null, bool useRelativeCoordinates = false, uint secondsForPressed = 0, uint secondsForReleased = 0) : base(InputSource.Mouse, windowMustBeActive)
        {
            Button = button;
            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;

            SecondsForPressed = secondsForPressed;
            SecondsForReleased = secondsForReleased;
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeButton(Button);
        }

        private bool ActionValid(bool ignoredConsumed, uint actionTime)
        {
            return (!WindowMustBeActive || (Villi.IsWindowActive && MouseHelpers.IsMouseInWindow))
                   && (ignoredConsumed || Villi.System.Mouse.IsButtonConsumed(Button))
                   && (actionTime == 0 || Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime) >= actionTime)
                   && (Boundaries == null || MouseHelpers.PointerInBoundaries((Rectangle)Boundaries, UseRelativeCoordinates, Villi.Window.ClientBounds));
        }

        public override bool Pressed(bool consumable = true, bool ignoredConsumed = false)
        {
            var isPressed = MouseHelpers.IsPressed(Button);
            if (isPressed && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Pressed);
            }
            else if (!isPressed && CurrentState == ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && isPressed)
            {
                InternalPressed(consumable);
                return true;
            }
            return false;
        }

        public override bool PressStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            var isPressed = MouseHelpers.IsPressed(Button);
            var wasPressed = MouseHelpers.WasPressed(Button);
            if (isPressed && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Pressed);
            }
            else if (!isPressed && CurrentState == ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }

            if (ActionValid(ignoredConsumed, 0) && isPressed && !wasPressed)
            {
                InternalPressStarted(consumable);
                return true;
            }
            return false;
        }

        public override bool Released(bool consumable = true, bool ignoredConsumed = false)
        {
            var isReleased = MouseHelpers.IsReleased(Button);
            if (isReleased && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }
            else if (!isReleased && CurrentState == ConditionState.Released)
            {
                UpdateState(ConditionState.Pressed);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && isReleased)
            {
                InternalReleased(consumable);
                return true;
            }
            return false;
        }

        public override bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            var isReleased = MouseHelpers.IsReleased(Button);
            var wasReleased = MouseHelpers.WasReleased(Button);
            if (isReleased && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }
            else if (!isReleased && CurrentState == ConditionState.Released)
            {
                UpdateState(ConditionState.Pressed);
            }

            if (ActionValid(ignoredConsumed, 0) && isReleased && !wasReleased)
            {
                InternalReleaseStarted(consumable);
                return true;
            }
            return false;
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
