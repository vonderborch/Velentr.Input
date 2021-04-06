using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.GamePad;
using Velentr.Input.Helpers;
using ValueType = Velentr.Input.Enums.ValueType;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class GamePadLogicCondition : LogicCondition
    {

        protected GamePadLogicCondition(InputManager manager, GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode sensorValueMode, ValueLogic logicValue, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.GamePad, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
        {
            Sensor = sensor;
            PlayerIndex = playerIndex;
            InputMode = inputMode;
            SensorValueMode = sensorValueMode;

            if (InputMode != GamePadInputMode.SingleGamePad && SensorValueMode == GamePadSensorValueMode.SingleGamePad)
            {
                throw new Exception("SensorValueMode must not be GamePadSensorValueMode.SingleGamePad when InputMode is set to GamePadInputMode.SingleGamePad!");
            }

            logicValue.Value.Validate();
            if ((Sensor == GamePadSensor.LeftStick || Sensor == GamePadSensor.RightStick) && logicValue.Value.Type != ValueType.Vector2)
            {
                throw new Exception("logicValue contains an invalid type for GamePadSensor.LeftStick and GamePadSensor.RightStick, you must use a ValueType.Vector2!");
            }

            if ((Sensor == GamePadSensor.LeftTrigger || Sensor == GamePadSensor.RightTrigger) && logicValue.Value.Type != ValueType.Float)
            {
                throw new Exception("logicValue contains an invalid type for GamePadSensor.LeftTrigger and GamePadSensor.RightTrigger, you must use a ValueType.Float!");
            }
        }

        public GamePadSensor Sensor { get; }

        public int PlayerIndex { get; protected set; }

        public GamePadInputMode InputMode { get; protected set; }

        public GamePadSensorValueMode SensorValueMode { get; protected set; }

        public override ConditionEventArguments GetArguments()
        {
            switch (Sensor)
            {
                case GamePadSensor.RightStick:
                case GamePadSensor.LeftStick:
                    return new GamePadStickSensorEventArguments
                    {
                        Sensor = Sensor,
                        PlayerIndex = PlayerIndex,
                        InputMode = InputMode,
                        SensorValueMode = SensorValueMode,
                        Condition = this,
                        InputSource = InputSource,
                        MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                        ConditionStateStartTime = CurrentStateStart,
                        ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
                        WindowMustBeActive = WindowMustBeActive,
                        Delta = Manager.GamePad.GetStickDelta(Sensor, PlayerIndex, InputMode, SensorValueMode),
                        CurrentPosition = Manager.GamePad.GetStickPosition(Sensor, PlayerIndex, InputMode, SensorValueMode)
                    };
                case GamePadSensor.LeftTrigger:
                case GamePadSensor.RightTrigger:
                    return new GamePadTriggerSensorEventArguments
                    {
                        Sensor = Sensor,
                        PlayerIndex = PlayerIndex,
                        InputMode = InputMode,
                        SensorValueMode = SensorValueMode,
                        Condition = this,
                        InputSource = InputSource,
                        MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                        ConditionStateStartTime = CurrentStateStart,
                        ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
                        WindowMustBeActive = WindowMustBeActive,
                        Delta = Manager.GamePad.GetTriggerDelta(Sensor, PlayerIndex, InputMode, SensorValueMode),
                        CurrentPosition = Manager.GamePad.GetTriggerPosition(Sensor, PlayerIndex, InputMode, SensorValueMode)
                    };
            }

            return null;
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (
                ((WindowMustBeActive && Manager.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime) >= milliSecondsForConditionMet)
            );
        }

        public override void Consume()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    Manager.GamePad.ConsumeSensor(Sensor, PlayerIndex);
                    break;
                case GamePadInputMode.AnyGamePad:
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        Manager.GamePad.ConsumeSensor(Sensor, Manager.GamePad.ConnectedGamePadIndexes[i]);
                    }

                    break;
            }
        }

        public override bool IsConsumed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return Manager.GamePad.IsSensorConsumed(Sensor, PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (Manager.GamePad.IsSensorConsumed(Sensor, Manager.GamePad.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!Manager.GamePad.IsSensorConsumed(Sensor, Manager.GamePad.ConnectedGamePadIndexes[i]))
                        {
                            return false;
                        }
                    }

                    return true;
            }

            return false;
        }

    }

}
