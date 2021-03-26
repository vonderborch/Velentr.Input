using VilliInput.OLD.Conditions.Internal;
using VilliInput.OLD.EventArguments;
using VilliInput.OLD.KeyboardInput;
using VilliInput.OLD.MouseInput;

namespace VilliInput.OLD.Conditions
{
    public class KeyboardCondition : ButtonCondition
    {
        public Key Key { get; private set; }

        public KeyboardCondition(Key key, bool windowMustBeActive = true, uint secondsForPressed = 0, uint secondsForReleased = 0) : base(InputSource.Keyboard, windowMustBeActive, secondsForPressed, secondsForReleased, true, true, true, true, false, null)
        {
            Key = key;
        }

        protected override bool ActionValid(bool ignoredConsumed, uint actionTime)
        {
            return (!WindowMustBeActive || (Villi.IsWindowActive && MouseHelpers.IsMouseInWindow))
                   && (ignoredConsumed || Villi.System.Keyboard.IsKeyConsumed(Key))
                   && (actionTime == 0 || Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime) >= actionTime);
        }

        public override void Consume()
        {
            Villi.System.Keyboard.ConsumeKey(Key);
        }

        protected override bool IsPressed()
        {
            return KeyboardHelpers.IsKeyPressed(Key);
        }

        protected override bool WasPressed()
        {
            return KeyboardHelpers.WasKeyPressed(Key);
        }

        protected override bool IsReleased()
        {
            return KeyboardHelpers.IsKeyReleased(Key);
        }

        protected override bool WasReleased()
        {
            return KeyboardHelpers.WasKeyReleased(Key);
        }

        internal override VilliEventArguments GetArguments()
        {
            return new KeyboardEventArguments()
            {
                Key = this.Key,
                ConditionSource = this,
                InputSource = this.Source,
                SecondsForPressed = this.SecondsForPressed,
                SecondsForReleased = this.SecondsForReleased,
                ConditionState = this.CurrentState,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
            };
        }
    }
}
