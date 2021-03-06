using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.GamePad;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class GamePadSensorBooleanCondition : BooleanCondition
    {

        protected GamePadSensorBooleanCondition(InputManager manager, GamePadSensor sensor, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, GamePadSensorValueMode sensorValueMode = GamePadSensorValueMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.GamePad, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
        {
            Sensor = sensor;
            PlayerIndex = playerIndex;
            InputMode = inputMode;
            SensorValueMode = sensorValueMode;

            if (InputMode != GamePadInputMode.SingleGamePad && SensorValueMode == GamePadSensorValueMode.SingleGamePad)
            {
                throw new Exception("SensorValueMode must not be GamePadSensorValueMode.SingleGamePad when InputMode is set to GamePadInputMode.SingleGamePad!");
            }

            if (manager.Settings.ThrowWhenCreatingConditionIfNoServiceEnabled && manager.GamePad == null)
            {
                throw new Exception(Constants.NoEngineConfiguredError);
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

        protected abstract bool InternalCurrent(int playerIndex);

        protected abstract bool InternalPrevious(int playerIndex);

        protected override bool CurrentStateValid()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return InternalCurrent(PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < Manager.GamePad.GetConnectedGamePadCount(); i++)
                    {
                        if (InternalCurrent(Manager.GamePad.GetIndexForConnectedGamePad(i)))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.GetConnectedGamePadCount(); i++)
                    {
                        if (!InternalCurrent(Manager.GamePad.GetIndexForConnectedGamePad(i)))
                        {
                            return false;
                        }
                    }

                    return true;
            }

            return false;
        }

        protected override bool PreviousStateValid()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return InternalPrevious(PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < Manager.GamePad.GetConnectedGamePadCount(); i++)
                    {
                        if (InternalPrevious(Manager.GamePad.GetIndexForConnectedGamePad(i)))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.GetConnectedGamePadCount(); i++)
                    {
                        if (!InternalPrevious(Manager.GamePad.GetIndexForConnectedGamePad(i)))
                        {
                            return false;
                        }
                    }

                    return true;
            }

            return false;
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
                    for (var i = 0; i < Manager.GamePad.GetConnectedGamePadCount(); i++)
                    {
                        Manager.GamePad.ConsumeSensor(Sensor, Manager.GamePad.GetIndexForConnectedGamePad(i));
                    }

                    break;
            }
        }

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (
                ((WindowMustBeActive && Manager.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime) >= milliSecondsForConditionMet)
                && (MilliSecondsForTimeOut == 0 || Helper.ElapsedMilliSeconds(LastFireTime, Manager.CurrentTime) >= MilliSecondsForTimeOut)
            );
        }

        public override bool IsConsumed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return Manager.GamePad.IsSensorConsumed(Sensor, PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < Manager.GamePad.GetConnectedGamePadCount(); i++)
                    {
                        if (Manager.GamePad.IsSensorConsumed(Sensor, Manager.GamePad.GetIndexForConnectedGamePad(i)))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.GetConnectedGamePadCount(); i++)
                    {
                        if (!Manager.GamePad.IsSensorConsumed(Sensor, Manager.GamePad.GetIndexForConnectedGamePad(i)))
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
