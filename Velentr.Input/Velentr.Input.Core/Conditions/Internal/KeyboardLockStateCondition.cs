using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class KeyboardLockStateCondition : BooleanCondition
    {

        protected KeyboardLockStateCondition(InputManager manager, KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.Keyboard, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
        {
            LockType = lockType;

            if (manager.Settings.ThrowWhenCreatingConditionIfNoServiceEnabled && manager.Keyboard == null)
            {
                throw new Exception(Constants.NoEngineConfiguredError);
            }
        }

        public KeyboardLock LockType { get; }

        public override ConditionEventArguments GetArguments()
        {
            return new KeyboardLockStateEventArguments
            {
                LockType = LockType,
                Condition = this,
                InputSource = InputSource,
                NumberOfKeysPressed = Manager.Keyboard.CurrentKeysPressed(),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
        }

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (
                ((WindowMustBeActive && Manager.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime) >= milliSecondsForConditionMet)
                && (MilliSecondsForTimeOut == 0 || Helper.ElapsedMilliSeconds(LastFireTime, Manager.CurrentTime) >= MilliSecondsForTimeOut)
            );
        }

        public override void Consume()
        {
            Manager.Keyboard.ConsumeLock(LockType);
        }

        public override bool IsConsumed()
        {
            return Manager.Keyboard.IsLockConsumed(LockType);
        }

    }

}
