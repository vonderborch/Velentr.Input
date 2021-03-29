using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions
{

    public class AllCondition : InputCondition
    {

        private List<ConditionEventArguments> _arguments;

        public AllCondition(bool windowMustBeActive = true, bool orderMatters = false, params InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive, false, true, 0)
        {
            Conditions = conditions;
            OrderMatters = orderMatters;
        }

        public AllCondition(params InputCondition[] conditions) : base(InputSource.AnyConditional, true, false, true, 0)
        {
            Conditions = conditions;
            OrderMatters = false;
        }

        public InputCondition[] Conditions { get; }

        public bool OrderMatters { get; }

        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            GameTime time = null;
            _arguments = new List<ConditionEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (!Conditions[i].IsConditionMet())
                    {
                        if (ConditionMetState)
                        {
                            UpdateState(false);
                        }
                        return false;
                    }

                    if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                    {
                        time = Conditions[i].CurrentStateStart;
                        _arguments.Add(Conditions[i].GetArguments());
                    }
                    else
                    {
                        if (ConditionMetState)
                        {
                            UpdateState(false);
                        }
                        return false;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            if (!ConditionMetState)
            {
                UpdateState(true);
            }
            ConditionMetCleanup(consumable, GetArguments());
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

        public override ConditionEventArguments GetArguments()
        {
            var args = new List<ConditionEventArguments>(_arguments.Count);
            foreach (var arg in _arguments)
            {
                args.Add(arg.Clone());
            }

            return new AllConditionEventArguments
            {
                Conditions = Conditions,
                Condition = this,
                InputSource = InputSource,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                ConditionEventArguments = args
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return true;
        }

    }

}
