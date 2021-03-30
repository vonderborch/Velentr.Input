using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.GamePad;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class GamePadButtonCondition : BooleanCondition
    {

        protected GamePadButtonCondition(GamePadButton button, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.GamePad, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Button = button;
            PlayerIndex = playerIndex;
            InputMode = inputMode;
        }

        public GamePadButton Button { get; }

        public int PlayerIndex { get; protected set; }

        public GamePadInputMode InputMode { get; protected set; }

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

        public override ConditionEventArguments GetArguments()
        {
            return new GamePadButtonEventArguments
            {
                Button = Button,
                Condition = this,
                InputSource = InputSource,
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                PlayerIndex = PlayerIndex,
                InputMode = InputMode
            };
        }

        public override void Consume()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    VelentrInput.System.GamePad.ConsumeButton(Button, PlayerIndex);
                    break;
                case GamePadInputMode.AnyGamePad:
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        VelentrInput.System.GamePad.ConsumeButton(Button, GamePadService.ConnectedGamePadIndexes[i]);
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
                ((WindowMustBeActive && VelentrInput.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime) >= milliSecondsForConditionMet)
            );
        }

        public override bool IsConsumed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return VelentrInput.System.GamePad.IsButtonConsumed(Button, PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (VelentrInput.System.GamePad.IsButtonConsumed(Button, GamePadService.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < GamePadService.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!VelentrInput.System.GamePad.IsButtonConsumed(Button, GamePadService.ConnectedGamePadIndexes[i]))
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
