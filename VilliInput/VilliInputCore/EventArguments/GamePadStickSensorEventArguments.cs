using Microsoft.Xna.Framework;
using VilliInput.OLD.GamePadInput;

namespace VilliInput.EventArguments
{
    public class GamePadStickSensorEventArguments : OLD.EventArguments.VilliEventArguments
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
