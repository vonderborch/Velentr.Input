using System;
using Microsoft.Xna.Framework;
using Velentr.Input.Conditions;
using Velentr.Input.Enums;

namespace Velentr.Input.EventArguments
{

    public abstract class ConditionEventArguments : EventArgs
    {

        public InputSource InputSource { get; internal set; }

        public InputCondition Condition { get; internal set; }

        public TimeSpan ConditionStateStartTime { get; internal set; }

        public uint ConditionStateTimeMilliSeconds { get; internal set; }

        public bool WindowMustBeActive { get; internal set; }

        public uint MilliSecondsForConditionMet { get; internal set; }

        public abstract ConditionEventArguments Clone();

    }

}
