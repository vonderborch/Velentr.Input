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

    public class TouchGestureCondition : InputCondition
    {

        private readonly Rectangle? _boundaries;

        private readonly Rectangle? _parentBoundaries;

        private List<Gesture> _gestures;

        protected TouchGestureCondition(GestureType type, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(InputSource.Touch, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            GestureType = type;
            UseRelativeCoordinates = useRelativeCoordinates;
            _boundaries = boundaries;
            _parentBoundaries = parentBoundaries;
        }

        public GestureType GestureType { get; }

        public Rectangle Boundaries => _boundaries ?? VelentrInput.Window.ClientBounds;

        public bool UseRelativeCoordinates { get; }

        public Rectangle ParentBoundaries => _parentBoundaries ?? VelentrInput.Window.ClientBounds;


        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            _gestures = VelentrInput.System.Touch.FetchValidGestures(GestureType, Boundaries, UseRelativeCoordinates, ParentBoundaries, allowedIfConsumed, MilliSecondsForConditionMet);

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

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        public override void Consume()
        {
            var ids = new List<int>(_gestures.Count);
            ids.AddRange(_gestures.Select(t => t.Id));

            VelentrInput.System.Touch.ConsumeGesture(ids);
        }

        public override bool IsConsumed()
        {
            // handled in touch engine when we fetch valid gestures
            return false;
        }

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
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                Gestures = new List<Gesture>(_gestures)
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return !WindowMustBeActive || VelentrInput.IsWindowActive;
        }

    }

}
