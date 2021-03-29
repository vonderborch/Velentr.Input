﻿using VilliInput.Conditions.Internal;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Keyboard;

namespace VilliInput.Conditions
{
    public class KeyboardCapsLockEnabledCondition : KeyboardLockStateCondition
    {

        public KeyboardCapsLockEnabledCondition(KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(lockType, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
        }

        protected override bool CurrentStateValid()
        {
            switch (LockType)
            {
                case KeyboardLock.CapsLock:
                    return KeyboardService.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return KeyboardService.IsNumLockEnabled();
            }

            return false;
        }

        protected override bool PreviousStateValid()
        {
            switch (LockType)
            {
                case KeyboardLock.CapsLock:
                    return KeyboardService.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return KeyboardService.IsNumLockEnabled();
            }

            return false;
        }

    }
}