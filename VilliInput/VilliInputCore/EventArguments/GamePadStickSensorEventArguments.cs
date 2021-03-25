using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.GamePadInput;

namespace VilliInput.EventArguments
{
    public class GamePadStickSensorEventArguments : VilliEventArguments
    {
        public GamePadSensor Sensor { get; internal set; }

        public int PlayerIndex { get; internal set; }

        public GamePadInputMode InputMode { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }

        public Vector2 Delta { get; internal set; }

        public Vector2 CurrentPosition { get; internal set; }
    }
}
