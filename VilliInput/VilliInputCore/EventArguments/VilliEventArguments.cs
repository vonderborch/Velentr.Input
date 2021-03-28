using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions;
using VilliInput.Enums;

namespace VilliInput.EventArguments
{
    public abstract class VilliEventArguments : EventArgs
    {
        public InputSource InputSource { get; internal set; }

        public InputCondition Condition { get; internal set; }

        public GameTime ConditionStateStartTime { get; internal set; }

        public uint ConditionStateTimeMilliSeconds { get; internal set; }

        public bool WindowMustBeActive { get; internal set; }

        public uint MilliSecondsForConditionMet { get; internal set; }

        public abstract VilliEventArguments Clone();

    }
}
