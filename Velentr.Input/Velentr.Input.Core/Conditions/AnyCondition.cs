using System;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that takes in other Input Conditions, any of which must be valid for it to be valid.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.InputCondition" />
    public class AnyCondition : InputCondition
    {
        /// <summary>
        /// The arguments
        /// </summary>
        private ConditionEventArguments _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnyCondition"/> class.
        /// </summary>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="conditions">The conditions.</param>
        public AnyCondition(bool windowMustBeActive = true, params InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive, false, true, 0)
        {
            Conditions = conditions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnyCondition"/> class.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        public AnyCondition(params InputCondition[] conditions) : base(InputSource.AnyConditional, true, false, true, 0)
        {
            Conditions = conditions;
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public InputCondition[] Conditions { get; }

        /// <summary>
        /// Internal method to determine if the conditions are met or not.
        /// </summary>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <returns></returns>
        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            _arguments = null;
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (Conditions[i].IsConditionMet())
                    {
                        _arguments = Conditions[i].GetArguments();
                        if (!ConditionMetState)
                        {
                            UpdateState(true);
                        }
                        ConditionMetCleanup(consumable, GetArguments());
                        return true;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            if (ConditionMetState)
            {
                UpdateState(false);
            }
            return false;
        }

        /// <summary>
        /// Internals the get value.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    Conditions[i].Consume();
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Determines whether the input is consumed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the input is consumed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsConsumed()
        {
            return false;
        }

        /// <summary>
        /// Gets the arguments to provide to events that are fired.
        /// </summary>
        /// <returns></returns>
        public override ConditionEventArguments GetArguments()
        {
            return new AnyConditionalEventArguments
            {
                Conditions = Conditions,
                Condition = this,
                InputSource = InputSource,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                ValidConditionArguments = _arguments.Clone()
            };
        }

        /// <summary>
        /// Checks to see if the input is valid.
        /// </summary>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns></returns>
        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return true;
        }

    }

}
