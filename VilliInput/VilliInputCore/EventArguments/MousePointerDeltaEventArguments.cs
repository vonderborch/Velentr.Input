using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.MouseInput;

namespace VilliInput.EventArguments
{
    public class MousePointerDeltaEventArguments : VilliEventArguments
    {
        public MouseSensor Sensor { get; internal set; }

        public Rectangle? Boundaries { get; internal set; }

        public bool UseRelativeCoordinates { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }

        public Point MouseCoordinates { get; internal set; }

        public Point RelativeMouseCoordinates { get; internal set; }

        public Point Delta { get; internal set; }

        public Vector2 DeltaVector { get; internal set; }
    }
}
