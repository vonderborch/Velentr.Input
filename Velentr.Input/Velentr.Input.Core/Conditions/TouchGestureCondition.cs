using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Touch;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when the position and type of a Touch gesture matches what we are looking for.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.InputCondition" />
    public class TouchGestureCondition : InputCondition
    {
        /// <summary>
        /// The boundaries
        /// </summary>
        private readonly Rectangle? _boundaries;

        /// <summary>
        /// The parent boundaries
        /// </summary>
        private readonly Rectangle? _parentBoundaries;

        /// <summary>
        /// The gestures
        /// </summary>
        private List<Gesture> _gestures;

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchGestureCondition"/> class.
        /// </summary>
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="type">The type.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        protected TouchGestureCondition(InputManager manager, GestureType type, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.Touch, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
        {
            GestureType = type;
            UseRelativeCoordinates = useRelativeCoordinates;
            _boundaries = boundaries;
            _parentBoundaries = parentBoundaries;

            if (manager.Settings.ThrowWhenCreatingConditionIfNoServiceEnabled && manager.Touch == null)
            {
                throw new Exception(Constants.NoEngineConfiguredError);
            }
        }

        /// <summary>
        /// Gets the type of the gesture.
        /// </summary>
        /// <value>
        /// The type of the gesture.
        /// </value>
        public GestureType GestureType { get; }

        /// <summary>
        /// Gets the boundaries.
        /// </summary>
        /// <value>
        /// The boundaries.
        /// </value>
        public Rectangle Boundaries => _boundaries ?? Manager.Window.ClientBounds;

        /// <summary>
        /// Gets a value indicating whether [use relative coordinates].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use relative coordinates]; otherwise, <c>false</c>.
        /// </value>
        public bool UseRelativeCoordinates { get; }

        /// <summary>
        /// Gets the parent boundaries.
        /// </summary>
        /// <value>
        /// The parent boundaries.
        /// </value>
        public Rectangle ParentBoundaries => _parentBoundaries ?? Manager.Window.ClientBounds;

        /// <summary>
        /// Internal method to determine if the conditions are met or not.
        /// </summary>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <returns></returns>
        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            _gestures = Manager.Touch.FetchValidGestures(GestureType, Boundaries, UseRelativeCoordinates, ParentBoundaries, allowedIfConsumed, MilliSecondsForConditionMet);

            if (_gestures.Count > 0)
            {
                UpdateState(true);
                if (ActionValid(allowedIfConsumed, MilliSecondsForConditionMet))
                {
                    ConditionMetCleanup(consumable, GetArguments());
                    return true;
                }

                return false;
            }

            UpdateState(false);
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
            var ids = new List<int>(_gestures.Count);
            ids.AddRange(_gestures.Select(t => t.Id));

            Manager.Touch.ConsumeGesture(ids);
        }

        /// <summary>
        /// Determines whether the input is consumed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the input is consumed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsConsumed()
        {
            // handled in touch engine when we fetch valid gestures
            return false;
        }

        /// <summary>
        /// Gets the arguments to provide to events that are fired.
        /// </summary>
        /// <returns></returns>
        public override ConditionEventArguments GetArguments()
        {
            return new TouchEventArguments
            {
                Boundaries = Boundaries,
                GestureType = GestureType,
                Condition = this,
                InputSource = InputSource,
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                Gestures = new List<Gesture>(_gestures)
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
            return (
                (!WindowMustBeActive || Manager.IsWindowActive)
                && (MilliSecondsForTimeOut == 0 || Helper.ElapsedMilliSeconds(LastFireTime, Manager.CurrentTime) >= MilliSecondsForTimeOut)
            );
        }

    }

}
