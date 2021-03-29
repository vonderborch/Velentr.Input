using Microsoft.Xna.Framework;
using Velentr.Input.Conditions.Internal;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;

namespace Velentr.Input.Conditions
{

    public class MouseButtonPressedCondition : MouseBooleanCondition
    {

        public MouseButtonPressedCondition(MouseButton button, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(boundaries, useRelativeCoordinates, parentBoundaries, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Button = button;
        }

        public MouseButton Button { get; }

        public override ConditionEventArguments GetArguments()
        {
            return new MouseButtonEventArguments
            {
                Boundaries = Boundaries,
                Button = Button,
                Condition = this,
                InputSource = InputSource,
                MouseCoordinates = MouseService.CurrentCursorPosition,
                RelativeMouseCoordinates = Helper.ScalePointToChild(MouseService.CurrentCursorPosition, ParentBoundaries, Boundaries),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, VelentrInput.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
        }

        public override void Consume()
        {
            VelentrInput.System.Mouse.ConsumeButton(Button);
        }

        protected override bool CurrentStateValid()
        {
            return MouseService.IsPressed(Button);
        }

        public override bool IsConsumed()
        {
            return VelentrInput.System.Mouse.IsButtonConsumed(Button);
        }

        protected override bool PreviousStateValid()
        {
            return MouseService.WasPressed(Button);
        }

    }

}
