using System.Collections.Generic;
using VilliInput.Conditions;

namespace VilliInput.EventArguments
{

    public class AllConditionEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public List<VilliEventArguments> ConditionEventArguments { get; internal set; }

        public override VilliEventArguments Clone()
        {
            var copy = (AllConditionEventArguments) MemberwiseClone();

            copy.ConditionEventArguments = new List<VilliEventArguments>(ConditionEventArguments.Count);
            for (var i = 0; i < ConditionEventArguments.Count; i++)
            {
                copy.ConditionEventArguments.Add(ConditionEventArguments[i].Clone());
            }

            return copy;
        }

    }

}
