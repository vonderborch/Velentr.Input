using VilliInput.OLD.Conditions;

namespace VilliInput.OLD.EventArguments
{
    public class AnyConditionEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public VilliEventArguments ValidConditionArguments { get; internal set; }
    }
}
