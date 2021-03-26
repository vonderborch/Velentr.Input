using Microsoft.Xna.Framework;
using VilliInput.OLD.MouseInput;

namespace VilliInput.OLD.EventArguments
{
    public class MouseButtonEventArguments : VilliEventArguments
    {
        public MouseButton Button { get; internal set; }

        public Rectangle? Boundaries { get; internal set; }

        public bool UseRelativeCoordinates { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }

        public Point MouseCoordinates { get; internal set; }

        public Point RelativeMouseCoordinates { get; internal set; }
    }
}
