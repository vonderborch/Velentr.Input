using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Touch;

namespace Velentr.Input.EventArguments
{

    public class TouchEventArguments : ConditionEventArguments
    {

        public GestureType GestureType { get; internal set; }

        public Rectangle? Boundaries { get; internal set; }

        public bool UseRelativeCoordinates { get; internal set; }

        public List<Gesture> Gestures { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (TouchEventArguments) MemberwiseClone();
        }

    }

}
