using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.GamePad;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{
    public class GamePadButtonPressStartedCondition : GamePadButtonCondition
    {
        public GamePadButtonPressStartedCondition(GamePadButton button, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(button, playerIndex, inputMode, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
        }

        protected override bool InternalCurrent(int playerIndex)
        {
            return GamePadService.IsButtonPressed(Button, playerIndex);
        }

        protected override bool InternalPrevious(int playerIndex)
        {
            return GamePadService.WasButtonReleased(Button, playerIndex);
        }
    }
}
