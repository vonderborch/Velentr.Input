using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velentr.Input.GamePad
{

    /// <summary>
    /// Defines the base methods and properties that are needed for GamePad Input support.
    /// </summary>
    /// <seealso cref="Velentr.Input.InputEngine" />
    public abstract class GamePadEngine : InputEngine
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadEngine"/> class.
        /// </summary>
        protected GamePadEngine() : base()
        {
        }

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
        /// Sets the game pad dead zone.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="deadZone">The dead zone.</param>
        public abstract void SetGamePadDeadZone(int playerIndex, GamePadDeadZone deadZone);

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
        /// Gets the index for connected game pad.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>the index.</returns>
        public abstract int GetIndexForConnectedGamePad(int playerIndex);

        /// <summary>
        /// Determines whether [is game pad connected] [the specified player index].
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is game pad connected] [the specified player index]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsGamePadConnected(int playerIndex);

    }

}
