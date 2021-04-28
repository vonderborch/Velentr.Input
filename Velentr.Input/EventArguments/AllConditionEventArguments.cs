using System.Collections.Generic;
using Velentr.Input.Conditions;

namespace Velentr.Input.EventArguments
{

    public class AllConditionEventArguments : ConditionEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public List<ConditionEventArguments> ConditionEventArguments { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            var copy = (AllConditionEventArguments) MemberwiseClone();

            copy.ConditionEventArguments = new List<ConditionEventArguments>(ConditionEventArguments.Count);
            for (var i = 0; i < ConditionEventArguments.Count; i++)
            {
                copy.ConditionEventArguments.Add(ConditionEventArguments[i].Clone());
            }

            return copy;
        }

    }

}
