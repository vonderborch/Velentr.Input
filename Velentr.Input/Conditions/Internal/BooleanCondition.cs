using Velentr.Input.Enums;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class BooleanCondition : InputCondition
    {

        protected BooleanCondition(InputManager manager, InputSource source, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet, uint milliSecondsForTimeOut) : base(manager, source, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut) { }

        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            var currentStateValid = CurrentStateValid();
            var previousStateValid = PreviousStateValid();
            if (currentStateValid && !ConditionMetState)
            {
                UpdateState(true);
            }
            else if (!currentStateValid && ConditionMetState)
            {
                UpdateState(false);
            }

            if (ActionValid(allowedIfConsumed, MilliSecondsForConditionMet) && currentStateValid && previousStateValid)
            {
                ConditionMetCleanup(consumable, GetArguments());
                return true;
            }

            return false;
        }

        protected abstract bool CurrentStateValid();

        protected abstract bool PreviousStateValid();

    }

}
