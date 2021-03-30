using Velentr.Input.Conditions.Internal;
using Velentr.Input.GamePad;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when a button on a GamePad has been released for at least 2 frames.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.GamePadButtonCondition" />
    public class GamePadButtonReleasedCondition : GamePadButtonCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadButtonReleasedCondition"/> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        public GamePadButtonReleasedCondition(GamePadButton button, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(button, playerIndex, inputMode, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        /// <summary>
        /// Internals the current.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
        protected override bool InternalCurrent(int playerIndex)
        {
            return GamePadService.IsButtonReleased(Button, playerIndex);
        }

        /// <summary>
        /// Internals the previous.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
        protected override bool InternalPrevious(int playerIndex)
        {
            return GamePadService.WasButtonPressed(Button, playerIndex);
        }

    }

}
