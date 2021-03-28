using System;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Keyboard;
using VilliInput.KeyboardInput;
using VilliInput.Mouse;

namespace VilliInput.Conditions.Internal
{
    public abstract class KeyboardButtonCondition : BooleanCondition
    {
        public Key Key { get; private set; }

        protected KeyboardButtonCondition(Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.Keyboard, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Key = key;
        }

        public override VilliEventArguments GetArguments()
        {
            return new KeyboardButtonEventArguments()
            {
                Key = this.Key,
                Condition = this,
                InputSource = this.InputSource,
                NumberOfKeysPressed = KeyboardService.CurrentKeysPressed(),
                MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
            };
        }

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || (Villi.IsWindowActive && MouseService.IsMouseInWindow))
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet);
        }

        public override void Consume()
        {
            Villi.System.Keyboard.ConsumeKey(Key);
        }

        public override bool IsConsumed()
        {
            return Villi.System.Keyboard.IsKeyConsumed(Key);
        }
    }
}
