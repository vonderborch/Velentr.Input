﻿using System;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.GamePad;

namespace VilliInput.Conditions.Internal
{
    public abstract class GamePadButtonCondition : BooleanCondition
    {
        public GamePadButton Button { get; private set; }

        public int PlayerIndex { get; protected set; }

        public GamePadInputMode InputMode { get; protected set; }

        protected GamePadButtonCondition(GamePadButton button, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.GamePad, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Button = button;
            PlayerIndex = playerIndex;
            InputMode = inputMode;
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
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (InternalCurrent(GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!InternalCurrent(GamePadService.ConnectedGamePadIndexes[i]))
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
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (InternalPrevious(GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!InternalPrevious(GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return false;
                        }
                    }

                    return true;
            }

            return false;
        }

        public override VilliEventArguments GetArguments()
        {
            return new GamePadButtonEventArguments()
            {
                Button = this.Button,
                Condition = this,
                InputSource = this.InputSource,
                MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
                PlayerIndex = this.PlayerIndex,
                InputMode = this.InputMode,
            };
        }

        public override void Consume()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    Villi.System.GamePad.ConsumeButton(Button, PlayerIndex);
                    break;
                case GamePadInputMode.AnyGamePad:
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        Villi.System.GamePad.ConsumeButton(Button, GamePadService.ConnectedGamePadIndexes[i]);
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
            return ((!WindowMustBeActive || Villi.IsWindowActive)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet));
        }

        public override bool IsConsumed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return Villi.System.GamePad.IsButtonConsumed(Button, PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (Villi.System.GamePad.IsButtonConsumed(Button, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!Villi.System.GamePad.IsButtonConsumed(Button, GamePadService.ConnectedGamePadIndexes[i]))
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