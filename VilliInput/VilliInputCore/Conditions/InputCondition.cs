using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VilliInput.EventArguments;

namespace VilliInput.Conditions
{
    public abstract class InputCondition
    {

        public InputSource Source;

        public VilliEvent PressedEvent = new VilliEvent();

        public VilliEvent PressStartedEvent = new VilliEvent();

        public VilliEvent ReleasedEvent = new VilliEvent();

        public VilliEvent ReleaseStartedEvent = new VilliEvent();

        public VilliEvent ValueValidEvent = new VilliEvent();

        public bool WindowMustBeActive { get; private set; }

        public ConditionState CurrentState { get; protected set; }

        public GameTime CurrentStateStart { get; protected set; }

        public bool ImplementedPressed { get; protected set; }

        public bool ImplementedPressStarted { get; protected set; }

        public bool ImplementedReleased { get; protected set; }

        public bool ImplementedReleaseStarted { get; protected set; }

        public bool ImplementedValidValue { get; protected set; }

        public InputValueLogic? ValidValueComparator { get; protected set; }

        protected InputCondition(InputSource source, bool windowMustBeActive = true, bool implementedPressed = false, bool implementedPressStarted = false, bool implementedReleased = false, bool implementedReleaseStarted = false, bool implementedValidValue = false, InputValueLogic? inputValueComparator = null)
        {
            Source = source;
            WindowMustBeActive = windowMustBeActive;

            ImplementedPressed = implementedPressed;
            ImplementedPressStarted = implementedPressStarted;
            ImplementedReleased = implementedReleased;
            ImplementedReleaseStarted = implementedReleaseStarted;
            ImplementedValidValue = implementedValidValue;

            ValidValueComparator = inputValueComparator;
        }

        public abstract bool Pressed(bool consumable = true, bool ignoredConsumed = false);

        public abstract bool PressStarted(bool consumable = true, bool ignoredConsumed = false);

        public abstract bool Released(bool consumable = true, bool ignoredConsumed = false);

        public abstract bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false);

        public abstract bool ValueValid();

        public abstract InputValue GetInputValue();

        public abstract void Consume();

        internal abstract VilliEventArguments GetArguments();

        protected void UpdateState(ConditionState newState)
        {
            CurrentState = newState;
            CurrentStateStart = Villi.CurrentTime;
        }

        protected void InternalPressed(bool consumable)
        {
            if (consumable)
            {
                Consume();
            }

            PressedEvent.TriggerEvent(this, GetArguments());
        }

        protected void InternalPressStarted(bool consumable)
        {
            if (consumable)
            {
                Consume();
            }

            PressStartedEvent.TriggerEvent(this, GetArguments());
        }

        protected void InternalReleased(bool consumable)
        {
            if (consumable)
            {
                Consume();
            }

            ReleasedEvent.TriggerEvent(this, GetArguments());
        }

        protected void InternalReleaseStarted(bool consumable)
        {
            if (consumable)
            {
                Consume();
            }

            ReleaseStartedEvent.TriggerEvent(this, GetArguments());
        }

        protected void InternalValueValid()
        {
            ValueValidEvent.TriggerEvent(this, GetArguments());
        }
    }
}
