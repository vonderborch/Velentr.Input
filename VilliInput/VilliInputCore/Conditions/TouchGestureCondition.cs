using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Mouse;
using VilliInput.Touch;

namespace VilliInput.Conditions
{
    public class TouchGestureCondition : InputCondition
    {
        public GestureType GestureType { get; private set; }

        private readonly Rectangle? _parentBoundaries;

        private readonly Rectangle? _boundaries;

        public Rectangle Boundaries => _boundaries ?? Villi.Window.ClientBounds;

        public bool UseRelativeCoordinates { get; private set; }

        public Rectangle ParentBoundaries => _parentBoundaries ?? Villi.Window.ClientBounds;

        private List<Gesture> _gestures;

        protected TouchGestureCondition(GestureType type, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(InputSource.Touch, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            GestureType = type;
            UseRelativeCoordinates = useRelativeCoordinates;
            _boundaries = boundaries;
            _parentBoundaries = parentBoundaries;
        }


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
            else
            {
                UpdateState(false);
                return false;
            }
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
            return new TouchEventArguments()
            {
                Boundaries = this.Boundaries,
                GestureType = this.GestureType,
                Condition = this,
                InputSource = this.InputSource,
                MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                UseRelativeCoordinates = this.UseRelativeCoordinates,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
                Gestures = new List<Gesture>(_gestures),
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || Villi.IsWindowActive);
        }

    }
}
