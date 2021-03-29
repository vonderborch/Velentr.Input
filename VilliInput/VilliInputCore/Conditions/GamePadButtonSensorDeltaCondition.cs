﻿using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.GamePad;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{
    public class GamePadButtonSensorDeltaCondition : GamePadLogicCondition
    {

        public GamePadButtonSensorDeltaCondition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode sensorValueMode, ValueLogic logicValue, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(sensor, playerIndex, inputMode, sensorValueMode, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {

        }

        protected override Value InternalGetValue()
        {
            switch (Sensor)
            {
                case GamePadSensor.LeftStick:
                case GamePadSensor.RightStick:
                    return new Value(ValueType.Vector2, valueVector2: GamePadService.GetStickDelta(Sensor, PlayerIndex, InputMode, SensorValueMode));
                case GamePadSensor.LeftTrigger:
                case GamePadSensor.RightTrigger:
                    return new Value(ValueType.Float, valueFloat: GamePadService.GetTriggerDelta(Sensor, PlayerIndex, InputMode, SensorValueMode));
            }

            return new Value(ValueType.None);
        }

    }
}