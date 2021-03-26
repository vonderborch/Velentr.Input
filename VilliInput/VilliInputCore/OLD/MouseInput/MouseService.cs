using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace VilliInput.OLD.MouseInput
{

    public class MouseService : InputService
    {

        private static readonly List<MouseButton> buttons = new List<MouseButton>(Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>().ToList());

        public static Dictionary<MouseButton, ulong> ButtonConsumed = new Dictionary<MouseButton, ulong>(Enum.GetNames(typeof(MouseButton)).Length);

        public static Dictionary<MouseSensor, ulong> SensorConsumed = new Dictionary<MouseSensor, ulong>(Enum.GetNames(typeof(MouseSensor)).Length);

        public static MouseState PreviousState { get; private set; }

        public static MouseState CurrentState { get; private set; }

        public bool ResetMouseCoordsToCenterOfScreen { get; set; } = false;

        public MouseService()
        {
            Source = InputSource.Mouse;

        }

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
                Microsoft.Xna.Framework.Input.Mouse.SetPosition(Villi.CenterCoordinates.X, Villi.CenterCoordinates.Y);
            }
        }

        public void ConsumeButton(MouseButton button)
        {
            ButtonConsumed[button] = Villi.CurrentFrame;
        }

        public void ConsumeSensor(MouseSensor sensor)
        {
            SensorConsumed[sensor] = Villi.CurrentFrame;
        }

        public bool IsButtonConsumed(MouseButton button)
        {
            if (ButtonConsumed.TryGetValue(button, out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }

        public bool IsSensorConsumed(MouseSensor sensor)
        {
            if (SensorConsumed.TryGetValue(sensor, out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }


    }
}
