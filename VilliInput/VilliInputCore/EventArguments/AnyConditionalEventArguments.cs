using System.Collections.Generic;
using VilliInput.Conditions;

namespace VilliInput.EventArguments
{
    public class AnyConditionalEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public EventArguments.VilliEventArguments ValidConditionArguments { get; internal set; }

        public override VilliEventArguments Clone()
        {
            var copy = (AnyConditionalEventArguments)this.MemberwiseClone();
            copy.ValidConditionArguments = ValidConditionArguments.Clone();
            return copy;
        }
    }
}
