using Velentr.Input.Conditions.Internal;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when caps lock is disabled.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.KeyboardLockStateCondition" />
    public class KeyboardCapsLockDisabledCondition : KeyboardLockStateCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardCapsLockDisabledCondition"/> class.
        /// </summary>
        /// <param name="lockType">Type of the lock.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        public KeyboardCapsLockDisabledCondition(KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(lockType, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        /// <summary>
        /// Currents the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool CurrentStateValid()
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

        /// <summary>
        /// Previouses the state valid.
        /// </summary>
        /// <returns></returns>
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
