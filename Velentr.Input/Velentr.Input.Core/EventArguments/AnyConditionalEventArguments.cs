using Velentr.Input.Conditions;

namespace Velentr.Input.EventArguments
{

    public class AnyConditionalEventArguments : ConditionEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public ConditionEventArguments ValidConditionArguments { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            var copy = (AnyConditionalEventArguments) MemberwiseClone();
            copy.ValidConditionArguments = ValidConditionArguments.Clone();
            return copy;
        }

    }

}
