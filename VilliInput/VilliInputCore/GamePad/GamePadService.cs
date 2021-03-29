using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using VilliInput.Enums;

namespace VilliInput.GamePad
{

    public class GamePadService : InputService
    {

        internal static Dictionary<GamePadButton, Func<GamePadState, ButtonState>> ButtonMapping = new Dictionary<GamePadButton, Func<GamePadState, ButtonState>>(Enum.GetNames(typeof(GamePadButton)).Length)
        {
            {GamePadButton.A, state => state.Buttons.A},
            {GamePadButton.B, state => state.Buttons.B},
            {GamePadButton.X, state => state.Buttons.X},
            {GamePadButton.Y, state => state.Buttons.Y},
            {GamePadButton.Back, state => state.Buttons.Back},
            {GamePadButton.Start, state => state.Buttons.Start},
            {GamePadButton.LeftShoulder, state => state.Buttons.LeftShoulder},
            {GamePadButton.RightShoulder, state => state.Buttons.RightShoulder},
            {GamePadButton.LeftStick, state => state.Buttons.LeftStick},
            {GamePadButton.RightStick, state => state.Buttons.RightStick},
            {GamePadButton.BigButton, state => state.Buttons.BigButton},
            {GamePadButton.DPadDown, state => state.DPad.Down},
            {GamePadButton.DPadLeft, state => state.DPad.Left},
            {GamePadButton.DPadRight, state => state.DPad.Right},
            {GamePadButton.DPadUp, state => state.DPad.Up}
        };

        public static VilliGamePad[] GamePads;

        public static List<int> ConnectedGamePadIndexes;

        public Dictionary<(int, GamePadButton), ulong> ButtonLastConsumed = new Dictionary<(int, GamePadButton), ulong>();

        public GameTime LastConnectionCheckTime;

        public Dictionary<(int, GamePadSensor), ulong> SensorConsumed = new Dictionary<(int, GamePadSensor), ulong>();

        public GamePadService()
        {
            Source = InputSource.GamePad;
        }

        public static int MaximumGamePads => Microsoft.Xna.Framework.Input.GamePad.MaximumGamePadCount;

        public static int HighestGamePadIndex => Microsoft.Xna.Framework.Input.GamePad.MaximumGamePadCount - 1;

        public override void Setup()
        {
            GamePads = new VilliGamePad[MaximumGamePads];
            ConnectedGamePadIndexes = new List<int>(MaximumGamePads);

            for (var i = 0; i < MaximumGamePads; i++)
            {
                GamePads[i] = new VilliGamePad
                {
                    PlayerIndex = i,
                    PreviousState = GamePadState.Default,
                    CurrentState = GamePadState.Default,
                    Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i),
                    DeadZone = Villi.System.Settings.DefaultGamePadDeadZone
                };

                if (GamePads[i].IsConnected)
                {
                    ConnectedGamePadIndexes.Add(i);
                }
            }
        }

        public void CheckForConnectedGamePads()
        {
            ConnectedGamePadIndexes.Clear();
            for (var i = 0; i < MaximumGamePads; i++)
            {
                GamePads[i].PreviousState = GamePads[i].CurrentState;
                GamePads[i].CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(i, GamePads[i].DeadZone);
                GamePads[i].Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i);
            }

            LastConnectionCheckTime = Villi.CurrentTime;
        }

        public override void Update()
        {
            // check if we need to check for if any controllers have been connected to the system
            if (Villi.System.Settings.SecondsBetweenGamePadConnectionCheck > 0)
            {
                var elapsedSecondsBetweenChecks = LastConnectionCheckTime == null
                    ? Villi.System.Settings.SecondsBetweenGamePadConnectionCheck
                    : (Villi.CurrentTime.TotalGameTime - LastConnectionCheckTime.TotalGameTime).TotalSeconds;

                if (elapsedSecondsBetweenChecks >= Villi.System.Settings.SecondsBetweenGamePadConnectionCheck)
                {
                    ConnectedGamePadIndexes.Clear();
                    for (var i = 0; i < GamePads.Length; i++)
                    {
                        GamePads[i].Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i);

                        if (GamePads[i].IsConnected)
                        {
                            ConnectedGamePadIndexes.Add(i);
                        }
                    }
                }
            }

            // update controller state
            for (var i = 0; i < GamePads.Length; i++)
            {
                if (GamePads[i].IsConnected)
                {
                    GamePads[i].PreviousState = GamePads[i].CurrentState;
                    GamePads[i].CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(i, GamePads[i].DeadZone);
                }
            }
        }

        public static void ValidatePlayerIndex(int playerIndex)
        {
            if (playerIndex < 0 || playerIndex > HighestGamePadIndex)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }
        }


        public void SetGamePadDeadZone(int playerIndex, GamePadDeadZone deadZone)
        {
            ValidatePlayerIndex(playerIndex);

            GamePads[playerIndex].DeadZone = deadZone;
        }


        public void ConsumeButton(GamePadButton button, int playerIndex = 0)
        {
            ValidatePlayerIndex(playerIndex);

            ButtonLastConsumed[(playerIndex, button)] = Villi.CurrentFrame;
        }

        public void ConsumeSensor(GamePadSensor sensor, int playerIndex = 0)
        {
            ValidatePlayerIndex(playerIndex);

            SensorConsumed[(playerIndex, sensor)] = Villi.CurrentFrame;
        }

        public bool IsButtonConsumed(GamePadButton button, int playerIndex = 0)
        {
            ValidatePlayerIndex(playerIndex);

            if (!GamePads[playerIndex].IsConnected)
            {
                return false;
            }

            if (ButtonLastConsumed.TryGetValue((playerIndex, button), out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }

        public bool IsSensorConsumed(GamePadSensor sensor, int playerIndex = 0)
        {
            ValidatePlayerIndex(playerIndex);

            if (!GamePads[playerIndex].IsConnected)
            {
                return false;
            }

            if (SensorConsumed.TryGetValue((playerIndex, sensor), out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }


        public bool SetVibration(int playerIndex, float leftMotor, float rightMotor)
        {
            ValidatePlayerIndex(playerIndex);

            if (!GamePads[playerIndex].IsConnected)
            {
                return false;
            }

            return Microsoft.Xna.Framework.Input.GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
        }


        public static bool IsButtonPressed(GamePadButton button, int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return ButtonMapping[button](GamePads[playerIndex].CurrentState) == ButtonState.Pressed;
        }

        public static bool WasButtonPressed(GamePadButton button, int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return ButtonMapping[button](GamePads[playerIndex].PreviousState) == ButtonState.Pressed;
        }

        public static bool IsButtonReleased(GamePadButton button, int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return ButtonMapping[button](GamePads[playerIndex].CurrentState) == ButtonState.Released;
        }

        public static bool WasButtonReleased(GamePadButton button, int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return ButtonMapping[button](GamePads[playerIndex].PreviousState) == ButtonState.Released;
        }

        private static bool InternalIsButtonHelper(GamePadButton button, int playerIndex, ButtonState state)
        {
            ValidatePlayerIndex(playerIndex);

            return ButtonMapping[button](GamePads[playerIndex].CurrentState) == state;
        }

        public static Vector2 CurrentLeftStick(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.ThumbSticks.Left;
        }

        public static Vector2 PreviousLeftStick(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].PreviousState.ThumbSticks.Left;
        }

        public static Vector2 CurrentRightStick(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.ThumbSticks.Right;
        }

        public static Vector2 PreviousRightStick(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].PreviousState.ThumbSticks.Right;
        }

        public static float CurrentLeftTrigger(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.Triggers.Left;
        }

        public static float PreviousLeftTrigger(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].PreviousState.Triggers.Left;
        }

        public static float CurrentRightTrigger(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.Triggers.Right;
        }

        public static float PreviousRightTrigger(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].PreviousState.Triggers.Right;
        }

        public static bool SensorMoved(GamePadSensor sensor, int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            switch (sensor)
            {
                case GamePadSensor.LeftStick:
                    return LeftStickDelta(playerIndex) != Vector2.Zero;
                case GamePadSensor.LeftTrigger:
                    return LeftTriggerDelta(playerIndex) != 0f;
                case GamePadSensor.RightStick:
                    return RightStickDelta(playerIndex) != Vector2.Zero;
                case GamePadSensor.RightTrigger:
                    return RightTriggerDelta(playerIndex) != 0f;
            }

            return false;
        }

        public static Vector2 LeftStickDelta(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.ThumbSticks.Left - GamePads[playerIndex].PreviousState.ThumbSticks.Left;
        }

        public static Vector2 RightStickDelta(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.ThumbSticks.Right - GamePads[playerIndex].PreviousState.ThumbSticks.Right;
        }

        public static float LeftTriggerDelta(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.Triggers.Left - GamePads[playerIndex].PreviousState.Triggers.Left;
        }

        public static float RightTriggerDelta(int playerIndex)
        {
            ValidatePlayerIndex(playerIndex);

            return GamePads[playerIndex].CurrentState.Triggers.Right - GamePads[playerIndex].PreviousState.Triggers.Right;
        }

        public static Vector2 GetStickDelta(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            ValidatePlayerIndex(playerIndex);

            if (sensor == GamePadSensor.LeftTrigger || sensor == GamePadSensor.RightTrigger)
            {
                throw new Exception(Constants.InvalidGamePadStickSensor);
            }

            // If we're only working with a single gamepad, let's exit early...
            if (inputMode == GamePadInputMode.SingleGamePad)
            {
                switch (sensor)
                {
                    case GamePadSensor.LeftStick:
                        return LeftStickDelta(playerIndex);
                    case GamePadSensor.RightStick:
                        return RightStickDelta(playerIndex);
                }
            }
            // otherwise, we'll now need to determine out output based on the SensorValueMode that is requested...
            else
            {
                var output = Vector2.Zero;

                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        output = sensor == GamePadSensor.LeftStick
                            ? LeftStickDelta(ConnectedGamePadIndexes[0])
                            : RightStickDelta(ConnectedGamePadIndexes[0]);
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftStick
                            ? LeftStickDelta(ConnectedGamePadIndexes.Count - 1)
                            : RightStickDelta(ConnectedGamePadIndexes.Count - 1);
                        break;
                    default:
                        for (var i = 0; i < ConnectedGamePadIndexes.Count; i++)
                        {
                            Vector2 indexValue;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.MaxX:
                                case GamePadSensorValueMode.MaxY:
                                case GamePadSensorValueMode.Max:
                                    indexValue = new Vector2(float.MinValue);
                                    break;
                                case GamePadSensorValueMode.MinX:
                                case GamePadSensorValueMode.MinY:
                                case GamePadSensorValueMode.Min:
                                    indexValue = new Vector2(float.MaxValue);
                                    break;
                                default:
                                    indexValue = sensor == GamePadSensor.LeftStick
                                        ? LeftStickDelta(i)
                                        : RightStickDelta(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += indexValue;
                                    break;
                                case GamePadSensorValueMode.MaxX:
                                    if (indexValue.X > output.X)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MaxY:
                                    if (indexValue.Y > output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.Max:
                                    if (indexValue.X > output.X || indexValue.Y > output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MinX:
                                    if (indexValue.X < output.X)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MinY:
                                    if (indexValue.Y < output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.Min:
                                    if (indexValue.X < output.X || indexValue.Y < output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                            }
                        }

                        break;
                }

                if (valueConditionMode == GamePadSensorValueMode.Average)
                {
                    output /= ConnectedGamePadIndexes.Count;
                }

                return output;
            }

            return Vector2.Zero;
        }

        public static float GetTriggerDelta(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            ValidatePlayerIndex(playerIndex);

            if (sensor == GamePadSensor.LeftStick || sensor == GamePadSensor.RightStick)
            {
                throw new Exception(Constants.InvalidGamePadTriggerSensor);
            }

            // If we're only working with a single gamepad, let's exit early...
            if (inputMode == GamePadInputMode.SingleGamePad)
            {
                switch (sensor)
                {
                    case GamePadSensor.LeftStick:
                        return LeftTriggerDelta(playerIndex);
                    case GamePadSensor.RightStick:
                        return RightTriggerDelta(playerIndex);
                }
            }
            // otherwise, we'll now need to determine out output based on the SensorValueMode that is requested...
            else
            {
                var output = 0f;

                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        output = sensor == GamePadSensor.LeftTrigger
                            ? LeftTriggerDelta(ConnectedGamePadIndexes[0])
                            : RightTriggerDelta(ConnectedGamePadIndexes[0]);
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftTrigger
                            ? LeftTriggerDelta(ConnectedGamePadIndexes.Count - 1)
                            : RightTriggerDelta(ConnectedGamePadIndexes.Count - 1);
                        break;
                    default:
                        for (var i = 0; i < ConnectedGamePadIndexes.Count; i++)
                        {
                            float indexValue;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.MaxX:
                                case GamePadSensorValueMode.MaxY:
                                case GamePadSensorValueMode.Max:
                                    indexValue = float.MinValue;
                                    break;
                                case GamePadSensorValueMode.MinX:
                                case GamePadSensorValueMode.MinY:
                                case GamePadSensorValueMode.Min:
                                    indexValue = float.MaxValue;
                                    break;
                                default:
                                    indexValue = sensor == GamePadSensor.LeftTrigger
                                        ? LeftTriggerDelta(i)
                                        : RightTriggerDelta(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += indexValue;
                                    break;
                                case GamePadSensorValueMode.MaxX:
                                case GamePadSensorValueMode.MaxY:
                                case GamePadSensorValueMode.Max:
                                    if (indexValue > output)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MinX:
                                case GamePadSensorValueMode.MinY:
                                case GamePadSensorValueMode.Min:
                                    if (indexValue < output)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                            }
                        }

                        break;
                }

                if (valueConditionMode == GamePadSensorValueMode.Average)
                {
                    output /= ConnectedGamePadIndexes.Count;
                }

                return output;
            }

            return float.MinValue;
        }

        public static Vector2 GetStickPosition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            ValidatePlayerIndex(playerIndex);

            if (sensor == GamePadSensor.LeftTrigger || sensor == GamePadSensor.RightTrigger)
            {
                throw new Exception(Constants.InvalidGamePadStickSensor);
            }

            // If we're only working with a single gamepad, let's exit early...
            if (inputMode == GamePadInputMode.SingleGamePad)
            {
                switch (sensor)
                {
                    case GamePadSensor.LeftStick:
                        return CurrentLeftStick(playerIndex);
                    case GamePadSensor.RightStick:
                        return CurrentRightStick(playerIndex);
                }
            }
            // otherwise, we'll now need to determine out output based on the SensorValueMode that is requested...
            else
            {
                var output = Vector2.Zero;

                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        output = sensor == GamePadSensor.LeftStick
                            ? CurrentLeftStick(ConnectedGamePadIndexes[0])
                            : CurrentRightStick(ConnectedGamePadIndexes[0]);
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftStick
                            ? CurrentLeftStick(ConnectedGamePadIndexes.Count - 1)
                            : CurrentRightStick(ConnectedGamePadIndexes.Count - 1);
                        break;
                    default:
                        for (var i = 0; i < ConnectedGamePadIndexes.Count; i++)
                        {
                            Vector2 indexValue;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.MaxX:
                                case GamePadSensorValueMode.MaxY:
                                case GamePadSensorValueMode.Max:
                                    indexValue = new Vector2(float.MinValue);
                                    break;
                                case GamePadSensorValueMode.MinX:
                                case GamePadSensorValueMode.MinY:
                                case GamePadSensorValueMode.Min:
                                    indexValue = new Vector2(float.MaxValue);
                                    break;
                                default:
                                    indexValue = sensor == GamePadSensor.LeftStick
                                        ? CurrentLeftStick(i)
                                        : CurrentRightStick(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += indexValue;
                                    break;
                                case GamePadSensorValueMode.MaxX:
                                    if (indexValue.X > output.X)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MaxY:
                                    if (indexValue.Y > output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.Max:
                                    if (indexValue.X > output.X || indexValue.Y > output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MinX:
                                    if (indexValue.X < output.X)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MinY:
                                    if (indexValue.Y < output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.Min:
                                    if (indexValue.X < output.X || indexValue.Y < output.Y)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                            }
                        }

                        break;
                }

                if (valueConditionMode == GamePadSensorValueMode.Average)
                {
                    output /= ConnectedGamePadIndexes.Count;
                }

                return output;
            }

            return Vector2.Zero;
        }

        public static float GetTriggerPosition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            ValidatePlayerIndex(playerIndex);

            if (sensor == GamePadSensor.LeftStick || sensor == GamePadSensor.RightStick)
            {
                throw new Exception(Constants.InvalidGamePadTriggerSensor);
            }

            // If we're only working with a single gamepad, let's exit early...
            if (inputMode == GamePadInputMode.SingleGamePad)
            {
                switch (sensor)
                {
                    case GamePadSensor.LeftStick:
                        return CurrentLeftTrigger(playerIndex);
                    case GamePadSensor.RightStick:
                        return CurrentRightTrigger(playerIndex);
                }
            }
            // otherwise, we'll now need to determine out output based on the SensorValueMode that is requested...
            else
            {
                var output = 0f;

                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        output = sensor == GamePadSensor.LeftTrigger
                            ? CurrentLeftTrigger(ConnectedGamePadIndexes[0])
                            : CurrentRightTrigger(ConnectedGamePadIndexes[0]);
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftTrigger
                            ? LeftTriggerDelta(ConnectedGamePadIndexes.Count - 1)
                            : CurrentRightTrigger(ConnectedGamePadIndexes.Count - 1);
                        break;
                    default:
                        for (var i = 0; i < ConnectedGamePadIndexes.Count; i++)
                        {
                            float indexValue;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.MaxX:
                                case GamePadSensorValueMode.MaxY:
                                case GamePadSensorValueMode.Max:
                                    indexValue = float.MinValue;
                                    break;
                                case GamePadSensorValueMode.MinX:
                                case GamePadSensorValueMode.MinY:
                                case GamePadSensorValueMode.Min:
                                    indexValue = float.MaxValue;
                                    break;
                                default:
                                    indexValue = sensor == GamePadSensor.LeftTrigger
                                        ? CurrentLeftTrigger(i)
                                        : CurrentRightTrigger(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += indexValue;
                                    break;
                                case GamePadSensorValueMode.MaxX:
                                case GamePadSensorValueMode.MaxY:
                                case GamePadSensorValueMode.Max:
                                    if (indexValue > output)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                                case GamePadSensorValueMode.MinX:
                                case GamePadSensorValueMode.MinY:
                                case GamePadSensorValueMode.Min:
                                    if (indexValue < output)
                                    {
                                        output = indexValue;
                                    }

                                    break;
                            }
                        }

                        break;
                }

                if (valueConditionMode == GamePadSensorValueMode.Average)
                {
                    output /= ConnectedGamePadIndexes.Count;
                }

                return output;
            }

            return float.MinValue;
        }

    }

}
