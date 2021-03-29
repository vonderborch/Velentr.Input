using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions
{

    public class AnyCondition : InputCondition
    {

        private ConditionEventArguments _arguments;

        public AnyCondition(bool windowMustBeActive = true, params InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive, false, true, 0)
        {
            Conditions = conditions;
        }

        public AnyCondition(params InputCondition[] conditions) : base(InputSource.AnyConditional, true, false, true, 0)
        {
            Conditions = conditions;
        }

        public InputCondition[] Conditions { get; }

        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            _arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (Conditions[i].ConditionMet())
                    {
                        _arguments = Conditions[i].GetArguments();
                        if (!ConditionMetState)
                        {
                            UpdateState(true);
                        }
                        ConditionMetCleanup(consumable, GetArguments());
                        return true;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            if (ConditionMetState)
            {
                UpdateState(false);
            }
            return false;
        }

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        public override void Consume()
        {
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    Conditions[i].Consume();
                }
                catch
                {
                    // ignored
                }
            }
        }

        public override bool IsConsumed()
        {
            return false;
        }

        public override ConditionEventArguments GetArguments()
        {
            return new AnyConditionalEventArguments
            {
                Conditions = Conditions,
                Condition = this,
                InputSource = InputSource,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                ValidConditionArguments = _arguments.Clone()
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return true;
        }

    }

}
