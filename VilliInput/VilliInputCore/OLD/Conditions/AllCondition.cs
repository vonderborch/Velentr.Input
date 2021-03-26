using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VilliInput.OLD.EventArguments;

namespace VilliInput.OLD.Conditions
{
    public class AllCondition : InputCondition
    {
        public InputCondition[] Conditions { get; private set; }
        public bool OrderMatters { get; private set; }

        private List<VilliEventArguments> arguments;

        public AllCondition(bool windowMustBeActive = true, bool orderMatters = false, params InputCondition[] conditions) : base(InputSource.AllConditional, windowMustBeActive, true, true, true, true, true)
        {
            Conditions = conditions;
            OrderMatters = orderMatters;
        }

        public override void Consume()
        {
            for (var i = 0; i < Conditions.Length; i++)
            {
                Conditions[i].Consume();
            }
        }

        public override bool Pressed(bool consumable = true, bool ignoredConsumed = false)
        {
            GameTime time = null;
            arguments = new List<VilliEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].Pressed(false, ignoredConsumed))
                {
                    return false;
                }
                else
                {
                    if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                    {
                        time = Conditions[i].CurrentStateStart;
                        arguments.Add(Conditions[i].GetArguments());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            InternalPressed(consumable);
            return true;
        }

        public override bool PressStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            GameTime time = null;
            arguments = new List<VilliEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].PressStarted(false, ignoredConsumed))
                {
                    return false;
                }
                else
                {
                    if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                    {
                        time = Conditions[i].CurrentStateStart;
                        arguments.Add(Conditions[i].GetArguments());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            InternalPressStarted(consumable);
            return true;
        }

        public override bool Released(bool consumable = true, bool ignoredConsumed = false)
        {
            GameTime time = null;
            arguments = new List<VilliEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].Released(false, ignoredConsumed))
                {
                    return false;
                }
                else
                {
                    if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                    {
                        time = Conditions[i].CurrentStateStart;
                        arguments.Add(Conditions[i].GetArguments());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            InternalReleased(consumable);
            return true;
        }

        public override bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            GameTime time = null;
            arguments = new List<VilliEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].ReleaseStarted(false, ignoredConsumed))
                {
                    return false;
                }
                else
                {
                    if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                    {
                        time = Conditions[i].CurrentStateStart;
                        arguments.Add(Conditions[i].GetArguments());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            InternalReleaseStarted(consumable);
            return true;
        }

        public override bool ValueValid()
        {
            GameTime time = null;
            arguments = new List<VilliEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].ValueValid())
                {
                    return false;
                }
                else
                {
                    if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                    {
                        time = Conditions[i].CurrentStateStart;
                        arguments.Add(Conditions[i].GetArguments());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            InternalValueValid();
            return true;
        }

        public override InputValue GetInputValue()
        {
            throw new NotImplementedException();
        }

        internal override VilliEventArguments GetArguments()
        {
            return new AllConditionEventArguments()
            {
                Conditions = this.Conditions,
                OrderMatters = this.OrderMatters,
                ConditionSource = this,
                InputSource = this.Source,
                ConditionState = this.CurrentState,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
                ConditionEventArguments = this.arguments,
            };
        }
    }
}
