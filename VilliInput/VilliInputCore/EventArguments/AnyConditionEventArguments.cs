using VilliInput.OLD.Conditions;

namespace VilliInput.EventArguments
{
    public class AnyConditionEventArguments : OLD.EventArguments.VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public OLD.EventArguments.VilliEventArguments ValidConditionArguments { get; internal set; }
    }
}
