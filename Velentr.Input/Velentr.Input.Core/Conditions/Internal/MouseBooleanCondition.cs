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

        protected MouseBooleanCondition(InputManager manager, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, InputSource.Mouse, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut)
        {
            _boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
            _parentBoundaries = parentBoundaries;
        }

        public Rectangle Boundaries => _boundaries ?? Manager.Window.ClientBounds;

        public bool UseRelativeCoordinates { get; }

        public Rectangle ParentBoundaries => _parentBoundaries ?? Manager.Window.ClientBounds;

        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (
                ((WindowMustBeActive && Manager.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime) >= milliSecondsForConditionMet)
                && (_boundaries == null || Manager.Mouse.CursorInBounds(Boundaries, UseRelativeCoordinates, ParentBoundaries))
            );
        }

    }

}
