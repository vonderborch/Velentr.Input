using Velentr.Input.Conditions.Internal;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when caps lock was just disabled.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.KeyboardLockStateCondition" />
    public class KeyboardLockDisabledStartedCondition : KeyboardLockStateCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardLockDisabledStartedCondition"/> class.
        /// </summary>
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="lockType">Type of the lock.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        public KeyboardLockDisabledStartedCondition(InputManager manager, KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, lockType, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut) { }

        /// <summary>
        /// Currents the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool CurrentStateValid()
        {
            switch (LockType)
            {
                case KeyboardLock.CapsLock:
                    return !Manager.Keyboard.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return !Manager.Keyboard.IsNumLockEnabled();
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
                    return Manager.Keyboard.IsCapsLockEnabled();
                case KeyboardLock.NumLock:
                    return Manager.Keyboard.IsNumLockEnabled();
            }

            return false;
        }

    }

}
