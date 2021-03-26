using System.Collections.Generic;
using VilliInput.OLD.Conditions;

namespace VilliInput.EventArguments
{
    public class AllConditionEventArguments : OLD.EventArguments.VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public List<OLD.EventArguments.VilliEventArguments> ConditionEventArguments { get; internal set; }
    }
}
