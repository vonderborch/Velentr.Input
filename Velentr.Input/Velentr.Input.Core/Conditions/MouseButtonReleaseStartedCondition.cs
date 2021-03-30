using Microsoft.Xna.Framework;
using Velentr.Input.Conditions.Internal;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;
using Velentr.Input.Mouse;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when a button on the Mouse has been pressed but is now released.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.MouseBooleanCondition" />
    public class MouseButtonReleaseStartedCondition : MouseBooleanCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseButtonReleaseStartedCondition"/> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        public MouseButtonReleaseStartedCondition(MouseButton button, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = true, uint milliSecondsForConditionMet = 0) : base(boundaries, useRelativeCoordinates, parentBoundaries, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Button = button;
        }

        /// <summary>
        /// Gets the button.
        /// </summary>
        /// <value>
        /// The button.
        /// </value>
        public MouseButton Button { get; }

        /// <summary>
        /// Gets the arguments to provide to events that are fired.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            VelentrInput.System.Mouse.ConsumeButton(Button);
        }

        /// <summary>
        /// Currents the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool CurrentStateValid()
        {
            return MouseService.IsReleased(Button);
        }

        /// <summary>
        /// Determines whether the input is consumed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the input is consumed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsConsumed()
        {
            return VelentrInput.System.Mouse.IsButtonConsumed(Button);
        }

        /// <summary>
        /// Previouses the state valid.
        /// </summary>
        /// <returns></returns>
        protected override bool PreviousStateValid()
        {
            return MouseService.WasPressed(Button);
        }

    }

}
