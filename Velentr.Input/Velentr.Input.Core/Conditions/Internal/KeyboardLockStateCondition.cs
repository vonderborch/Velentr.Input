using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class KeyboardLockStateCondition : BooleanCondition
    {

        protected KeyboardLockStateCondition(KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.Keyboard, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            LockType = lockType;
        }

        public KeyboardLock LockType { get; }

        public override ConditionEventArguments GetArguments()
        {
            return new KeyboardLockStateEventArguments
            {
                LockType = LockType,
                Condition = this,
                InputSource = InputSource,
                NumberOfKeysPressed = VelentrInput.System.Keyboard.CurrentKeysPressed(),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
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
                ((WindowMustBeActive && VelentrInput.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime) >= milliSecondsForConditionMet)
            );
        }

        public override void Consume()
        {
            VelentrInput.System.Keyboard.ConsumeLock(LockType);
        }

        public override bool IsConsumed()
        {
            return VelentrInput.System.Keyboard.IsLockConsumed(LockType);
        }

    }

}
