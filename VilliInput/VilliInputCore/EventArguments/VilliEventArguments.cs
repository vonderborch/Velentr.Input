using System;
using Microsoft.Xna.Framework;
using VilliInput.Conditions;

namespace VilliInput.EventArguments
{
    public abstract class VilliEventArguments : EventArgs
    {
        public InputSource InputSource { get; internal set; }

        public InputCondition ConditionSource { get; internal set; }

        public ConditionState ConditionState { get; internal set; }

        public GameTime ConditionStateStartTime { get; internal set; }

        public uint ConditionStateTimeSeconds { get; internal set; }

        public bool WindowMustBeActive { get; internal set; }
    }
}
