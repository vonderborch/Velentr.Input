using System.Collections.Generic;
using VilliInput.OLD.Conditions;

namespace VilliInput.OLD.EventArguments
{
    public class AllConditionEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public List<VilliEventArguments> ConditionEventArguments { get; internal set; }
    }
}
