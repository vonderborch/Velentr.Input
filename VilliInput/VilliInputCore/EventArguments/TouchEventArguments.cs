using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VilliInput.Touch;

namespace VilliInput.EventArguments
{

    public class TouchEventArguments : VilliEventArguments
    {

        public GestureType GestureType { get; internal set; }

        public Rectangle? Boundaries { get; internal set; }

        public bool UseRelativeCoordinates { get; internal set; }

        public List<Gesture> Gestures { get; internal set; }

        public override VilliEventArguments Clone()
        {
            return (TouchEventArguments) MemberwiseClone();
        }

    }

}
