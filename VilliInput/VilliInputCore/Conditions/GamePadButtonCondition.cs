using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.GamePadInput;
using VilliInput.MouseInput;

namespace VilliInput.Conditions
{
    public class GamePadButtonCondition : ButtonCondition
    {
        public GamePadButton Button { get; private set; }

        public int PlayerIndex { get; private set; }

        public GamePadInputMode InputMode { get; private set; }

        public GamePadButtonCondition(GamePadButton button, int playerIndex, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, uint secondsForPressed = 0, uint secondsForReleased = 0) : base(InputSource.GamePad, windowMustBeActive, secondsForPressed, secondsForReleased, true, true, true, true, false, null)
        {
            Button = button;
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
        }

        public override void Consume()
        {
            if (InputMode != GamePadInputMode.SingleGamePad)
            {
                for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                {
                    Villi.System.GamePad.ConsumeButton(Button, GamePadService.ConnectedGamePadIndexes[i]);
                }
            }
            else
            {
                Villi.System.GamePad.ConsumeButton(Button, PlayerIndex);
            }
        }

        internal override VilliEventArguments GetArguments()
        {
            return new GamePadButtonEventArguments()
            {
                Button = this.Button,
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
            };
        }

        protected override bool ActionValid(bool ignoredConsumed, uint actionTime)
        {
            return (!WindowMustBeActive || Villi.IsWindowActive)
                   && (ignoredConsumed || Villi.System.GamePad.IsButtonConsumed(Button))
                   && (actionTime == 0 || Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime) >= actionTime);
        }

        protected override bool IsPressed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return GamePadHelpers.IsButtonPressed(Button, PlayerIndex);
                case GamePadInputMode.AllGamePads:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!GamePadHelpers.IsButtonPressed(Button, GamePadService.ConnectedGamePadIndexes[i]))
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
                        if (GamePadHelpers.IsButtonPressed(Button, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        protected override bool IsReleased()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return GamePadHelpers.IsButtonReleased(Button, PlayerIndex);
                case GamePadInputMode.AllGamePads:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!GamePadHelpers.IsButtonReleased(Button, GamePadService.ConnectedGamePadIndexes[i]))
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
                        if (GamePadHelpers.IsButtonReleased(Button, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        protected override bool WasPressed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return GamePadHelpers.WasButtonPressed(Button, PlayerIndex);
                case GamePadInputMode.AllGamePads:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!GamePadHelpers.WasButtonPressed(Button, GamePadService.ConnectedGamePadIndexes[i]))
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
                        if (GamePadHelpers.WasButtonPressed(Button, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        protected override bool WasReleased()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return GamePadHelpers.WasButtonReleased(Button, PlayerIndex);
                case GamePadInputMode.AllGamePads:
                {
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!GamePadHelpers.WasButtonReleased(Button, GamePadService.ConnectedGamePadIndexes[i]))
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
                        if (GamePadHelpers.WasButtonReleased(Button, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }
    }
}
