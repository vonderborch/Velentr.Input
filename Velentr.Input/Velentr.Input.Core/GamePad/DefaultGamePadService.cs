using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velentr.Input.GamePad
{

    /// <summary>
    /// The default XNA-based GamePad Engine for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.GamePad.GamePadService" />
    public class DefaultGamePadService : GamePadService
    {

        /// <summary>
        /// The button last consumed
        /// </summary>
        public Dictionary<(int, GamePadButton), ulong> ButtonLastConsumed = new Dictionary<(int, GamePadButton), ulong>();

        /// <summary>
        /// The sensor consumed
        /// </summary>
        public Dictionary<(int, GamePadSensor), ulong> SensorConsumed = new Dictionary<(int, GamePadSensor), ulong>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultGamePadService"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public DefaultGamePadService(InputManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Gets the maximum game pads.
        /// </summary>
        /// <value>
        /// The maximum game pads.
        /// </value>
        public override int MaximumGamePads => Engine.MaximumGamePads;

        /// <summary>
        /// Gets the index of the highest game pad.
        /// </summary>
        /// <value>
        /// The index of the highest game pad.
        /// </value>
        public override int HighestGamePadIndex => Engine.HighestGamePadIndex;

        /// <summary>
        /// Sets up the input service.
        /// </summary>
        /// <param name="engine">The engine to setup the input service with.</param>
        protected override void SetupInternal(InputEngine engine)
        {
            Engine = (GamePadEngine)engine;
        }

        /// <summary>
        /// Updates the input service.
        /// </summary>
        public override void Update()
        {
            Engine?.Update();
        }

        /// <summary>
        /// Sets the game pad dead zone.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="deadZone">The dead zone.</param>
        public override void SetGamePadDeadZone(int playerIndex, GamePadDeadZone deadZone)
        {
            Engine.SetGamePadDeadZone(playerIndex, deadZone);
        }

        /// <summary>
        /// Consumes the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        public override void ConsumeButton(GamePadButton button, int playerIndex = 0)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            ButtonLastConsumed[(playerIndex, button)] = Manager.CurrentFrame;
        }

        /// <summary>
        /// Consumes the sensor.
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        public override void ConsumeSensor(GamePadSensor sensor, int playerIndex = 0)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            SensorConsumed[(playerIndex, sensor)] = Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is button consumed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is button consumed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsButtonConsumed(GamePadButton button, int playerIndex = 0)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            if (!Engine.IsGamePadConnected(playerIndex))
            {
                return true;
            }

            if (ButtonLastConsumed.TryGetValue((playerIndex, button), out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is sensor consumed] [the specified sensor].
        /// </summary>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is sensor consumed] [the specified sensor]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsSensorConsumed(GamePadSensor sensor, int playerIndex = 0)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            if (!Engine.IsGamePadConnected(playerIndex))
            {
                return true;
            }

            if (SensorConsumed.TryGetValue((playerIndex, sensor), out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        /// <summary>
        /// Sets the vibration.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="leftMotor">The left motor.</param>
        /// <param name="rightMotor">The right motor.</param>
        /// <returns></returns>
        public override bool SetVibration(int playerIndex, float leftMotor, float rightMotor)
        {
            return Engine.SetVibration(playerIndex, leftMotor, rightMotor);
        }

        /// <summary>
        /// Determines whether [is button pressed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is button pressed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsButtonPressed(GamePadButton button, int playerIndex)
        {
            return Engine.IsButtonPressed(button, playerIndex);
        }

        /// <summary>
        /// Determines whether [was button pressed] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [was button pressed] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasButtonPressed(GamePadButton button, int playerIndex)
        {
            return Engine.WasButtonPressed(button, playerIndex);
        }

        /// <summary>
        /// Determines whether [is button released] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is button released] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsButtonReleased(GamePadButton button, int playerIndex)
        {
            return Engine.IsButtonReleased(button, playerIndex);
        }

        /// <summary>
        /// Determines whether [was button released] [the specified button].
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [was button released] [the specified button]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasButtonReleased(GamePadButton button, int playerIndex)
        {
            return Engine.WasButtonReleased(button, playerIndex);
        }

        /// <summary>
        /// Determines the current left stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the stick position.
        /// </returns>
        public override Vector2 CurrentLeftStick(int playerIndex)
        {
            return Engine.CurrentLeftStick(playerIndex);
        }

        /// <summary>
        /// Determines the previous left stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the stick position.
        /// </returns>
        public override Vector2 PreviousLeftStick(int playerIndex)
        {
            return Engine.PreviousLeftStick(playerIndex);
        }

        /// <summary>
        /// Determines the current right stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the stick position.
        /// </returns>
        public override Vector2 CurrentRightStick(int playerIndex)
        {
            return Engine.CurrentRightStick(playerIndex);
        }

        /// <summary>
        /// Determines the previous right stick position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the stick position.
        /// </returns>
        public override Vector2 PreviousRightStick(int playerIndex)
        {
            return Engine.PreviousRightStick(playerIndex);
        }

        /// <summary>
        /// Determines the current left trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the trigger position.
        /// </returns>
        public override float CurrentLeftTrigger(int playerIndex)
        {
            return Engine.CurrentLeftTrigger(playerIndex);
        }

        /// <summary>
        /// Determines the previous left trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the trigger position.
        /// </returns>
        public override float PreviousLeftTrigger(int playerIndex)
        {
            return Engine.PreviousLeftTrigger(playerIndex);
        }

        /// <summary>
        /// Determines the current right trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the trigger position.
        /// </returns>
        public override float CurrentRightTrigger(int playerIndex)
        {
            return Engine.CurrentRightTrigger(playerIndex);
        }

        /// <summary>
        /// Determines the previous right trigger position.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the trigger position.
        /// </returns>
        public override float PreviousRightTrigger(int playerIndex)
        {
            return Engine.PreviousRightTrigger(playerIndex);
        }

        /// <summary>
        /// Gets the first index of the connected game pad.
        /// </summary>
        /// <returns></returns>
        public override int GetFirstConnectedGamePadIndex()
        {
            return Engine.GetFirstConnectedGamePadIndex();
        }

        /// <summary>
        /// Gets the connected game pad count.
        /// </summary>
        /// <returns></returns>
        public override int GetConnectedGamePadCount()
        {
            return Engine.GetConnectedGamePadCount();
        }

        /// <summary>
        /// Gets the index for connected game pad.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        /// the index.
        /// </returns>
        public override int GetIndexForConnectedGamePad(int playerIndex)
        {
            return Engine.GetIndexForConnectedGamePad(playerIndex);
        }

    }

}
