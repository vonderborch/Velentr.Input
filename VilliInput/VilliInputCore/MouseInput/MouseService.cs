using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace VilliInput.MouseInput
{

    public class MouseService : InputService
    {

        private static readonly List<MouseButton> buttons = new List<MouseButton>(Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>().ToList());

        protected static Dictionary<MouseButton, ulong> ButtonConsumed = new Dictionary<MouseButton, ulong>(Enum.GetNames(typeof(MouseButton)).Length);

        protected static Dictionary<MouseSensor, ulong> SensorConsumed = new Dictionary<MouseSensor, ulong>(Enum.GetNames(typeof(MouseSensor)).Length);

        public static MouseState PreviousState { get; private set; }

        public static MouseState CurrentState { get; private set; }

        public bool ResetMouseCoordsToCenterOfScreen { get; set; } = false;

        public MouseService()
        {
            Source = InputSource.Mouse;

        }

        public override void Setup()
        {
            PreviousState = Mouse.GetState();
            CurrentState = Mouse.GetState();
        }

        public override void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Mouse.GetState();

            if (ResetMouseCoordsToCenterOfScreen)
            {
                Mouse.SetPosition(Villi.CenterCoordinates.X, Villi.CenterCoordinates.Y);
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
