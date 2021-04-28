using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Keyboard;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class KeyboardButtonCondition : BooleanCondition
    {

        protected KeyboardButtonCondition(InputManager manager, Key key, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.Keyboard, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
        {
            Key = key;

            if (manager.Settings.ThrowWhenCreatingConditionIfNoServiceEnabled && manager.Keyboard == null)
            {
                throw new Exception(Constants.NoEngineConfiguredError);
            }
        }

        public Key Key { get; }

        public override ConditionEventArguments GetArguments()
        {
            return new KeyboardButtonEventArguments
            {
                Key = Key,
                Condition = this,
                InputSource = InputSource,
                NumberOfKeysPressed = Manager.Keyboard.CurrentKeysPressed(),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
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

        public override void Consume()
        {
            Manager.Keyboard.ConsumeKey(Key);
        }

        public override bool IsConsumed()
        {
            return Manager.Keyboard.IsKeyConsumed(Key);
        }

    }

}
