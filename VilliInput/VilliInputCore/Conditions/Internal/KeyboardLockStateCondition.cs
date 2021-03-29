using System;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Keyboard;
using VilliInput.Mouse;

namespace VilliInput.Conditions.Internal
{

    public abstract class KeyboardLockStateCondition : BooleanCondition
    {

        protected KeyboardLockStateCondition(KeyboardLock lockType, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.Keyboard, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            LockType = lockType;
        }

        public KeyboardLock LockType { get; }

        public override VilliEventArguments GetArguments()
        {
            return new KeyboardLockStateEventArguments
            {
                LockType = LockType,
                Condition = this,
                InputSource = InputSource,
                NumberOfKeysPressed = KeyboardService.CurrentKeysPressed(),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
        }

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || Villi.IsWindowActive && MouseService.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet);
        }

        public override void Consume()
        {
            Villi.System.Keyboard.ConsumeLock(LockType);
        }

        public override bool IsConsumed()
        {
            return Villi.System.Keyboard.IsLockConsumed(LockType);
        }

    }

}
