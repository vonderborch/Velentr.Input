using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Velentr.Input.Enums;
using Velentr.Input.Helpers;

namespace Velentr.Input.Mouse
{

    public class MouseService : InputService
    {

        private static readonly List<MouseButton> buttons = new List<MouseButton>(Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>().ToList());

        internal static Dictionary<MouseButton, ulong> ButtonLastConsumed = new Dictionary<MouseButton, ulong>(Enum.GetNames(typeof(MouseButton)).Length);

        internal static Dictionary<MouseSensor, ulong> SensorLastConsumed = new Dictionary<MouseSensor, ulong>(Enum.GetNames(typeof(MouseSensor)).Length);

        internal static Dictionary<MouseButton, Func<MouseState, ButtonState>> ButtonMapping = new Dictionary<MouseButton, Func<MouseState, ButtonState>>(Enum.GetNames(typeof(MouseButton)).Length)
        {
            {MouseButton.LeftButton, state => state.LeftButton},
            {MouseButton.MiddleButton, state => state.MiddleButton},
            {MouseButton.RightButton, state => state.RightButton},
            {MouseButton.XButton1, state => state.XButton1},
            {MouseButton.XButton2, state => state.XButton2}
        };

        public MouseService(InputManager inputManager) : base(inputManager)
        {
            Source = InputSource.Mouse;
        }

        public static MouseState PreviousState { get; private set; }

        public static MouseState CurrentState { get; private set; }

        public bool ResetMouseCoordsToCenterOfScreen { get; set; } = false;

#if MONOGAME
        public static int CurrentHorizontalScrollWheelValue => CurrentState.HorizontalScrollWheelValue;

        public static int PreviousHorizontalScrollWheelValue => PreviousState.HorizontalScrollWheelValue;
#else
        public static int CurrentHorizontalScrollWheelValue => CurrentState.ScrollWheelValue;

        public static int PreviousHorizontalScrollWheelValue => PreviousState.ScrollWheelValue;
#endif

        public static int CurrentVerticalScrollWheelValue => CurrentState.ScrollWheelValue;

        public static int PreviousVerticalScrollWheelValue => PreviousState.ScrollWheelValue;

        public static int HorizontalScrollDelta => CurrentHorizontalScrollWheelValue - PreviousHorizontalScrollWheelValue;

        public static int VerticalScrollDelta => CurrentVerticalScrollWheelValue - PreviousVerticalScrollWheelValue;

        public static Point ScrollDelta => new Point(HorizontalScrollDelta, VerticalScrollDelta);

        public static Point CurrentScrollPositions => new Point(CurrentHorizontalScrollWheelValue, CurrentVerticalScrollWheelValue);

        public static Point PreviousScrollPositions => new Point(PreviousHorizontalScrollWheelValue, PreviousVerticalScrollWheelValue);

#if MONOGAME
        public static Point CurrentCursorPosition => CurrentState.Position;

        public static Point PreviousCursorPosition => PreviousState.Position;
#else
        public static Point CurrentCursorPosition => new Point(CurrentState.X, CurrentState.Y);

        public static Point PreviousCursorPosition => new Point(PreviousState.X, PreviousState.Y);
#endif

        public static Point CursorPositionDelta => CurrentCursorPosition - PreviousCursorPosition;

        public static bool ScrolledVertically => VerticalScrollDelta != 0;

        public static bool ScrolledHorizontally => HorizontalScrollDelta != 0;

        public static bool Scrolled => ScrolledVertically || ScrolledHorizontally;

        public static bool CursorMoved => CursorPositionDelta != Point.Zero;

        public static bool IsMouseInWindow => VelentrInput.IsWindowActive && Helper.CoordinateInRectangle(CurrentCursorPosition, VelentrInput.Window.ClientBounds);

        public override void Setup()
        {
            PreviousState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            CurrentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        public override void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            if (ResetMouseCoordsToCenterOfScreen)
            {
                Microsoft.Xna.Framework.Input.Mouse.SetPosition(VelentrInput.CenterCoordinates.X, VelentrInput.CenterCoordinates.Y);
            }
        }

        public void ConsumeButton(MouseButton button)
        {
            ButtonLastConsumed[button] = VelentrInput.CurrentFrame;
        }

        public void ConsumeSensor(MouseSensor sensor)
        {
            SensorLastConsumed[sensor] = VelentrInput.CurrentFrame;
        }

        public bool IsButtonConsumed(MouseButton button)
        {
            if (ButtonLastConsumed.TryGetValue(button, out var frame))
            {
                return frame == VelentrInput.CurrentFrame;
            }

            return false;
        }

        public bool IsSensorConsumed(MouseSensor sensor)
        {
            if (SensorLastConsumed.TryGetValue(sensor, out var frame))
            {
                return frame == VelentrInput.CurrentFrame;
            }

            return false;
        }

        public static bool IsPressed(MouseButton button)
        {
            return ButtonMapping[button](CurrentState) == ButtonState.Pressed;
        }

        public static bool WasPressed(MouseButton button)
        {
            return ButtonMapping[button](PreviousState) == ButtonState.Pressed;
        }

        public static bool IsReleased(MouseButton button)
        {
            return ButtonMapping[button](CurrentState) == ButtonState.Released;
        }

        public static bool WasReleased(MouseButton button)
        {
            return ButtonMapping[button](PreviousState) == ButtonState.Released;
        }

        public static bool CursorInBounds(Rectangle boundaries, bool scaleCoordinatesToArea = false, Rectangle? parentBoundaries = null)
        {
            return Helper.CoordinateInRectangle(
                scaleCoordinatesToArea
                    ? Helper.ScalePointToChild(CurrentCursorPosition, parentBoundaries ?? VelentrInput.Window.ClientBounds, boundaries)
                    : CurrentCursorPosition,
                boundaries
            );
        }

    }

}
