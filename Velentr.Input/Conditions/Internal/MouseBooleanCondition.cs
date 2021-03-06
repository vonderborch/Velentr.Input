using System;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.Helpers;

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

            if (manager.Settings.ThrowWhenCreatingConditionIfNoServiceEnabled && manager.Mouse == null)
            {
                throw new Exception(Constants.NoEngineConfiguredError);
            }
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
                && (MilliSecondsForTimeOut == 0 || Helper.ElapsedMilliSeconds(LastFireTime, Manager.CurrentTime) >= MilliSecondsForTimeOut)
                && (_boundaries == null || Manager.Mouse.CursorInBounds(Boundaries, UseRelativeCoordinates, ParentBoundaries))
            );
        }

    }

}
