﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that takes in other Input Conditions, all of which must be valid for it to be valid.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.InputCondition" />
    public class AllCondition : InputCondition
    {
        /// <summary>
        /// The arguments
        /// </summary>
        private List<ConditionEventArguments> _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllCondition"/> class.
        /// </summary>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="orderMatters">if set to <c>true</c> [order matters].</param>
        /// <param name="conditions">The conditions.</param>
        public AllCondition(bool windowMustBeActive = true, bool orderMatters = false, params InputCondition[] conditions) : base(InputSource.AnyConditional, windowMustBeActive, false, true, 0)
        {
            Conditions = conditions;
            OrderMatters = orderMatters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllCondition"/> class.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        public AllCondition(params InputCondition[] conditions) : base(InputSource.AnyConditional, true, false, true, 0)
        {
            Conditions = conditions;
            OrderMatters = false;
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        /// <value>
        /// The conditions.
        /// </value>
        public InputCondition[] Conditions { get; }

        /// <summary>
        /// Gets a value indicating whether [order matters].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [order matters]; otherwise, <c>false</c>.
        /// </value>
        public bool OrderMatters { get; }

        /// <summary>
        /// Internal method to determine if the conditions are met or not.
        /// </summary>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <returns></returns>
        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            GameTime time = null;
            _arguments = new List<ConditionEventArguments>(Conditions.Length);

            for (var i = 0; i < Conditions.Length; i++)
            {
                try
                {
                    if (!Conditions[i].IsConditionMet())
                    {
                        if (ConditionMetState)
                        {
                            UpdateState(false);
                        }
                        return false;
                    }

                    if (time == null || time.TotalGameTime <= Conditions[i].CurrentStateStart.TotalGameTime)
                    {
                        time = Conditions[i].CurrentStateStart;
                        _arguments.Add(Conditions[i].GetArguments());
                    }
                    else
                    {
                        if (ConditionMetState)
                        {
                            UpdateState(false);
                        }
                        return false;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            if (!ConditionMetState)
            {
                UpdateState(true);
            }
            ConditionMetCleanup(consumable, GetArguments());
            return true;
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
            var args = new List<ConditionEventArguments>(_arguments.Count);
            foreach (var arg in _arguments)
            {
                args.Add(arg.Clone());
            }

            return new AllConditionEventArguments
            {
                Conditions = Conditions,
                Condition = this,
                InputSource = InputSource,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                ConditionEventArguments = args
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
