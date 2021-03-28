using System.Collections.Generic;
using VilliInput.Conditions;

namespace VilliInput.EventArguments
{
    public class AllConditionEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public List<VilliEventArguments> ConditionEventArguments { get; internal set; }
    }
}
