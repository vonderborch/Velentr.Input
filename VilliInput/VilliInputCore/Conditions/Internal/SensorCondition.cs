using System;
using VilliInput.MouseInput;

namespace VilliInput.Conditions.Internal
{

    public abstract class SensorCondition : InputCondition
    {
        public uint SecondsForPressed { get; private set; }

        public uint SecondsForReleased { get; private set; }

        protected SensorCondition(InputSource source, bool windowMustBeActive, uint secondsForPressed = 0, uint secondsForReleased = 0, InputValueLogic? inputValueComparator = null) : base(source, windowMustBeActive, true, false, true, false, true, inputValueComparator)
        {
            SecondsForPressed = secondsForPressed;
            SecondsForReleased = secondsForReleased;
        }

        protected abstract bool ActionValid(bool ignoredConsumed, uint actionTime);

        public abstract bool IsPressed();

        public abstract bool IsReleased();


        public override bool Pressed(bool consumable = true, bool ignoredConsumed = false)
        {
            var pressed = IsPressed();

            if (!pressed && CurrentState != ConditionState.Released)
            {
                UpdateState(ConditionState.Released);
            }
            else if (pressed && CurrentState == ConditionState.Released)
            {
                UpdateState(ConditionState.Pressed);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && !pressed)
            {
                InternalReleased(consumable);
                return true;
            }

            return false;
        }

        public override bool PressStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            throw new NotImplementedException();
        }

        public override bool Released(bool consumable = true, bool ignoredConsumed = false)
        {
            var released = IsReleased();

            if (!released && CurrentState != ConditionState.Released)
            {
                UpdateState(ConditionState.Released);
            }
            else if (released && CurrentState == ConditionState.Released)
            {
                UpdateState(ConditionState.Pressed);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && !released)
            {
                InternalReleased(consumable);
                return true;
            }

            return false;
        }

        public override bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            throw new NotImplementedException();
        }

        public override bool ValueValid()
        {
            if (ValidValueComparator == null)
            {
                throw new NullReferenceException("ValidValueComparator cannot be null!");
            }

            return Helpers.ValidValueComparator(GetInputValue(), (InputValueLogic)ValidValueComparator);
        }
    }
}
