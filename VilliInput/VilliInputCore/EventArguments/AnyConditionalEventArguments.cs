using VilliInput.Conditions;

namespace VilliInput.EventArguments
{
    public class AnyConditionalEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public EventArguments.VilliEventArguments ValidConditionArguments { get; internal set; }
    }
}
