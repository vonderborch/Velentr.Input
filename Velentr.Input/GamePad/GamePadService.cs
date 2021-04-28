using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Velentr.Input.Enums;

namespace Velentr.Input.GamePad
{

    /// <summary>
    /// Defines what methods must be available at a minimum to support GamePad inputs
    /// </summary>
    /// <seealso cref="Velentr.Input.InputService" />
    public abstract class GamePadService : InputService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadService"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        protected GamePadService(InputManager manager) : base(manager)
        {
            Source = InputSource.GamePad;
        }

        /// <summary>
        /// Gets or sets the engine.
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        public GamePadEngine Engine { get; protected set; }

        /// <summary>
        /// Gets the maximum game pads.
        /// </summary>
        /// <value>
        /// The maximum game pads.
        /// </value>
        public abstract int MaximumGamePads { get; }

        /// <summary>
        /// Gets the index of the highest game pad.
        /// </summary>
        /// <value>
        /// The index of the highest game pad.
        /// </value>
        public abstract int HighestGamePadIndex { get; }

        /// <summary>
        /// Gets the index for connected game pad.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the index.</returns>
        public abstract int GetIndexForConnectedGamePad(int playerIndex);

        /// <summary>
        /// Sets the game pad dead zone.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="deadZone">The dead zone.</param>
        public abstract void SetGamePadDeadZone(int playerIndex, GamePadDeadZone deadZone);

        /// <summary>
        /// Consumes the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        public abstract void ConsumeButton(GamePadButton button, int playerIndex = 0);

        /// <summary>
        /// Consumes the sensor.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        public abstract void ConsumeSensor(GamePadSensor sensor, int playerIndex = 0);

        /// <summary>
        /// Determines whether [is button consumed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is button consumed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsButtonConsumed(GamePadButton button, int playerIndex = 0);

        /// <summary>
        /// Determines whether [is sensor consumed] [the specified sensor].
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is sensor consumed] [the specified sensor]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsSensorConsumed(GamePadSensor sensor, int playerIndex = 0);

        /// <summary>
        /// Sets the vibration.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="leftMotor">The left motor.</param>
        /// <param name="rightMotor">The right motor.</param>
        /// <returns></returns>
        public abstract bool SetVibration(int playerIndex, float leftMotor, float rightMotor);

        /// <summary>
        /// Determines whether [is button pressed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is button pressed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsButtonPressed(GamePadButton button, int playerIndex);

        /// <summary>
        /// Determines whether [was button pressed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [was button pressed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasButtonPressed(GamePadButton button, int playerIndex);

        /// <summary>
        /// Determines whether [is button released] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is button released] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsButtonReleased(GamePadButton button, int playerIndex);

        /// <summary>
        /// Determines whether [was button released] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [was button released] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasButtonReleased(GamePadButton button, int playerIndex);

        /// <summary>
        /// Determines the current left stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the stick position.</returns>
        public abstract Vector2 CurrentLeftStick(int playerIndex);

        /// <summary>
        /// Determines the previous left stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the stick position.</returns>
        public abstract Vector2 PreviousLeftStick(int playerIndex);

        /// <summary>
        /// Determines the current right stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the stick position.</returns>
        public abstract Vector2 CurrentRightStick(int playerIndex);

        /// <summary>
        /// Determines the previous right stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the stick position.</returns>
        public abstract Vector2 PreviousRightStick(int playerIndex);

        /// <summary>
        /// Determines the current left trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the trigger position.</returns>
        public abstract float CurrentLeftTrigger(int playerIndex);

        /// <summary>
        /// Determines the previous left trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the trigger position.</returns>
        public abstract float PreviousLeftTrigger(int playerIndex);

        /// <summary>
        /// Determines the current right trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the trigger position.</returns>
        public abstract float CurrentRightTrigger(int playerIndex);

        /// <summary>
        /// Determines the previous right trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the trigger position.</returns>
        public abstract float PreviousRightTrigger(int playerIndex);

        /// <summary>
        /// Determines whether the specified sensor has moved.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>Whether the sensor moved.</returns>
        public bool SensorMoved(GamePadSensor sensor, int playerIndex)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

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

        /// <summary>
        /// Determines the delta for the left stick.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the delta.</returns>
        public Vector2 LeftStickDelta(int playerIndex)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return CurrentLeftStick(playerIndex) - PreviousLeftStick(playerIndex);
        }

        /// <summary>
        /// Determines the delta for the right stick.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the delta.</returns>
        public Vector2 RightStickDelta(int playerIndex)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return CurrentRightStick(playerIndex) - PreviousRightStick(playerIndex);
        }

        /// <summary>
        /// Determines the delta for the left trigger.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the delta.</returns>
        public float LeftTriggerDelta(int playerIndex)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return CurrentLeftTrigger(playerIndex) - PreviousLeftTrigger(playerIndex);
        }

        /// <summary>
        /// Determines the delta for the right trigger.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the delta.</returns>
        public float RightTriggerDelta(int playerIndex)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return CurrentRightTrigger(playerIndex) - PreviousRightTrigger(playerIndex);
        }

        /// <summary>
        /// Gets the first index of the connected game pad.
        /// </summary>
        /// <returns></returns>
        public abstract int GetFirstConnectedGamePadIndex();

        /// <summary>
        /// Gets the connected game pad count.
        /// </summary>
        /// <returns></returns>
        public abstract int GetConnectedGamePadCount();

        /// <summary>
        /// Gets the stick delta.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="valueConditionMode">The value condition mode.</param>
        /// <returns>the stick delta</returns>
        /// <exception cref="System.Exception"></exception>
        public Vector2 GetStickDelta(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

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
                            ? LeftStickDelta(GetFirstConnectedGamePadIndex())
                            : RightStickDelta(GetFirstConnectedGamePadIndex());
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftStick
                            ? LeftStickDelta(GetConnectedGamePadCount() - 1)
                            : RightStickDelta(GetConnectedGamePadCount() - 1);
                        break;
                    default:
                        for (var i = 0; i < GetConnectedGamePadCount(); i++)
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
                    output /= GetConnectedGamePadCount();
                }

                return output;
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Gets the trigger delta.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="valueConditionMode">The value condition mode.</param>
        /// <returns>the trigger delta</returns>
        /// <exception cref="System.Exception"></exception>
        public float GetTriggerDelta(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

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
                            ? LeftTriggerDelta(GetFirstConnectedGamePadIndex())
                            : RightTriggerDelta(GetFirstConnectedGamePadIndex());
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftTrigger
                            ? LeftTriggerDelta(GetConnectedGamePadCount() - 1)
                            : RightTriggerDelta(GetConnectedGamePadCount() - 1);
                        break;
                    default:
                        for (var i = 0; i < GetConnectedGamePadCount(); i++)
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
                    output /= GetConnectedGamePadCount();
                }

                return output;
            }

            return float.MinValue;
        }

        /// <summary>
        /// Gets the stick position.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="valueConditionMode">The value condition mode.</param>
        /// <returns>the stick position</returns>
        /// <exception cref="System.Exception"></exception>
        public Vector2 GetStickPosition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

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
                            ? CurrentLeftStick(GetFirstConnectedGamePadIndex())
                            : CurrentRightStick(GetFirstConnectedGamePadIndex());
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftStick
                            ? CurrentLeftStick(GetConnectedGamePadCount() - 1)
                            : CurrentRightStick(GetConnectedGamePadCount() - 1);
                        break;
                    default:
                        for (var i = 0; i < GetConnectedGamePadCount(); i++)
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
                    output /= GetConnectedGamePadCount();
                }

                return output;
            }

            return Vector2.Zero;
        }

        /// <summary>
        /// Gets the trigger position.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="valueConditionMode">The value condition mode.</param>
        /// <returns>the trigger position</returns>
        /// <exception cref="System.Exception"></exception>
        public float GetTriggerPosition(GamePadSensor sensor, int playerIndex, GamePadInputMode inputMode, GamePadSensorValueMode valueConditionMode)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

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
                            ? CurrentLeftTrigger(GetFirstConnectedGamePadIndex())
                            : CurrentRightTrigger(GetFirstConnectedGamePadIndex());
                        break;
                    case GamePadSensorValueMode.Last:
                        output = sensor == GamePadSensor.LeftTrigger
                            ? LeftTriggerDelta(GetConnectedGamePadCount() - 1)
                            : CurrentRightTrigger(GetConnectedGamePadCount() - 1);
                        break;
                    default:
                        for (var i = 0; i < GetConnectedGamePadCount(); i++)
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
                    output /= GetConnectedGamePadCount();
                }

                return output;
            }

            return float.MinValue;
        }

    }

}
