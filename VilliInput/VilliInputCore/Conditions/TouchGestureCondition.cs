using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Touch;

namespace VilliInput.Conditions
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

        public Rectangle Boundaries => _boundaries ?? Villi.Window.ClientBounds;

        public bool UseRelativeCoordinates { get; }

        public Rectangle ParentBoundaries => _parentBoundaries ?? Villi.Window.ClientBounds;


        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            _gestures = Villi.System.Touch.FetchValidGestures(GestureType, Boundaries, UseRelativeCoordinates, ParentBoundaries, allowedIfConsumed, MilliSecondsForConditionMet);

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

            Villi.System.Touch.ConsumeGesture(ids);
        }

        public override bool IsConsumed()
        {
            // handled in touch engine when we fetch valid gestures
            return false;
        }

        public override VilliEventArguments GetArguments()
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
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                Gestures = new List<Gesture>(_gestures)
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return !WindowMustBeActive || Villi.IsWindowActive;
        }

    }

}
