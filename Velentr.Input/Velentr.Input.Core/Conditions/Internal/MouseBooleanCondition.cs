using System;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;

namespace Velentr.Input.Conditions.Internal
{

    public abstract class MouseBooleanCondition : BooleanCondition
    {

        private readonly Rectangle? _parentBoundaries;

        private readonly Rectangle? _boundaries;

        protected MouseBooleanCondition(Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.Mouse, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            _boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
            _parentBoundaries = parentBoundaries;
        }

        public Rectangle Boundaries => _boundaries ?? VelentrInput.Window.ClientBounds;

        public bool UseRelativeCoordinates { get; }

        public Rectangle ParentBoundaries => _parentBoundaries ?? VelentrInput.Window.ClientBounds;

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (
                ((WindowMustBeActive && VelentrInput.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime) >= milliSecondsForConditionMet)
                && (_boundaries == null || MouseService.CursorInBounds(Boundaries, UseRelativeCoordinates, ParentBoundaries))
            );
        }

    }

}
