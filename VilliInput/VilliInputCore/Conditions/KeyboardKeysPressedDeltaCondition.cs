using System;
using VilliInput.Conditions.Internal;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Keyboard;
using VilliInput.Mouse;
using ValueType = VilliInput.Enums.ValueType;

namespace VilliInput.Conditions
{

    public class KeyboardKeysPressedDeltaCondition : LogicCondition
    {

        public KeyboardKeysPressedDeltaCondition(ValueLogic logicValue, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet) : base(InputSource.Keyboard, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            logicValue.Value.Validate();
            if (logicValue.Value.Type != ValueType.Int)
            {
                throw new Exception("logicValue contains an invalid type for KeysPressedDelta, you must use a ValueType.Int!");
            }
        }

        protected override Value InternalGetValue()
        {
            return new Value(ValueType.Int, valueInt: KeyboardService.KeysPressedCountDelta());
        }

        public override void Consume()
        {
            Villi.System.Keyboard.ConsumeKeysPressedDeltaCount();
        }

        public override bool IsConsumed()
        {
            return Villi.System.Keyboard.IsKeysPressedDeltaConsumed();
        }

        public override VilliEventArguments GetArguments()
        {
            return new KeyboardKeysPressedCountEventArguments
            {
                Condition = this,
                InputSource = InputSource,
                NumberOfKeysPressed = KeyboardService.CurrentKeysPressed(),
                NumberOfKeysPressedDelta = KeyboardService.KeysPressedCountDelta(),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || Villi.IsWindowActive && MouseService.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet);
        }

    }

}
