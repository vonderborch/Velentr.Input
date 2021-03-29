using Velentr.Input.Conditions.Internal;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions
{

    public class KeyboardButtonReleasedCondition : KeyboardButtonCondition
    {

        public KeyboardButtonReleasedCondition(Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(key, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        protected override bool CurrentStateValid()
        {
            return VelentrInput.System.Keyboard.IsKeyReleased(Key);
        }

        protected override bool PreviousStateValid()
        {
            return VelentrInput.System.Keyboard.WasKeyReleased(Key);
        }

    }

}
