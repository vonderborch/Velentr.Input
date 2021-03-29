using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput.Conditions
{

    public class MouseButtonReleaseStartedCondition : MouseBooleanCondition
    {

        public MouseButtonReleaseStartedCondition(MouseButton button, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(boundaries, useRelativeCoordinates, parentBoundaries, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Button = button;
        }

        public MouseButton Button { get; }

        public override VilliEventArguments GetArguments()
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
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeButton(Button);
        }

        protected override bool CurrentStateValid()
        {
            return MouseService.IsReleased(Button);
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
