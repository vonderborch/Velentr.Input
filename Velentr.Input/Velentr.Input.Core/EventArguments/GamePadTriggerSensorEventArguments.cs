using Velentr.Input.GamePad;

namespace Velentr.Input.EventArguments
{

    public class GamePadTriggerSensorEventArguments : ConditionEventArguments
    {

        public GamePadSensor Sensor { get; internal set; }

        public int PlayerIndex { get; internal set; }

        public GamePadInputMode InputMode { get; internal set; }

        public GamePadSensorValueMode SensorValueMode { get; internal set; }

        public float Delta { get; internal set; }

        public float CurrentPosition { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (GamePadTriggerSensorEventArguments) MemberwiseClone();
        }

    }

}
