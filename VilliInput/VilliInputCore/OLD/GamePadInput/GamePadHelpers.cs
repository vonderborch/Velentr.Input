using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VilliInput.OLD.GamePadInput
{
    public static class GamePadHelpers
    {
        internal static Dictionary<GamePadButton, Func<GamePadState, Microsoft.Xna.Framework.Input.ButtonState>> ButtonMapping = new Dictionary<GamePadButton, Func<GamePadState, Microsoft.Xna.Framework.Input.ButtonState>>(Enum.GetNames(typeof(GamePadButton)).Length)
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
            {GamePadButton.DPadUp, state => state.DPad.Up},
        };

        public static int MaximumGamePads => GamePad.MaximumGamePadCount;

        public static int HighestGamePadIndex => GamePad.MaximumGamePadCount - 1;

        public static bool IsButtonPressed(GamePadButton button, int playerIndex)
        {
            return ButtonMapping[button](GamePadService.GamePads[playerIndex].CurrentState) == ButtonState.Pressed;
        }

        public static bool WasButtonPressed(GamePadButton button, int playerIndex)
        {
            return ButtonMapping[button](GamePadService.GamePads[playerIndex].PreviousState) == ButtonState.Pressed;
        }

        public static bool IsButtonReleased(GamePadButton button, int playerIndex)
        {
            return ButtonMapping[button](GamePadService.GamePads[playerIndex].CurrentState) == ButtonState.Released;
        }

        public static bool WasButtonReleased(GamePadButton button, int playerIndex)
        {
            return ButtonMapping[button](GamePadService.GamePads[playerIndex].PreviousState) == ButtonState.Released;
        }

        private static bool InternalIsButtonHelper(GamePadButton button, int playerIndex, ButtonState state)
        {
            return ButtonMapping[button](GamePadService.GamePads[playerIndex].CurrentState) == state;
        }

        public static Vector2 CurrentLeftStick(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.ThumbSticks.Left;
        }

        public static Vector2 PreviousLeftStick(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].PreviousState.ThumbSticks.Left;
        }

        public static Vector2 CurrentRightStick(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.ThumbSticks.Right;
        }

        public static Vector2 PreviousRightStick(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].PreviousState.ThumbSticks.Right;
        }




        public static float CurrentLeftTrigger(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.Triggers.Left;
        }

        public static float PreviousLeftTrigger(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].PreviousState.Triggers.Left;
        }

        public static float CurrentRightTrigger(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.Triggers.Right;
        }

        public static float PreviousRightTrigger(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].PreviousState.Triggers.Right;
        }


        public static bool SensorMoved(GamePadSensor sensor, int playerIndex)
        {
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
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.ThumbSticks.Left - GamePadService.GamePads[playerIndex].PreviousState.ThumbSticks.Left;
        }

        public static Vector2 RightStickDelta(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.ThumbSticks.Right - GamePadService.GamePads[playerIndex].PreviousState.ThumbSticks.Right;
        }

        public static float LeftTriggerDelta(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.Triggers.Left - GamePadService.GamePads[playerIndex].PreviousState.Triggers.Left;
        }

        public static float RightTriggerDelta(int playerIndex)
        {
            if (playerIndex > HighestGamePadIndex || playerIndex < 0)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            return GamePadService.GamePads[playerIndex].CurrentState.Triggers.Right - GamePadService.GamePads[playerIndex].PreviousState.Triggers.Right;
        }

        public static Vector2 GetStickDelta(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            if (sensor == GamePadSensor.LeftStick || sensor == GamePadSensor.RightStick)
            {
                throw new Exception("Invalid sensor!");
            }


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
            else
            {
                var output = Vector2.Zero;
                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        if (sensor == GamePadSensor.LeftStick)
                        {
                            output = LeftStickDelta(GamePadService.ConnectedGamePadIndexes[0]);
                        }
                        else
                        {
                            output = RightStickDelta(GamePadService.ConnectedGamePadIndexes[0]);
                        }

                        break;
                    case GamePadSensorValueMode.Last:
                        if (sensor == GamePadSensor.LeftStick)
                        {
                            output = LeftStickDelta(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }
                        else
                        {
                            output = RightStickDelta(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }

                        break;
                    default:
                        for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                        {
                            Vector2 value = Vector2.Zero;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Max:
                                    value = new Vector2(float.MinValue);
                                    break;
                                case GamePadSensorValueMode.Min:
                                    value = new Vector2(float.MaxValue);
                                    break;
                            }

                            switch (sensor)
                            {
                                case GamePadSensor.LeftStick:
                                    value = LeftStickDelta(i);
                                    break;
                                case GamePadSensor.RightStick:
                                    value = RightStickDelta(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += value;
                                    break;
                                case GamePadSensorValueMode.Max:
                                    if (value.X > output.X || value.Y > output.Y)
                                    {
                                        output = value;
                                    }
                                    break;
                                case GamePadSensorValueMode.Min:
                                    if (value.X < output.X || value.Y < output.Y)
                                    {
                                        output = value;
                                    }
                                    break;
                            }
                        }

                        break;
                }


                return output;
            }

            return Vector2.Zero;
        }

        public static float GetTriggerDelta(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            if (sensor == GamePadSensor.LeftStick || sensor == GamePadSensor.RightStick)
            {
                throw new Exception("Invalid sensor!");
            }


            if (inputMode == GamePadInputMode.SingleGamePad)
            {
                switch (sensor)
                {
                    case GamePadSensor.LeftTrigger:
                        return LeftTriggerDelta(playerIndex);
                    case GamePadSensor.RightTrigger:
                        return RightTriggerDelta(playerIndex);
                }
            }
            else
            {
                var output = 0f;
                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        if (sensor == GamePadSensor.LeftTrigger)
                        {
                            output = LeftTriggerDelta(GamePadService.ConnectedGamePadIndexes[0]);
                        }
                        else
                        {
                            output = RightTriggerDelta(GamePadService.ConnectedGamePadIndexes[0]);
                        }

                        break;
                    case GamePadSensorValueMode.Last:
                        if (sensor == GamePadSensor.LeftTrigger)
                        {
                            output = LeftTriggerDelta(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }
                        else
                        {
                            output = RightTriggerDelta(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }

                        break;
                    default:
                        for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                        {
                            float value = 0f;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Max:
                                    value = float.MinValue;
                                    break;
                                case GamePadSensorValueMode.Min:
                                    value = float.MaxValue;
                                    break;
                            }

                            switch (sensor)
                            {
                                case GamePadSensor.LeftTrigger:
                                    value = LeftTriggerDelta(i);
                                    break;
                                case GamePadSensor.RightTrigger:
                                    value = RightTriggerDelta(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += value;
                                    break;
                                case GamePadSensorValueMode.Max:
                                    if (value > output)
                                    {
                                        output = value;
                                    }
                                    break;
                                case GamePadSensorValueMode.Min:
                                    if (value < output)
                                    {
                                        output = value;
                                    }
                                    break;
                            }
                        }

                        break;
                }


                return output;
            }

            return float.MinValue;
        }






        public static Vector2 GetStickPosition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            if (sensor == GamePadSensor.LeftStick || sensor == GamePadSensor.RightStick)
            {
                throw new Exception("Invalid sensor!");
            }


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
            else
            {
                var output = Vector2.Zero;
                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        if (sensor == GamePadSensor.LeftStick)
                        {
                            output = CurrentLeftStick(GamePadService.ConnectedGamePadIndexes[0]);
                        }
                        else
                        {
                            output = CurrentRightStick(GamePadService.ConnectedGamePadIndexes[0]);
                        }

                        break;
                    case GamePadSensorValueMode.Last:
                        if (sensor == GamePadSensor.LeftStick)
                        {
                            output = CurrentLeftStick(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }
                        else
                        {
                            output = CurrentRightStick(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }

                        break;
                    default:
                        for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                        {
                            Vector2 value = Vector2.Zero;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Max:
                                    value = new Vector2(float.MinValue);
                                    break;
                                case GamePadSensorValueMode.Min:
                                    value = new Vector2(float.MaxValue);
                                    break;
                            }

                            switch (sensor)
                            {
                                case GamePadSensor.LeftStick:
                                    value = CurrentLeftStick(i);
                                    break;
                                case GamePadSensor.RightStick:
                                    value = CurrentRightStick(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += value;
                                    break;
                                case GamePadSensorValueMode.Max:
                                    if (value.X > output.X || value.Y > output.Y)
                                    {
                                        output = value;
                                    }
                                    break;
                                case GamePadSensorValueMode.Min:
                                    if (value.X < output.X || value.Y < output.Y)
                                    {
                                        output = value;
                                    }
                                    break;
                            }
                        }

                        break;
                }


                return output;
            }

            return Vector2.Zero;
        }

        public static float GetTriggerPosition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            if (sensor == GamePadSensor.LeftStick || sensor == GamePadSensor.RightStick)
            {
                throw new Exception("Invalid sensor!");
            }


            if (inputMode == GamePadInputMode.SingleGamePad)
            {
                switch (sensor)
                {
                    case GamePadSensor.LeftTrigger:
                        return CurrentLeftTrigger(playerIndex);
                    case GamePadSensor.RightTrigger:
                        return CurrentRightTrigger(playerIndex);
                }
            }
            else
            {
                var output = 0f;
                switch (valueConditionMode)
                {
                    case GamePadSensorValueMode.First:
                        if (sensor == GamePadSensor.LeftTrigger)
                        {
                            output = CurrentLeftTrigger(GamePadService.ConnectedGamePadIndexes[0]);
                        }
                        else
                        {
                            output = CurrentRightTrigger(GamePadService.ConnectedGamePadIndexes[0]);
                        }

                        break;
                    case GamePadSensorValueMode.Last:
                        if (sensor == GamePadSensor.LeftTrigger)
                        {
                            output = CurrentLeftTrigger(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }
                        else
                        {
                            output = CurrentRightTrigger(GamePadService.ConnectedGamePadIndexes[GamePadService.ConnectedGamePadIndexes.Count - 1]);
                        }

                        break;
                    default:
                        for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                        {
                            float value = 0f;
                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Max:
                                    value = float.MinValue;
                                    break;
                                case GamePadSensorValueMode.Min:
                                    value = float.MaxValue;
                                    break;
                            }

                            switch (sensor)
                            {
                                case GamePadSensor.LeftTrigger:
                                    value = CurrentLeftTrigger(i);
                                    break;
                                case GamePadSensor.RightTrigger:
                                    value = CurrentRightTrigger(i);
                                    break;
                            }

                            switch (valueConditionMode)
                            {
                                case GamePadSensorValueMode.Average:
                                    output += value;
                                    break;
                                case GamePadSensorValueMode.Max:
                                    if (value > output)
                                    {
                                        output = value;
                                    }
                                    break;
                                case GamePadSensorValueMode.Min:
                                    if (value < output)
                                    {
                                        output = value;
                                    }
                                    break;
                            }
                        }

                        break;
                }


                return output;
            }

            return float.MinValue;
        }
    }
}
