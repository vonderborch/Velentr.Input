using System;
using VilliInput.OLD.Conditions.Internal;
using VilliInput.OLD.EventArguments;
using VilliInput.OLD.GamePadInput;

namespace VilliInput.OLD.Conditions
{
    public class GamePadSensorCondition : SensorCondition
    {
        public GamePadSensor Sensor { get; private set; }

        public int PlayerIndex { get; private set; }

        public GamePadInputMode InputMode { get; private set; }

        public GamePadSensorValueMode ValueConditionMode { get; private set; }

        public GamePadSensorCondition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, GamePadSensorValueMode valueConditionMode = GamePadSensorValueMode.Average, bool windowMustBeActive = true, uint secondsForPressed = 0, uint secondsForReleased = 0, InputValueLogic? inputValueComparator = null) : base(InputSource.GamePad, windowMustBeActive, secondsForPressed, secondsForReleased, inputValueComparator)
        {
            Sensor = sensor;
            switch (inputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    if (playerIndex < 0 || playerIndex > GamePadHelpers.HighestGamePadIndex)
                    {
                        throw new Exception(Constants.PlayerIndexExceptionMessage);
                    }

                    break;
                case GamePadInputMode.AllGamePads:
                case GamePadInputMode.AnyGamePad:
                    playerIndex = -1;
                    break;
            }

            PlayerIndex = playerIndex;
            InputMode = inputMode;
            ValueConditionMode = valueConditionMode;
        }

        protected override bool ActionValid(bool ignoredConsumed, uint actionTime)
        {
            return (!WindowMustBeActive || Villi.IsWindowActive)
                   && (ignoredConsumed || Villi.System.GamePad.IsSensorConsumed(Sensor))
                   && (actionTime == 0 || Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime) >= actionTime);
        }

        public override bool IsPressed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return GamePadHelpers.SensorMoved(Sensor, PlayerIndex);
                case GamePadInputMode.AllGamePads:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!GamePadHelpers.SensorMoved(Sensor, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                case GamePadInputMode.AnyGamePad:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (GamePadHelpers.SensorMoved(Sensor, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        public override bool IsReleased()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return !GamePadHelpers.SensorMoved(Sensor, PlayerIndex);
                case GamePadInputMode.AllGamePads:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (GamePadHelpers.SensorMoved(Sensor, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                case GamePadInputMode.AnyGamePad:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!GamePadHelpers.SensorMoved(Sensor, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        public override InputValue GetInputValue()
        {
            switch (Sensor)
            {
                case GamePadSensor.LeftStick:
                case GamePadSensor.RightStick:
                    return new InputValue(valueVector2: GamePadHelpers.GetStickDelta(Sensor, PlayerIndex, InputMode, ValueConditionMode));
                case GamePadSensor.LeftTrigger:
                case GamePadSensor.RightTrigger:
                    return new InputValue(valueFloat: GamePadHelpers.GetTriggerDelta(Sensor, PlayerIndex, InputMode, ValueConditionMode));
            }

            return new InputValue();
        }

        public override void Consume()
        {
            if (InputMode != GamePadInputMode.SingleGamePad)
            {
                for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                {
                    Villi.System.GamePad.ConsumeSensor(Sensor, GamePadService.ConnectedGamePadIndexes[i]);
                }
            }
            else
            {
                Villi.System.GamePad.ConsumeSensor(Sensor, PlayerIndex);
            }
        }

        internal override VilliEventArguments GetArguments()
        {
            switch (Sensor)
            {
                case GamePadSensor.LeftStick:
                case GamePadSensor.RightStick:
                    return new GamePadStickSensorEventArguments()
                    {
                        Sensor = this.Sensor,
                        PlayerIndex = this.PlayerIndex,
                        InputMode = this.InputMode,
                        ConditionSource = this,
                        InputSource = this.Source,
                        SecondsForPressed = this.SecondsForPressed,
                        SecondsForReleased = this.SecondsForReleased,
                        ConditionState = this.CurrentState,
                        ConditionStateStartTime = this.CurrentStateStart,
                        ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                        WindowMustBeActive = this.WindowMustBeActive,
                        Delta = GamePadHelpers.GetStickDelta(Sensor, PlayerIndex, InputMode, ValueConditionMode),
                        CurrentPosition = GamePadHelpers.GetStickPosition(Sensor, PlayerIndex, InputMode, ValueConditionMode),
                    };
                case GamePadSensor.LeftTrigger:
                case GamePadSensor.RightTrigger:
                    return new GamePadTriggerSensorEventArguments()
                    {
                        Sensor = this.Sensor,
                        PlayerIndex = this.PlayerIndex,
                        InputMode = this.InputMode,
                        ConditionSource = this,
                        InputSource = this.Source,
                        SecondsForPressed = this.SecondsForPressed,
                        SecondsForReleased = this.SecondsForReleased,
                        ConditionState = this.CurrentState,
                        ConditionStateStartTime = this.CurrentStateStart,
                        ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                        WindowMustBeActive = this.WindowMustBeActive,
                        Delta = GamePadHelpers.GetTriggerDelta(Sensor, PlayerIndex, InputMode, ValueConditionMode),
                        CurrentPosition = GamePadHelpers.GetTriggerPosition(Sensor, PlayerIndex, InputMode, ValueConditionMode),
                    };
            }

            return null;
        }
    }
}
