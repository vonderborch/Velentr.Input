using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.GamePad;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class GamePadButtonCondition : BooleanCondition
    {

        protected GamePadButtonCondition(InputManager manager, GamePadButton button, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.GamePad, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
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
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (InternalCurrent(Manager.GamePad.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!InternalCurrent(Manager.GamePad.ConnectedGamePadIndexes[i]))
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
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (InternalPrevious(Manager.GamePad.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!InternalPrevious(Manager.GamePad.ConnectedGamePadIndexes[i]))
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
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
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
                    Manager.GamePad.ConsumeButton(Button, PlayerIndex);
                    break;
                case GamePadInputMode.AnyGamePad:
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        Manager.GamePad.ConsumeButton(Button, Manager.GamePad.ConnectedGamePadIndexes[i]);
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
                ((WindowMustBeActive && Manager.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime) >= milliSecondsForConditionMet)
                && (MilliSecondsForTimeOut == 0 || Helper.ElapsedMilliSeconds(LastFireTime, Manager.CurrentTime) >= MilliSecondsForTimeOut)
            );
        }

        public override bool IsConsumed()
        {
            switch (InputMode)
            {
                case GamePadInputMode.SingleGamePad:
                    return Manager.GamePad.IsButtonConsumed(Button, PlayerIndex);
                case GamePadInputMode.AnyGamePad:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (Manager.GamePad.IsButtonConsumed(Button, Manager.GamePad.ConnectedGamePadIndexes[i]))
                        {
                            return true;
                        }
                    }

                    return false;
                case GamePadInputMode.AllGamePads:
                    for (var i = 0; i < Manager.GamePad.ConnectedGamePadIndexes.Count; i++)
                    {
                        if (!Manager.GamePad.IsButtonConsumed(Button, Manager.GamePad.ConnectedGamePadIndexes[i]))
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
