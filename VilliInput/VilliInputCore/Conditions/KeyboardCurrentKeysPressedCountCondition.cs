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

    public class KeyboardCurrentKeysPressedCountCondition : LogicCondition
    {

        public KeyboardCurrentKeysPressedCountCondition(ValueLogic logicValue, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet) : base(InputSource.Keyboard, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            logicValue.Value.Validate();
            if (logicValue.Value.Type != ValueType.Int)
            {
                throw new Exception("logicValue contains an invalid type for CurrentKeysPressedCount, you must use a ValueType.Int!");
            }
        }

        protected override Value InternalGetValue()
        {
            return new Value(ValueType.Int, valueInt: KeyboardService.CurrentKeysPressed());
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
