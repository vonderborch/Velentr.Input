using System;
using VilliInput.Conditions.Internal;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Keyboard;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{
    public class KeyboardCurrentKeysPressedCountCondition : LogicCondition
    {

        public KeyboardCurrentKeysPressedCountCondition(ValueLogic logicValue, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet) : base(InputSource.Keyboard, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {

            logicValue.Value.Validate();
            if (logicValue.Value.Type != Enums.ValueType.Int)
            {
                throw new Exception("logicValue contains an invalid type for CurrentKeysPressedCount, you must use a ValueType.Int!");
            }
        }

        protected override Value InternalGetValue()
        {
            return new Value(Enums.ValueType.Int, valueInt: KeyboardService.CurrentKeysPressed());
        }

        public override void Consume()
        {
            Villi.System.Keyboard.ConsumeCurrentKeysPressedCount();
        }

        public override bool IsConsumed()
        {
            return Villi.System.Keyboard.IsCurrentKeysPressedCountConsumed();
        }

        public override VilliEventArguments GetArguments()
        {
            return new KeyboardKeysPressedCountEventArguments()
            {
                Condition = this,
                InputSource = this.InputSource,
                NumberOfKeysPressed = KeyboardService.CurrentKeysPressed(),
                NumberOfKeysPressedDelta =  KeyboardService.KeysPressedCountDelta(),
                MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return ((!WindowMustBeActive || (Villi.IsWindowActive && MouseService.IsMouseInWindow))
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet));
        }

    }
}
