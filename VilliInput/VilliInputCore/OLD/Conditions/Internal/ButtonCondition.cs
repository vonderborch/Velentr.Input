using System;

namespace VilliInput.OLD.Conditions.Internal
{
    public abstract class ButtonCondition : InputCondition
    {
        public uint SecondsForPressed { get; private set; }

        public uint SecondsForReleased { get; private set; }

        protected ButtonCondition(InputSource source, bool windowMustBeActive = true, uint secondsForPressed = 0, uint secondsForReleased = 0, bool implementedPressed = false, bool implementedPressStarted = false, bool implementedReleased = false, bool implementedReleaseStarted = false, bool implementedValidValue = false, InputValueLogic? inputValueComparator = null) : base(source, windowMustBeActive, implementedPressed, implementedPressStarted, implementedReleased, implementedReleaseStarted, implementedValidValue, inputValueComparator)
        {
            SecondsForPressed = secondsForPressed;
            SecondsForReleased = secondsForReleased;
        }

        protected abstract bool ActionValid(bool ignoredConsumed, uint actionTime);

        protected abstract bool IsPressed();

        protected abstract bool WasPressed();

        protected abstract bool IsReleased();

        protected abstract bool WasReleased();

        public override InputValue GetInputValue()
        {
            throw new NotImplementedException();
        }

        public override bool Pressed(bool consumable = true, bool ignoredConsumed = false)
        {
            var isPressed = IsPressed();
            if (isPressed && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Pressed);
            }
            else if (!isPressed && CurrentState == ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && isPressed)
            {
                InternalPressed(consumable);
                return true;
            }
            return false;
        }

        public override bool PressStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            var isPressed = IsPressed();
            var wasPressed = WasPressed();
            if (isPressed && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Pressed);
            }
            else if (!isPressed && CurrentState == ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }

            if (ActionValid(ignoredConsumed, 0) && isPressed && !wasPressed)
            {
                InternalPressStarted(consumable);
                return true;
            }
            return false;
        }

        public override bool Released(bool consumable = true, bool ignoredConsumed = false)
        {
            var isReleased = IsReleased();
            if (isReleased && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }
            else if (!isReleased && CurrentState == ConditionState.Released)
            {
                UpdateState(ConditionState.Pressed);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && isReleased)
            {
                InternalReleased(consumable);
                return true;
            }
            return false;
        }

        public override bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            var isReleased = IsReleased();
            var wasReleased = WasReleased();
            if (isReleased && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }
            else if (!isReleased && CurrentState == ConditionState.Released)
            {
                UpdateState(ConditionState.Pressed);
            }

            if (ActionValid(ignoredConsumed, 0) && isReleased && !wasReleased)
            {
                InternalReleaseStarted(consumable);
                return true;
            }
            return false;
        }

        public override bool ValueValid()
        {
            throw new NotImplementedException();
        }
    }
}
