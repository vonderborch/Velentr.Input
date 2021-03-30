using Microsoft.Xna.Framework;
using Velentr.Input.GamePad;

namespace Velentr.Input.EventArguments
{

    public class GamePadStickSensorEventArguments : ConditionEventArguments
    {

        public GamePadSensor Sensor { get; internal set; }

        public int PlayerIndex { get; internal set; }

        public GamePadInputMode InputMode { get; internal set; }

        public GamePadSensorValueMode SensorValueMode { get; internal set; }

        public Vector2 Delta { get; internal set; }

        public Vector2 CurrentPosition { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (GamePadStickSensorEventArguments) MemberwiseClone();
        }

    }

}
