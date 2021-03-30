using Velentr.Input.Conditions.Internal;
using Velentr.Input.Enums;
using Velentr.Input.GamePad;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when the Position of a GamePad sensor meets a certain condition specified by the logicValue parameter.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.GamePadLogicCondition" />
    public class GamePadButtonSensorPositionCondition : GamePadLogicCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadButtonSensorPositionCondition"/> class.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="sensorValueMode">The sensor value mode.</param>
        /// <param name="logicValue">The logic value.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        public GamePadButtonSensorPositionCondition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode sensorValueMode, ValueLogic logicValue, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(sensor, playerIndex, inputMode, sensorValueMode, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet) { }

        /// <summary>
        /// Internals the get value.
        /// </summary>
        /// <returns></returns>
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
