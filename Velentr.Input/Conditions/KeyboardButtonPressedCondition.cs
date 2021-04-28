using Velentr.Input.Conditions.Internal;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when a button on the Keyboard has been pressed for at least 2 frames.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.KeyboardButtonCondition" />
    public class KeyboardButtonPressedCondition : KeyboardButtonCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButtonPressedCondition"/> class.
        /// </summary>
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="key">The key.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        public KeyboardButtonPressedCondition(InputManager manager, Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, key, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut) { }

        /// <summary>
        /// Currents the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool CurrentStateValid()
        {
            return Manager.Keyboard.IsKeyPressed(Key);
        }

        /// <summary>
        /// Previouses the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool PreviousStateValid()
        {
            return Manager.Keyboard.WasKeyPressed(Key);
        }

    }

}
