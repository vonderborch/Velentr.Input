using VilliInput.Conditions;

namespace VilliInput.EventArguments
{

    public class AnyConditionalEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public VilliEventArguments ValidConditionArguments { get; internal set; }

        public override VilliEventArguments Clone()
        {
            var copy = (AnyConditionalEventArguments) MemberwiseClone();
            copy.ValidConditionArguments = ValidConditionArguments.Clone();
            return copy;
        }

    }

}
