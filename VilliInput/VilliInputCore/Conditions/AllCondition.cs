using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;

namespace VilliInput.Conditions
{
    public class AllCondition : InputCondition
    {
        public InputCondition[] Conditions { get; private set; }

        public bool OrderMatters { get; private set; }

        private List<VilliEventArguments> _arguments;

        public AllCondition(bool windowMustBeActive = true, bool orderMatters = false, params Conditions.InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive, false, true, 0)
        {
            Conditions = conditions;
            OrderMatters = orderMatters;
        }

        public AllCondition(params Conditions.InputCondition[] conditions) : base(InputSource.AnyConditional, true, false, true, 0)
        {
            Conditions = conditions;
            OrderMatters = false;
        }

        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            GameTime time = null;
            _arguments = new List<VilliEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (!Conditions[i].ConditionMet())
                    {
                        return false;
                    }
                    else
                    {
                        if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                        {
                            time = Conditions[i].CurrentStateStart;
                            _arguments.Add(Conditions[i].GetArguments());
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return true;
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
            var args = new List<VilliEventArguments>(_arguments.Count);
            foreach (var arg in _arguments)
            {
                args.Add(arg.Clone());
            }

            return new AllConditionEventArguments()
            {
                Conditions = this.Conditions,
                Condition = this,
                InputSource = this.InputSource,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
                ConditionEventArguments = args,
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return true;
        }

    }
}
