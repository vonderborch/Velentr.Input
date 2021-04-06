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

        private readonly List<MouseButton> buttons = new List<MouseButton>(Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>().ToList());

        internal Dictionary<MouseButton, ulong> ButtonLastConsumed = new Dictionary<MouseButton, ulong>(Enum.GetNames(typeof(MouseButton)).Length);

        internal Dictionary<MouseSensor, ulong> SensorLastConsumed = new Dictionary<MouseSensor, ulong>(Enum.GetNames(typeof(MouseSensor)).Length);

        internal Dictionary<MouseButton, Func<MouseState, ButtonState>> ButtonMapping = new Dictionary<MouseButton, Func<MouseState, ButtonState>>(Enum.GetNames(typeof(MouseButton)).Length)
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

        public MouseState PreviousState { get; private set; }

        public MouseState CurrentState { get; private set; }

        public bool ResetMouseCoordsToCenterOfScreen { get; set; } = false;

#if MONOGAME
        public int CurrentHorizontalScrollWheelValue => CurrentState.HorizontalScrollWheelValue;

        public int PreviousHorizontalScrollWheelValue => PreviousState.HorizontalScrollWheelValue;
#else
        public int CurrentHorizontalScrollWheelValue => CurrentState.ScrollWheelValue;

        public int PreviousHorizontalScrollWheelValue => PreviousState.ScrollWheelValue;
#endif

        public int CurrentVerticalScrollWheelValue => CurrentState.ScrollWheelValue;

        public int PreviousVerticalScrollWheelValue => PreviousState.ScrollWheelValue;

        public int HorizontalScrollDelta => CurrentHorizontalScrollWheelValue - PreviousHorizontalScrollWheelValue;

        public int VerticalScrollDelta => CurrentVerticalScrollWheelValue - PreviousVerticalScrollWheelValue;

        public Point ScrollDelta => new Point(HorizontalScrollDelta, VerticalScrollDelta);

        public Point CurrentScrollPositions => new Point(CurrentHorizontalScrollWheelValue, CurrentVerticalScrollWheelValue);

        public Point PreviousScrollPositions => new Point(PreviousHorizontalScrollWheelValue, PreviousVerticalScrollWheelValue);

#if MONOGAME
        public Point CurrentCursorPosition => CurrentState.Position;

        public Point PreviousCursorPosition => PreviousState.Position;
#else
        public Point CurrentCursorPosition => new Point(CurrentState.X, CurrentState.Y);

        public Point PreviousCursorPosition => new Point(PreviousState.X, PreviousState.Y);
#endif

        public Point CursorPositionDelta => CurrentCursorPosition - PreviousCursorPosition;

        public bool ScrolledVertically => VerticalScrollDelta != 0;

        public bool ScrolledHorizontally => HorizontalScrollDelta != 0;

        public bool Scrolled => ScrolledVertically || ScrolledHorizontally;

        public bool CursorMoved => CursorPositionDelta != Point.Zero;

        public bool IsMouseInWindow => Manager.IsWindowActive && Helper.CoordinateInRectangle(CurrentCursorPosition, Manager.Window.ClientBounds);

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
                Microsoft.Xna.Framework.Input.Mouse.SetPosition(Manager.CenterCoordinates.X, Manager.CenterCoordinates.Y);
            }
        }

        public void ConsumeButton(MouseButton button)
        {
            ButtonLastConsumed[button] = Manager.CurrentFrame;
        }

        public void ConsumeSensor(MouseSensor sensor)
        {
            SensorLastConsumed[sensor] = Manager.CurrentFrame;
        }

        public bool IsButtonConsumed(MouseButton button)
        {
            if (ButtonLastConsumed.TryGetValue(button, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        public bool IsSensorConsumed(MouseSensor sensor)
        {
            if (SensorLastConsumed.TryGetValue(sensor, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        public bool IsPressed(MouseButton button)
        {
            return ButtonMapping[button](CurrentState) == ButtonState.Pressed;
        }

        public bool WasPressed(MouseButton button)
        {
            return ButtonMapping[button](PreviousState) == ButtonState.Pressed;
        }

        public bool IsReleased(MouseButton button)
        {
            return ButtonMapping[button](CurrentState) == ButtonState.Released;
        }

        public bool WasReleased(MouseButton button)
        {
            return ButtonMapping[button](PreviousState) == ButtonState.Released;
        }

        public bool CursorInBounds(Rectangle boundaries, bool scaleCoordinatesToArea = false, Rectangle? parentBoundaries = null)
        {
            return Helper.CoordinateInRectangle(
                scaleCoordinatesToArea
                    ? Helper.ScalePointToChild(CurrentCursorPosition, parentBoundaries ?? Manager.Window.ClientBounds, boundaries)
                    : CurrentCursorPosition,
                boundaries
            );
        }

    }

}
