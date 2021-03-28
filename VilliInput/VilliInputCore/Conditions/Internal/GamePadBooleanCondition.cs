using System;
using VilliInput.Enums;
using VilliInput.Helpers;

namespace VilliInput.Conditions.Internal
{
    public abstract class GamePadBooleanCondition : BooleanCondition
    {
        protected GamePadBooleanCondition(bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.GamePad, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
        }

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return ((!WindowMustBeActive || Villi.IsWindowActive)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet));
        }
    }
}
