﻿using VilliInput.Conditions.Internal;
using VilliInput.Keyboard;
using VilliInput.KeyboardInput;

namespace VilliInput.Conditions
{
    public class KeyboardButtonReleasedCondition : KeyboardButtonCondition
    {
        public KeyboardButtonReleasedCondition(Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(key, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
        }

        protected override bool CurrentStateValid()
        {
            return KeyboardService.IsKeyReleased(Key);
        }

        protected override bool PreviousStateValid()
        {
            return KeyboardService.WasKeyReleased(Key);
        }

    }
}
