using Velentr.Input.Conditions.Internal;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when a button on the Keyboard has been released for at least 2 frames.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.KeyboardButtonCondition" />
    public class KeyboardButtonReleasedCondition : KeyboardButtonCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardButtonReleasedCondition"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        public KeyboardButtonReleasedCondition(Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(key, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        /// <summary>
        /// Currents the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool CurrentStateValid()
        {
            return VelentrInput.System.Keyboard.IsKeyReleased(Key);
        }

        /// <summary>
        /// Previouses the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool PreviousStateValid()
        {
            return VelentrInput.System.Keyboard.WasKeyReleased(Key);
        }

    }

}
