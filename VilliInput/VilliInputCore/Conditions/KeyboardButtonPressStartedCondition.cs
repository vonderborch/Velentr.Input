using VilliInput.Conditions.Internal;
using VilliInput.Keyboard;

namespace VilliInput.Conditions
{

    public class KeyboardButtonPressStartedCondition : KeyboardButtonCondition
    {

        public KeyboardButtonPressStartedCondition(Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(key, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        protected override bool CurrentStateValid()
        {
            return Villi.System.Keyboard.IsKeyPressed(Key);
        }

        protected override bool PreviousStateValid()
        {
            return Villi.System.Keyboard.WasKeyReleased(Key);
        }

    }

}
