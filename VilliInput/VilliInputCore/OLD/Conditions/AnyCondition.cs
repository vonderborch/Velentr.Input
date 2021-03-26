using System;
using VilliInput.OLD.EventArguments;

namespace VilliInput.OLD.Conditions
{
    public class AnyCondition : InputCondition
    {
        public InputCondition[] Conditions { get; private set; }

        private VilliEventArguments arguments;

        public AnyCondition(bool windowMustBeActive = true, params InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive, true, true, true, true, true)
        {
            this.Conditions = conditions;
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

        public override bool Pressed(bool consumable = true, bool ignoredConsumed = false)
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (Conditions[i].Pressed(false, ignoredConsumed))
                    {
                        arguments = Conditions[i].GetArguments();
                        InternalPressed(consumable);
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

        public override bool PressStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (Conditions[i].PressStarted(false, ignoredConsumed))
                    {
                        arguments = Conditions[i].GetArguments();
                        InternalPressStarted(consumable);
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

        public override bool Released(bool consumable = true, bool ignoredConsumed = false)
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (Conditions[i].Released(false, ignoredConsumed))
                    {
                        arguments = Conditions[i].GetArguments();
                        InternalReleased(consumable);
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

        public override bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (Conditions[i].ReleaseStarted(false, ignoredConsumed))
                    {
                        arguments = Conditions[i].GetArguments();
                        InternalReleaseStarted(consumable);
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

        public override bool ValueValid()
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (Conditions[i].ValueValid())
                    {
                        arguments = Conditions[i].GetArguments();
                        InternalValueValid();
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

        public override InputValue GetInputValue()
        {
            throw new NotImplementedException();
        }

        internal override VilliEventArguments GetArguments()
        {
            return new AnyConditionEventArguments()
            {
                Conditions = this.Conditions,
                ConditionSource = this,
                InputSource = this.Source,
                ConditionState = this.CurrentState,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
                ValidConditionArguments = this.arguments,
            };
        }
    }
}
