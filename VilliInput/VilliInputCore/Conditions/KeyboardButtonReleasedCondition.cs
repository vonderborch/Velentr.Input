using VilliInput.Conditions.Internal;
using VilliInput.Keyboard;

namespace VilliInput.Conditions
{

    public class KeyboardButtonReleasedCondition : KeyboardButtonCondition
    {

        public KeyboardButtonReleasedCondition(Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(key, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        protected override bool CurrentStateValid()
        {
            return Villi.System.Keyboard.IsKeyReleased(Key);
        }

        protected override bool PreviousStateValid()
        {
            return Villi.System.Keyboard.WasKeyReleased(Key);
        }

    }

}
