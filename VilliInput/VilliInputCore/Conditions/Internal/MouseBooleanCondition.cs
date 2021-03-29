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

        protected MouseBooleanCondition(Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(InputSource.Mouse, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
            _parentBoundaries = parentBoundaries;
        }

        public Rectangle? Boundaries { get; }

        public bool UseRelativeCoordinates { get; }

        public Rectangle? ParentBoundaries => _parentBoundaries ?? Villi.Window.ClientBounds;

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || Villi.IsWindowActive && MouseService.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet)
                   && (Boundaries == null || MouseService.CursorInBounds((Rectangle) Boundaries, UseRelativeCoordinates, ParentBoundaries));
        }

    }

}
