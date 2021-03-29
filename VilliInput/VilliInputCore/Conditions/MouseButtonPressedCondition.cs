﻿using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{
    public class MouseButtonPressedCondition : MouseBooleanCondition
    {

        public MouseButton Button { get; private set; }

        public MouseButtonPressedCondition(MouseButton button, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(boundaries, useRelativeCoordinates, parentBoundaries, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Button = button;
        }

        public override VilliEventArguments GetArguments()
        {
            return new MouseButtonEventArguments()
            {
                Boundaries = this.Boundaries,
                Button = this.Button,
                Condition = this,
                InputSource = this.InputSource,
                MouseCoordinates = MouseService.CurrentCursorPosition,
                RelativeMouseCoordinates = Helper.ScalePointToChild(MouseService.CurrentCursorPosition, ParentBoundaries ?? Villi.Window.ClientBounds, Boundaries ?? Villi.Window.ClientBounds),
                MilliSecondsForConditionMet = this.MilliSecondsForConditionMet,
                UseRelativeCoordinates = this.UseRelativeCoordinates,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = this.WindowMustBeActive,
            };
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeButton(Button);
        }

        protected override bool CurrentStateValid()
        {
            return MouseService.IsPressed(Button);
        }

        public override bool IsConsumed()
        {
            return Villi.System.Mouse.IsButtonConsumed(Button);
        }

        protected override bool PreviousStateValid()
        {
            return MouseService.WasPressed(Button);
        }

    }
}