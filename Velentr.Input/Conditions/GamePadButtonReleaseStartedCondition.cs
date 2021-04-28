using Velentr.Input.Conditions.Internal;
using Velentr.Input.GamePad;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when a button on a GamePad has been pressed but is now released.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.GamePadButtonCondition" />
    public class GamePadButtonReleaseStartedCondition : GamePadButtonCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadButtonReleaseStartedCondition"/> class.
        /// </summary>
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        public GamePadButtonReleaseStartedCondition(InputManager manager, GamePadButton button, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, button, playerIndex, inputMode, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut) { }

        /// <summary>
        /// Internals the current.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
        protected override bool InternalCurrent(int playerIndex)
        {
            return Manager.GamePad.IsButtonReleased(Button, playerIndex);
        }

        /// <summary>
        /// Internals the previous.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
        protected override bool InternalPrevious(int playerIndex)
        {
            return Manager.GamePad.WasButtonPressed(Button, playerIndex);
        }

    }

}
