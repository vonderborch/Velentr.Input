﻿using VilliInput.OLD.GamePadInput;

namespace VilliInput.EventArguments
{
    public class GamePadTriggerSensorEventArguments : OLD.EventArguments.VilliEventArguments
    {
        public GamePadSensor Sensor { get; internal set; }

        public int PlayerIndex { get; internal set; }

        public GamePadInputMode InputMode { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }

        public float Delta { get; internal set; }

        public float CurrentPosition { get; internal set; }
    }
}
