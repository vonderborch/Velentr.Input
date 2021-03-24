using System;
using System.Collections.Generic;
using System.Text;
using VilliInput.EventArguments;

namespace VilliInput.Conditions
{
    public class AnyCondition : InputCondition
    {
        public InputCondition[] Conditions { get; private set; }

        private VilliEventArguments arguments;

        public AnyCondition(bool windowMustBeActive = true, params InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive)
        {
            this.Conditions = conditions;
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
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].Pressed(false, ignoredConsumed))
                {
                    arguments = Conditions[i].GetArguments();
                    InternalPressed(consumable);
                    return true;
                }
            }

            return false;
        }

        public override bool PressStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].PressStarted(false, ignoredConsumed))
                {
                    arguments = Conditions[i].GetArguments();
                    InternalPressStarted(consumable);
                    return true;
                }
            }

            return false;
        }

        public override bool Released(bool consumable = true, bool ignoredConsumed = false)
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].Released(false, ignoredConsumed))
                {
                    arguments = Conditions[i].GetArguments();
                    InternalReleased(consumable);
                    return true;
                }
            }

            return false;
        }

        public override bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].ReleaseStarted(false, ignoredConsumed))
                {
                    arguments = Conditions[i].GetArguments();
                    InternalReleaseStarted(consumable);
                    return true;
                }
            }

            return false;
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
