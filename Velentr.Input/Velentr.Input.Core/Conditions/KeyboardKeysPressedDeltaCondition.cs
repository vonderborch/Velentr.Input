using System;
using Velentr.Input.Conditions.Internal;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;
using ValueType = Velentr.Input.Enums.ValueType;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when the number of keys pressed on the keyboard on this frame versus the last frame meets a certain condition specified by the logicValue parameter.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.LogicCondition" />
    public class KeyboardKeysPressedDeltaCondition : LogicCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardKeysPressedDeltaCondition"/> class.
        /// </summary>
        /// <param name="logicValue">The logic value.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <exception cref="System.Exception">logicValue contains an invalid type for KeysPressedDelta, you must use a ValueType.Int!</exception>
        public KeyboardKeysPressedDeltaCondition(ValueLogic logicValue, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet) : base(InputSource.Keyboard, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            logicValue.Value.Validate();
            if (logicValue.Value.Type != ValueType.Int)
            {
                throw new Exception("logicValue contains an invalid type for KeysPressedDelta, you must use a ValueType.Int!");
            }
        }

        /// <summary>
        /// Internals the get value.
        /// </summary>
        /// <returns></returns>
        protected override Value InternalGetValue()
        {
            return new Value(ValueType.Int, valueInt: VelentrInput.System.Keyboard.KeysPressedCountDelta());
        }

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            VelentrInput.System.Keyboard.ConsumeKeysPressedDeltaCount();
        }

        /// <summary>
        /// Determines whether the input is consumed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the input is consumed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsConsumed()
        {
            return VelentrInput.System.Keyboard.IsKeysPressedDeltaConsumed();
        }

        /// <summary>
        /// Gets the arguments to provide to events that are fired.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks to see if the input is valid.
        /// </summary>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns></returns>
        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || VelentrInput.IsWindowActive && MouseService.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime) >= milliSecondsForConditionMet);
        }

    }

}
