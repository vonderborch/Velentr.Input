using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;

namespace Velentr.Input.Conditions
{

    public abstract class InputCondition
    {

        public InputConditionEvent Event = new InputConditionEvent();

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

        public bool IsConditionMet()
        {
            return InternalConditionMet(Consumable, AllowedIfConsumed);
        }

        public bool IsConditionMet(bool consumable, bool allowedIfConsumed)
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

        public abstract ConditionEventArguments GetArguments();

        protected abstract bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet);

        protected void UpdateState(bool newState)
        {
            ConditionMetState = newState;
            CurrentStateStart = VelentrInput.CurrentTime;
        }

        protected void ConditionMetCleanup(bool consumable, ConditionEventArguments arguments)
        {
            if (consumable)
            {
                Consume();
            }

            Event.TriggerEvent(this, arguments);
        }

    }

}
