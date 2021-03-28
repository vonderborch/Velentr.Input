using System;
using Microsoft.Xna.Framework;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.GamePad;
using VilliInput.Helpers;
using ValueType = VilliInput.Enums.ValueType;

namespace VilliInput.Conditions.Internal
{
    public abstract class GamePadLogicCondition : LogicCondition
    {
        public GamePadSensor Sensor { get; private set; }

        public int PlayerIndex { get; protected set; }

        public GamePadInputMode InputMode { get; protected set; }

        public GamePadSensorValueMode SensorValueMode { get; protected set; }

        protected GamePadLogicCondition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode sensorValueMode, ValueLogic logicValue, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(InputSource.GamePad, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
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
            else if ((Sensor == GamePadSensor.LeftTrigger || Sensor == GamePadSensor.RightTrigger) && logicValue.Value.Type != ValueType.Float)
            {
                throw new Exception("logicValue contains an invalid type for GamePadSensor.LeftTrigger and GamePadSensor.RightTrigger, you must use a ValueType.Float!");
            }
        }

        public override VilliEventArguments GetArguments()
        {
            switch (Sensor)
            {
                case GamePadSensor.RightStick:
                case GamePadSensor.LeftStick:
                    return new GamePadStickSensorEventArguments()
                    {
                        Sensor = this.Sensor,
                        PlayerIndex = this.PlayerIndex,
                        InputMode = this.InputMode,
                        SensorValueMode = this.SensorValueMode,
                        Condition = this,
                        InputSource = this.InputSource,
                        MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                        ConditionStateStartTime = this.CurrentStateStart,
                        ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                        WindowMustBeActive = this.WindowMustBeActive,
                        Delta = GamePadService.GetStickDelta(Sensor, PlayerIndex, InputMode, this.SensorValueMode),
                        CurrentPosition = GamePadService.GetStickPosition(Sensor, PlayerIndex, InputMode, this.SensorValueMode),
                    };
                case GamePadSensor.LeftTrigger:
                case GamePadSensor.RightTrigger:
                    return new GamePadTriggerSensorEventArguments()
                    {
                        Sensor = this.Sensor,
                        PlayerIndex = this.PlayerIndex,
                        InputMode = this.InputMode,
                        SensorValueMode = this.SensorValueMode,
                        Condition = this,
                        InputSource = this.InputSource,
                        MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                        ConditionStateStartTime = this.CurrentStateStart,
                        ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                        WindowMustBeActive = this.WindowMustBeActive,
                        Delta = GamePadService.GetTriggerDelta(Sensor, PlayerIndex, InputMode, this.SensorValueMode),
                        CurrentPosition = GamePadService.GetTriggerPosition(Sensor, PlayerIndex, InputMode, this.SensorValueMode),
                    };
            }

            return null;
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return ((!WindowMustBeActive || Villi.IsWindowActive)
                    && (allowedIfConsumed || IsConsumed())
                    && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet));
        }

        public override void Consume()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    Villi.System.GamePad.ConsumeSensor(Sensor, PlayerIndex);
                    break;
                case GamePadInputMode.AnyGamePad:
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        Villi.System.GamePad.ConsumeSensor(Sensor, GamePadService.ConnectedGamePadIndexes[i]);
                    }

                    break;
            }
        }

        public override bool IsConsumed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return Villi.System.GamePad.IsSensorConsumed(Sensor, PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (Villi.System.GamePad.IsSensorConsumed(Sensor, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!Villi.System.GamePad.IsSensorConsumed(Sensor, GamePadService.ConnectedGamePadIndexes[i]))
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
