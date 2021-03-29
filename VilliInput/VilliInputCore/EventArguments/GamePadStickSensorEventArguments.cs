using Microsoft.Xna.Framework;
using VilliInput.GamePad;

namespace VilliInput.EventArguments
{

    public class GamePadStickSensorEventArguments : VilliEventArguments
    {

        public GamePadSensor Sensor { get; internal set; }

        public int PlayerIndex { get; internal set; }

        public GamePadInputMode InputMode { get; internal set; }

        public GamePadSensorValueMode SensorValueMode { get; internal set; }

        public Vector2 Delta { get; internal set; }

        public Vector2 CurrentPosition { get; internal set; }

        public override VilliEventArguments Clone()
        {
            return (GamePadStickSensorEventArguments) MemberwiseClone();
        }

    }

}
