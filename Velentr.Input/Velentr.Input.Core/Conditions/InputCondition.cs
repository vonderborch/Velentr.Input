using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition
    /// </summary>
    public abstract class InputCondition
    {
        /// <summary>
        /// The event to fire when the input conditions are met
        /// </summary>
        public InputConditionEvent Event = new InputConditionEvent();

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCondition"/> class.
        /// </summary>
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="source">The source.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        protected InputCondition(InputManager manager, InputSource source, bool windowMustBeActive, bool consumable, bool allowedIfConsumed, uint milliSecondsForConditionMet, uint milliSecondsForTimeOut)
        {
            InputSource = source;
            WindowMustBeActive = windowMustBeActive;

            Consumable = consumable;
            AllowedIfConsumed = allowedIfConsumed;

            MilliSecondsForConditionMet = milliSecondsForConditionMet;
            MilliSecondsForTimeOut = milliSecondsForTimeOut;
            Manager = manager;
        }

        /// <summary>
        /// Gets the input manager.
        /// </summary>
        /// <value>
        /// The input manager.
        /// </value>
        public InputManager Manager { get; protected set; }

        /// <summary>
        /// Gets the input source.
        /// </summary>
        /// <value>
        /// The input source.
        /// </value>
        public InputSource InputSource { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether [condition met state].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [condition met state]; otherwise, <c>false</c>.
        /// </value>
        public bool ConditionMetState { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="InputCondition"/> is consumable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if consumable; otherwise, <c>false</c>.
        /// </value>
        public bool Consumable { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether [allowed if consumed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allowed if consumed]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowedIfConsumed { get; protected set; }

        /// <summary>
        /// Gets the current state start.
        /// </summary>
        /// <value>
        /// The current state start.
        /// </value>
        public GameTime CurrentStateStart { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether [window must be active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [window must be active]; otherwise, <c>false</c>.
        /// </value>
        public bool WindowMustBeActive { get; protected set; }

        /// <summary>
        /// Gets the value logic.
        /// </summary>
        /// <value>
        /// The value logic.
        /// </value>
        public ValueLogic? ValueLogic { get; protected set; }

        /// <summary>
        /// Gets the milliseconds when inputs are valid before the condition will be met.
        /// </summary>
        /// <value>
        /// The milli seconds for condition met.
        /// </value>
        public uint MilliSecondsForConditionMet { get; protected set; }

        /// <summary>
        /// Gets the last time the condition was met and fired.
        /// </summary>
        /// <value>
        /// The last fire time.
        /// </value>
        public GameTime LastFireTime { get; protected set; }

        /// <summary>
        /// Gets the milliseconds when inputs are valid between the condition being met.
        /// </summary>
        /// <value>
        /// The milli seconds for time out.
        /// </value>
        public uint MilliSecondsForTimeOut { get; protected set; }

        /// <summary>
        /// Determines whether the input condition is met.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is condition met]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsConditionMet()
        {
            return InternalConditionMet(Consumable, AllowedIfConsumed);
        }

        /// <summary>
        /// Determines whether [is condition met] [the specified consumable].
        /// </summary>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <returns>
        ///   <c>true</c> if [is condition met] [the specified consumable]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsConditionMet(bool consumable, bool allowedIfConsumed)
        {
            return InternalConditionMet(consumable, allowedIfConsumed);
        }

        /// <summary>
        /// Internal method to determine if the conditions are met or not.
        /// </summary>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <returns></returns>
        public abstract bool InternalConditionMet(bool consumable, bool allowedIfConsumed);

        /// <summary>
        /// Gets the current value associated with the input condition (i.e. for sensors).
        /// </summary>
        /// <returns></returns>
        public Value GetValue()
        {
            return ValueLogic == null
                ? new Value(ValueType.None)
                : InternalGetValue();
        }

        /// <summary>
        /// Internals the get value.
        /// </summary>
        /// <returns></returns>
        protected abstract Value InternalGetValue();

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public abstract void Consume();

        /// <summary>
        /// Determines whether the input is consumed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the input is consumed; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsConsumed();

        /// <summary>
        /// Gets the arguments to provide to events that are fired.
        /// </summary>
        /// <returns></returns>
        public abstract ConditionEventArguments GetArguments();

        /// <summary>
        /// Checks to see if the input is valid.
        /// </summary>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns></returns>
        protected abstract bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet);

        /// <summary>
        /// Updates the state of the condition.
        /// </summary>
        /// <param name="newState">if set to <c>true</c> [new state].</param>
        protected void UpdateState(bool newState)
        {
            ConditionMetState = newState;
            CurrentStateStart = Manager.CurrentTime;

            if (newState)
            {
                LastFireTime = Manager.CurrentTime;
            }
        }

        /// <summary>
        /// Cleanup that is run when conditions are met.
        /// </summary>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="arguments">The arguments.</param>
        protected void ConditionMetCleanup(bool consumable, ConditionEventArguments arguments)
        {
            if (consumable)
            {
                Consume();
            }

            LastFireTime = Manager.CurrentTime;
            if (Event.Delegates.Count > 0)
            {
                Event.TriggerEvent(this, arguments);
            }
        }

    }

}
