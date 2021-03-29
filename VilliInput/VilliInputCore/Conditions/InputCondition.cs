﻿using Microsoft.Xna.Framework;
using VilliInput.Enums;
using VilliInput.EventArguments;

namespace VilliInput.Conditions
{

    public abstract class InputCondition
    {

        public VilliEvent Event = new VilliEvent();

        protected InputCondition(InputSource source, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            InputSource = source;
            WindowMustBeActive = windowMustBeActive;

            Consumable = consumable;
            AllowedIfConsumed = allowedIfConsumed;

            MilliSecondsForConditionMet = milliSecondsForConditionMet;
        }

        public InputSource InputSource { get; protected set; }

        public bool ConditionMetState { get; protected set; }

        public bool Consumable { get; protected set; }

        public bool AllowedIfConsumed { get; protected set; }

        public GameTime CurrentStateStart { get; protected set; }

        public bool WindowMustBeActive { get; protected set; }

        public ValueLogic? ValueLogic { get; protected set; }

        public uint MilliSecondsForConditionMet { get; }

        public bool ConditionMet()
        {
            return InternalConditionMet(Consumable, AllowedIfConsumed);
        }

        public bool ConditionMet(bool consumable, bool allowedIfConsumed)
        {
            return InternalConditionMet(consumable, allowedIfConsumed);
        }


        public abstract bool InternalConditionMet(bool consumable, bool allowedIfConsumed);

        public Value GetValue()
        {
            return ValueLogic == null
                ? new Value(ValueType.None)
                : InternalGetValue();
        }

        protected abstract Value InternalGetValue();

        public abstract void Consume();

        public abstract bool IsConsumed();

        public abstract VilliEventArguments GetArguments();

        protected abstract bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet);

        protected void UpdateState(bool newState)
        {
            ConditionMetState = newState;
            CurrentStateStart = Villi.CurrentTime;
        }

        protected void ConditionMetCleanup(bool consumable, VilliEventArguments arguments)
        {
            if (consumable)
            {
                Consume();
            }

            Event.TriggerEvent(this, arguments);
        }

    }

}
