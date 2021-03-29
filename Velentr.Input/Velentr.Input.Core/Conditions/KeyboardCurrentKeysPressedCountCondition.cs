using System;
using Velentr.Input.Conditions.Internal;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;
using ValueType = Velentr.Input.Enums.ValueType;

namespace Velentr.Input.Conditions
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
            return new Value(ValueType.Int, valueInt: VelentrInput.System.Keyboard.CurrentKeysPressed());
        }

        public override void Consume()
        {
            VelentrInput.System.Keyboard.ConsumeCurrentKeysPressedCount();
        }

        public override bool IsConsumed()
        {
            return VelentrInput.System.Keyboard.IsCurrentKeysPressedCountConsumed();
        }

        public override ConditionEventArguments GetArguments()
        {
            return new KeyboardKeysPressedCountEventArguments
            {
                Condition = this,
                InputSource = InputSource,
                NumberOfKeysPressed = VelentrInput.System.Keyboard.CurrentKeysPressed(),
                NumberOfKeysPressedDelta = VelentrInput.System.Keyboard.KeysPressedCountDelta(),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || VelentrInput.IsWindowActive && MouseService.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime) >= milliSecondsForConditionMet);
        }

    }

}
