using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VilliInput.OLD.GamePadInput
{

    public class GamePadService : InputService
    {
        public Dictionary<(int, GamePadButton), ulong> ButtonConsumed = new Dictionary<(int, GamePadButton), ulong>();

        public Dictionary<(int, GamePadSensor), ulong> SensorConsumed = new Dictionary<(int, GamePadSensor), ulong>();

        public static VilliGamePad[] GamePads;

        public static List<int> ConnectedGamePadIndexes;

        public GameTime LastConnectionCheckTime = null;

        public GamePadService()
        {
            Source = InputSource.GamePad;
        }

        public override void Setup()
        {
            GamePads = new VilliGamePad[GamePadHelpers.MaximumGamePads];
            ConnectedGamePadIndexes = new List<int>(GamePadHelpers.MaximumGamePads);

            for (var i = 0; i < GamePadHelpers.MaximumGamePads; i++)
            {
                GamePads[i] = new VilliGamePad
                {
                    PlayerIndex = i,
                    PreviousState = GamePadState.Default,
                    CurrentState = GamePadState.Default,
                    Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i),
                    DeadZone = Villi.System.Settings.DefaultGamePadDeadZone,
                };

                if (GamePads[i].IsConnected)
                {
                    ConnectedGamePadIndexes.Add(i);
                }
            }
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

        public void CheckForConnectedGamePads()
        {
            ConnectedGamePadIndexes.Clear();
            for (var i = 0; i < GamePadHelpers.MaximumGamePads; i++)
            {
                GamePads[i].PreviousState = GamePads[i].CurrentState;
                GamePads[i].CurrentState = Microsoft.Xna.Framework.Input.GamePad.GetState(i, GamePads[i].DeadZone);
                GamePads[i].Capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(i);
            }

            LastConnectionCheckTime = Villi.CurrentTime;
        }

        public void SetGamePadDeadZone(int playerIndex, GamePadDeadZone deadZone)
        {
            if (playerIndex < 0 || playerIndex > GamePadHelpers.HighestGamePadIndex)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }

            GamePads[playerIndex].DeadZone = deadZone;
        }

        public void ConsumeButton(GamePadButton button, int playerIndex = 0)
        {
            ButtonConsumed[(playerIndex, button)] = Villi.CurrentFrame;
        }

        public void ConsumeSensor(GamePadSensor sensor, int playerIndex = 0)
        {
            SensorConsumed[(playerIndex, sensor)] = Villi.CurrentFrame;
        }

        public bool IsButtonConsumed(GamePadButton button, int playerIndex = 0)
        {
            if (!GamePads[playerIndex].IsConnected)
            {
                return false;
            }

            if (ButtonConsumed.TryGetValue((playerIndex, button), out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }

        public bool IsSensorConsumed(GamePadSensor sensor, int playerIndex = 0)
        {
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
            if (!GamePads[playerIndex].IsConnected)
            {
                return false;
            }

            return Microsoft.Xna.Framework.Input.GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
        }
    }
}
