using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.EventArguments;
using VilliInput.MouseInput;

namespace VilliInput.Conditions
{
    public class MousePointerDeltaCondition : MouseSensorCondition
    {
        public Rectangle? Boundaries { get; private set; }

        public bool UseRelativeCoordinates { get; private set; }

        public uint SecondsForPressed { get; private set; }

        public uint SecondsForReleased { get; private set; }

        public MousePointerDeltaCondition(bool windowMustBeActive = true, Rectangle? boundaries = null, bool useRelativeCoordinates = false, uint secondsForPressed = 0, uint secondsForReleased = 0) : base(MouseSensor.Pointer, windowMustBeActive)
        {
            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;

            SecondsForPressed = secondsForPressed;
            SecondsForReleased = secondsForReleased;
        }

        private bool ActionValid(bool ignoredConsumed, uint actionTime)
        {
            return (!WindowMustBeActive || (Villi.IsWindowActive && MouseHelpers.IsMouseInWindow))
                   && (ignoredConsumed || Villi.System.Mouse.IsSensorConsumed(Sensor))
                   && (actionTime == 0 || Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime) >= actionTime)
                   && (Boundaries == null || MouseHelpers.PointerInBoundaries((Rectangle)Boundaries, UseRelativeCoordinates, Villi.Window.ClientBounds));
        }

        public override bool Pressed(bool consumable = true, bool ignoredConsumed = false)
        {
            if (MouseHelpers.PointerMoved && CurrentState != ConditionState.Pressed)
            {
                UpdateState(ConditionState.Pressed);
            }
            else if (!MouseHelpers.PointerMoved && CurrentState == ConditionState.Pressed)
            {
                UpdateState(ConditionState.Released);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && MouseHelpers.PointerMoved)
            {
                InternalPressed(consumable);
                return true;
            }

            return false;
        }

        public override bool PressStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            throw new NotImplementedException();
        }

        public override bool Released(bool consumable = true, bool ignoredConsumed = false)
        {
            if (!MouseHelpers.PointerMoved && CurrentState != ConditionState.Released)
            {
                UpdateState(ConditionState.Released);
            }
            else if (MouseHelpers.PointerMoved && CurrentState == ConditionState.Released)
            {
                UpdateState(ConditionState.Pressed);
            }

            if (ActionValid(ignoredConsumed, SecondsForPressed) && !MouseHelpers.PointerMoved)
            {
                InternalReleased(consumable);
                return true;
            }

            return false;
        }

        public override bool ReleaseStarted(bool consumable = true, bool ignoredConsumed = false)
        {
            throw new NotImplementedException();
        }

        internal override VilliEventArguments GetArguments()
        {
            return new MousePointerDeltaEventArguments()
            {
                Boundaries = this.Boundaries,
                Sensor = this.Sensor,
                ConditionSource = this,
                InputSource = this.Source,
                MouseCoordinates = MouseHelpers.CurrentCoordinates,
                RelativeMouseCoordinates = Helpers.ScalePointToChild(MouseHelpers.CurrentCoordinates, Villi.Window.ClientBounds, Boundaries ?? Villi.Window.ClientBounds),
                SecondsForPressed = this.SecondsForPressed,
                SecondsForReleased = this.SecondsForReleased,
                UseRelativeCoordinates = this.UseRelativeCoordinates,
                ConditionState = this.CurrentState,
                ConditionStateStartTime = this.CurrentStateStart,
                ConditionStateTimeSeconds = Helpers.ElapsedSeconds(CurrentStateStart, Villi.CurrentTime),
                Delta = MouseHelpers.PointerDelta,
                DeltaVector = MouseHelpers.PointerDeltaVector,
                WindowMustBeActive = this.WindowMustBeActive,
            };
        }
    }
}
