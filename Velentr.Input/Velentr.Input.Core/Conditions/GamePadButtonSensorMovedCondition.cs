using Velentr.Input.Conditions.Internal;
using Velentr.Input.GamePad;

namespace Velentr.Input.Conditions
{

    public class GamePadButtonSensorMovedCondition : GamePadSensorBooleanCondition
    {

        public GamePadButtonSensorMovedCondition(GamePadSensor sensor, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, GamePadSensorValueMode sensorValueMode = GamePadSensorValueMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(sensor, playerIndex, inputMode, sensorValueMode, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        protected override bool InternalCurrent(int playerIndex)
        {
            return GamePadService.SensorMoved(Sensor, playerIndex);
        }

        protected override bool InternalPrevious(int playerIndex)
        {
            return true;
        }

    }

}
