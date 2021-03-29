using System;
using Velentr.Input.Enums;
using ValueType = Velentr.Input.Enums.ValueType;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class LogicCondition : InputCondition
    {

        protected LogicCondition(InputSource source, ValueLogic logicValue, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet) : base(source, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            ValueLogic = logicValue;
        }

        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            var currentStateValid = ValueValid();
            if (currentStateValid && !ConditionMetState)
            {
                UpdateState(true);
            }
            else if (!currentStateValid && ConditionMetState)
            {
                UpdateState(false);
            }

            if (ActionValid(allowedIfConsumed, MilliSecondsForConditionMet) && currentStateValid)
            {
                ConditionMetCleanup(consumable, GetArguments());
                return true;
            }

            return false;
        }


        public bool ValueValid()
        {
            if (ValueLogic == null || ValueLogic?.Value.Type == ValueType.None)
            {
                throw new NullReferenceException("ValueLogic must be initialized!");
            }

            return (bool) ValueLogic?.Compare(GetValue());
        }

    }

}
