using System;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;

namespace VilliInput.Conditions
{
    public class AnyCondition : InputCondition
    {
        public InputCondition[] Conditions { get; private set; }

        private VilliEventArguments _arguments;

        public AnyCondition(bool windowMustBeActive = true, params Conditions.InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive, false, true, 0)
        {
            Conditions = conditions;
        }

        public AnyCondition(params Conditions.InputCondition[] conditions) : base(InputSource.AnyConditional, true, false, true, 0)
        {
            Conditions = conditions;
        }

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
                        return true;
                    }
                }
                catch
                {
                    // ignored
                }
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

        public override VilliEventArguments GetArguments()
        {
            return new AnyConditionalEventArguments()
            {
                Conditions = this.Conditions,
                Condition = this,
                InputSource = this.InputSource,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
                ValidConditionArguments = this._arguments.Clone(),
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return true;
        }

    }
}
