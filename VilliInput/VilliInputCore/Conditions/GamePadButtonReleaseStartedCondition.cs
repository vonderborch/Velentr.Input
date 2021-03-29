﻿using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.GamePad;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{
    public class GamePadButtonReleaseStartedCondition : GamePadButtonCondition
    {
        public GamePadButtonReleaseStartedCondition(GamePadButton button, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(button, playerIndex, inputMode, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
        }

        protected override bool InternalCurrent(int playerIndex)
        {
            return GamePadService.IsButtonReleased(Button, playerIndex);
        }

        protected override bool InternalPrevious(int playerIndex)
        {
            return GamePadService.WasButtonPressed(Button, playerIndex);
        }
    }
}