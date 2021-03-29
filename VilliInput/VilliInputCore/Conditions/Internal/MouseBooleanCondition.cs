using System;
using Microsoft.Xna.Framework;
using VilliInput.Enums;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions.Internal
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

        public Rectangle Boundaries => _boundaries ?? Villi.Window.ClientBounds;

        public bool UseRelativeCoordinates { get; }

        public Rectangle ParentBoundaries => _parentBoundaries ?? Villi.Window.ClientBounds;

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (
                ((WindowMustBeActive && Villi.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet)
                && (_boundaries == null || MouseService.CursorInBounds(Boundaries, UseRelativeCoordinates, ParentBoundaries))
            );
        }

    }

}
