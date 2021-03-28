using Microsoft.Xna.Framework.Input;

namespace VilliInput
{
    public sealed class Constants
    {
        static Constants() { }
        private Constants() { }

        public static Constants Settings { get; } = new Constants();

        public GamePadDeadZone DefaultGamePadDeadZone { get; set; } = GamePadDeadZone.IndependentAxes;

        public int SecondsBetweenGamePadConnectionCheck { get; set; } = 15;

        public const string PlayerIndexExceptionMessage = "playerIndex must be 0 or greater, and less than the maximum supported gamepads on the system";

        public const string InvalidGamePadStickSensor = "Invalid sensor! Must use GamePadSensor.LeftStick or GamePadSensor.RightStick!";

        public const string InvalidGamePadTriggerSensor = "Invalid sensor! Must use GamePadSensor.LeftTrigger or GamePadSensor.RightTrigger!";

        public const string PlayerIndexExceptionMessageButtonState = "playerIndex must be less than the maximum supported gamepads on the system";

        public float MaxFloatDifference { get; set; } = 0.00001f;

        public double MaxDoubleDifference { get; set; } = 0.0000001d;

    }
}
