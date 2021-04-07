using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Velentr.Input.GamePad
{

    /// <summary>
    /// The default XNA-based GamePad Engine for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.GamePad.GamePadEngine" />
    public class XnaGamePadEngine : GamePadEngine
    {

        /// <summary>
        /// The button mapping
        /// </summary>
        internal Dictionary<GamePadButton, Func<GamePadState, ButtonState>> ButtonMapping = new Dictionary<GamePadButton, Func<GamePadState, ButtonState>>(Enum.GetNames(typeof(GamePadButton)).Length)
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

        /// <summary>
        /// The game pads
        /// </summary>
        public VilliGamePad[] GamePads;

        /// <summary>
        /// The connected game pad indexes
        /// </summary>
        public List<int> ConnectedGamePadIndexes;

        /// <summary>
        /// The last connection check time
        /// </summary>
        public GameTime LastConnectionCheckTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaGamePadEngine"/> class.
        /// </summary>
        public XnaGamePadEngine() : base()
        {
        }

#if MONOGAME
        /// <summary>
        /// Gets the maximum game pads.
        /// </summary>
        /// <value>
        /// The maximum game pads.
        /// </value>
        public override int MaximumGamePads => Microsoft.Xna.Framework.Input.GamePad.MaximumGamePadCount;
        
        /// <summary>
        /// Gets the index of the highest game pad.
        /// </summary>
        /// <value>
        /// The index of the highest game pad.
        /// </value>
        public override int HighestGamePadIndex => Microsoft.Xna.Framework.Input.GamePad.MaximumGamePadCount - 1;
#else
        /// <summary>
        /// Gets the maximum game pads.
        /// </summary>
        /// <value>
        /// The maximum game pads.
        /// </value>
        public override int MaximumGamePads => 4;

        /// <summary>
        /// Gets the index of the highest game pad.
        /// </summary>
        /// <value>
        /// The index of the highest game pad.
        /// </value>
        public override int HighestGamePadIndex => 3;
#endif

        /// <summary>
        /// Sets up the InputEngine
        /// </summary>
        protected override void SetupInternal()
        {
            GamePads = new VilliGamePad[MaximumGamePads];
            ConnectedGamePadIndexes = new List<int>(MaximumGamePads);

            for (var i = 0; i < MaximumGamePads; i++)
            {
#if MONOGAME
                GamePads[i] = new VilliGamePad
                {
                    PlayerIndex = i,
                    PreviousState = GamePadState.Default,
                    CurrentState = GamePadState.Default,
                    Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i),
                    DeadZone = Constants.Settings.DefaultGamePadDeadZone
                };
#else
                GamePads[i] = new VilliGamePad
                {
                    PlayerIndex = i,
                    PreviousState = Microsoft.Xna.Framework.Input.GamePad.GetState(GamePadHelpers.IntIndexToPlayerIndex(i)),
                    CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(GamePadHelpers.IntIndexToPlayerIndex(i)),
                    Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(GamePadHelpers.IntIndexToPlayerIndex(i)),
                    DeadZone = Constants.Settings.DefaultGamePadDeadZone
                };
#endif

                if (GamePads[i].IsConnected)
                {
                    ConnectedGamePadIndexes.Add(i);
                }
            }
        }

        /// <summary>
        /// Checks for connected game pads.
        /// </summary>
        private void CheckForConnectedGamePads()
        {
            ConnectedGamePadIndexes.Clear();
            for (var i = 0; i < MaximumGamePads; i++)
            {
                GamePads[i].PreviousState = GamePads[i].CurrentState;
#if MONOGAME
                GamePads[i].CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(i, GamePads[i].DeadZone);
                GamePads[i].Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i);
#else
                GamePads[i].CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(GamePadHelpers.IntIndexToPlayerIndex(i), GamePads[i].DeadZone);
                GamePads[i].Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(GamePadHelpers.IntIndexToPlayerIndex(i));
#endif
            }

            LastConnectionCheckTime = Manager.CurrentTime;
        }

        /// <summary>
        /// Updates the InputEngine.
        /// </summary>
        public override void Update()
        {
            // check if we need to check for if any controllers have been connected to the system
            if (Constants.Settings.SecondsBetweenGamePadConnectionCheck > 0)
            {
                var elapsedSecondsBetweenChecks = LastConnectionCheckTime == null
                    ? Constants.Settings.SecondsBetweenGamePadConnectionCheck
                    : (Manager.CurrentTime.TotalGameTime - LastConnectionCheckTime.TotalGameTime).TotalSeconds;

                if (elapsedSecondsBetweenChecks >= Constants.Settings.SecondsBetweenGamePadConnectionCheck)
                {
                    ConnectedGamePadIndexes.Clear();
                    for (var i = 0; i < GamePads.Length; i++)
                    {
#if MONOGAME
                        GamePads[i].Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i);
#else
                        GamePads[i].Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(GamePadHelpers.IntIndexToPlayerIndex(i));
#endif

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
#if MONOGAME
                    GamePads[i].CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(i, GamePads[i].DeadZone);
#else
                    GamePads[i].CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(GamePadHelpers.IntIndexToPlayerIndex(i), GamePads[i].DeadZone);
#endif
                }
            }
        }

        /// <summary>
        /// Sets the game pad dead zone.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="deadZone">The dead zone.</param>
        public override void SetGamePadDeadZone(int playerIndex, GamePadDeadZone deadZone)
        {
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            GamePads[playerIndex].DeadZone = deadZone;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            if (!GamePads[playerIndex].IsConnected)
            {
                return false;
            }

            return Microsoft.Xna.Framework.Input.GamePad.SetVibration(GamePadHelpers.IntIndexToPlayerIndex(playerIndex), leftMotor, rightMotor);
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return ButtonMapping[button](GamePads[playerIndex].CurrentState) == ButtonState.Pressed;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return ButtonMapping[button](GamePads[playerIndex].PreviousState) == ButtonState.Pressed;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return ButtonMapping[button](GamePads[playerIndex].CurrentState) == ButtonState.Released;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return ButtonMapping[button](GamePads[playerIndex].PreviousState) == ButtonState.Released;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].CurrentState.ThumbSticks.Left;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].PreviousState.ThumbSticks.Left;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].CurrentState.ThumbSticks.Right;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].PreviousState.ThumbSticks.Right;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].CurrentState.Triggers.Left;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].PreviousState.Triggers.Left;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].CurrentState.Triggers.Right;
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
            GamePadHelpers.ValidatePlayerIndex(playerIndex, HighestGamePadIndex);

            return GamePads[playerIndex].PreviousState.Triggers.Right;
        }

        /// <summary>
        /// Gets the connected game pad count.
        /// </summary>
        /// <returns></returns>
        public override int GetConnectedGamePadCount()
        {
            return ConnectedGamePadIndexes.Count;
        }

        /// <summary>
        /// Gets the first index of the connected game pad.
        /// </summary>
        /// <returns></returns>
        public override int GetFirstConnectedGamePadIndex()
        {
            return ConnectedGamePadIndexes[0];
        }

        /// <summary>
        /// Determines whether [is game pad connected] [the specified player index].
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>
        ///   <c>true</c> if [is game pad connected] [the specified player index]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsGamePadConnected(int playerIndex)
        {
            if (playerIndex < 0 || playerIndex > HighestGamePadIndex)
            {
                return false;
            }

            return GamePads[playerIndex].IsConnected;
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
            return ConnectedGamePadIndexes[playerIndex];
        }

    }

}
