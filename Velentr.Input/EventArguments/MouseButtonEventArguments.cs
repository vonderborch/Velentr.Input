using Microsoft.Xna.Framework;
using Velentr.Input.Mouse;

namespace Velentr.Input.EventArguments
{

    public class MouseButtonEventArguments : ConditionEventArguments
    {

        public MouseButton Button { get; internal set; }

        public Rectangle? Boundaries { get; internal set; }

        public bool UseRelativeCoordinates { get; internal set; }

        public Point MouseCoordinates { get; internal set; }

        public Point RelativeMouseCoordinates { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (MouseButtonEventArguments) MemberwiseClone();
        }

    }

}
