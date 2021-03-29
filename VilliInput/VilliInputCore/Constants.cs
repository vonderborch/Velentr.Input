using Microsoft.Xna.Framework.Input;
using VilliInput.Touch;
using XnaGestureType = Microsoft.Xna.Framework.Input.Touch.GestureType;

namespace VilliInput
{

    public sealed class Constants
    {

        public const string PlayerIndexExceptionMessage = "playerIndex must be 0 or greater, and less than the maximum supported gamepads on the system";

        public const string InvalidGamePadStickSensor = "Invalid sensor! Must use GamePadSensor.LeftStick or GamePadSensor.RightStick!";

        public const string InvalidGamePadTriggerSensor = "Invalid sensor! Must use GamePadSensor.LeftTrigger or GamePadSensor.RightTrigger!";

        public const string PlayerIndexExceptionMessageButtonState = "playerIndex must be less than the maximum supported gamepads on the system";

        static Constants() { }

        private Constants() { }

        public static Constants Settings { get; } = new Constants();

        public GamePadDeadZone DefaultGamePadDeadZone { get; set; } = GamePadDeadZone.IndependentAxes;

        public int SecondsBetweenGamePadConnectionCheck { get; set; } = 15;

        public TouchEngines TouchEngine { get; set; } = TouchEngines.XNA_derived;

        public XnaGestureType EnabledTouchGestures { get; set; } = XnaGestureType.Tap | XnaGestureType.DoubleTap | XnaGestureType.FreeDrag | XnaGestureType.DragComplete | XnaGestureType.Pinch | XnaGestureType.PinchComplete;

        public float MaxFloatDifference { get; set; } = 0.00001f;

        public double MaxDoubleDifference { get; set; } = 0.0000001d;

    }

}
