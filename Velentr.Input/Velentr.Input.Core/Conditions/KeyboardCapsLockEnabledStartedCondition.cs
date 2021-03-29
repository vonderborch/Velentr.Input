using Velentr.Input.Conditions.Internal;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions
{

    public class KeyboardCapsLockEnabledStartedCondition : KeyboardLockStateCondition
    {

        public KeyboardCapsLockEnabledStartedCondition(KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(lockType, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        protected override bool CurrentStateValid()
        {
            switch (LockType)
            {
                case KeyboardLock.CapsLock:
                    return VelentrInput.System.Keyboard.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return VelentrInput.System.Keyboard.IsNumLockEnabled();
            }

            return false;
        }

        protected override bool PreviousStateValid()
        {
            switch (LockType)
            {
                case KeyboardLock.CapsLock:
                    return !VelentrInput.System.Keyboard.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return !VelentrInput.System.Keyboard.IsNumLockEnabled();
            }

            return false;
        }

    }

}
