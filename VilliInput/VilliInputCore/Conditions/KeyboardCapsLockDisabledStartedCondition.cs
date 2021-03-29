using VilliInput.Conditions.Internal;
using VilliInput.Keyboard;

namespace VilliInput.Conditions
{

    public class KeyboardCapsLockDisabledStartedCondition : KeyboardLockStateCondition
    {

        public KeyboardCapsLockDisabledStartedCondition(KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(lockType, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        protected override bool CurrentStateValid()
        {
            switch (LockType)
            {
                case KeyboardLock.CapsLock:
                    return !Villi.System.Keyboard.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return !Villi.System.Keyboard.IsNumLockEnabled();
            }

            return false;
        }

        protected override bool PreviousStateValid()
        {
            switch (LockType)
            {
                case KeyboardLock.CapsLock:
                    return Villi.System.Keyboard.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return Villi.System.Keyboard.IsNumLockEnabled();
            }

            return false;
        }

    }

}
