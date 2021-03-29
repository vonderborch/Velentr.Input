using Velentr.Input.Conditions.Internal;
using Velentr.Input.Enums;
using Velentr.Input.GamePad;

namespace Velentr.Input.Conditions
{

    public class GamePadButtonSensorPositionCondition : GamePadLogicCondition
    {

        public GamePadButtonSensorPositionCondition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode sensorValueMode, ValueLogic logicValue, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(sensor, playerIndex, inputMode, sensorValueMode, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        protected override Value InternalGetValue()
        {
            switch (Sensor)
            {
                case GamePadSensor.LeftStick:
                case GamePadSensor.RightStick:
                    return new Value(ValueType.Vector2, valueVector2: GamePadService.GetStickPosition(Sensor, PlayerIndex, InputMode, SensorValueMode));
                case GamePadSensor.LeftTrigger:
                case GamePadSensor.RightTrigger:
                    return new Value(ValueType.Float, valueFloat: GamePadService.GetTriggerPosition(Sensor, PlayerIndex, InputMode, SensorValueMode));
            }

            return new Value(ValueType.None);
        }

    }

}
