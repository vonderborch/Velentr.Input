using Microsoft.Xna.Framework.Input;
using Velentr.Input.Touch;
using Velentr.Input.Voice;
using XnaGestureType = Microsoft.Xna.Framework.Input.Touch.GestureType;

namespace Velentr.Input
{

    public sealed class Constants
    {
        /// <summary>
        /// The player index exception message exception message
        /// </summary>
        public const string PlayerIndexExceptionMessage = "playerIndex must be 0 or greater, and less than the maximum supported gamepads on the system";

        /// <summary>
        /// The invalid game pad stick sensor exception message
        /// </summary>
        public const string InvalidGamePadStickSensor = "Invalid sensor! Must use GamePadSensor.LeftStick or GamePadSensor.RightStick!";

        /// <summary>
        /// The invalid game pad trigger sensor exception message
        /// </summary>
        public const string InvalidGamePadTriggerSensor = "Invalid sensor! Must use GamePadSensor.LeftTrigger or GamePadSensor.RightTrigger!";

        static Constants() { }

        private Constants() { }

        /// <summary>
        /// Settings for Velentr.Input.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public static Constants Settings { get; } = new Constants();

        /// <summary>
        /// Gets or sets the default game pad dead zone.
        /// </summary>
        /// <value>
        /// The default game pad dead zone.
        /// </value>
        public GamePadDeadZone DefaultGamePadDeadZone { get; set; } = GamePadDeadZone.IndependentAxes;

        /// <summary>
        /// Gets or sets the seconds between game pad connection check.
        /// </summary>
        /// <value>
        /// The seconds between game pad connection check.
        /// </value>
        public int SecondsBetweenGamePadConnectionCheck { get; set; } = 15;

        /// <summary>
        /// Gets or sets the default touch engine
        /// </summary>
        /// <value>
        /// The touch engine.
        /// </value>
        public TouchEngines TouchEngine { get; set; } = TouchEngines.XNA_derived;

        /// <summary>
        /// Gets or sets the voice engine we'll use for voice commands.
        /// </summary>
        /// <value>
        /// The voice engine.
        /// </value>
        public VoiceEngine VoiceEngine { get; set; } = null;

        /// <summary>
        /// Gets or sets the enabled touch gestures.
        /// </summary>
        /// <value>
        /// The enabled touch gestures.
        /// </value>
        public XnaGestureType EnabledTouchGestures { get; set; } = XnaGestureType.Tap | XnaGestureType.DoubleTap | XnaGestureType.FreeDrag | XnaGestureType.DragComplete | XnaGestureType.Pinch | XnaGestureType.PinchComplete;

        /// <summary>
        /// Gets or sets the maximum float difference.
        /// </summary>
        /// <value>
        /// The maximum float difference.
        /// </value>
        public float MaxFloatDifference { get; set; } = 0.00001f;

        /// <summary>
        /// Gets or sets the maximum double difference.
        /// </summary>
        /// <value>
        /// The maximum double difference.
        /// </value>
        public double MaxDoubleDifference { get; set; } = 0.0000001d;

        public static string GamePadService = "GAMEPAD_SERVICE";

        public static string KeyboardService = "KEYBOARD_SERVICE";

        public static string MouseService = "MOUSE_SERVICE";

        public static string TouchService = "TOUCH_SERVICE";

        public static string VoiceService = "VOICE_SERVICE";

    }

}
