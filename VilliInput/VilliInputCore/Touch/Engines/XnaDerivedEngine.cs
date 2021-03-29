using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;

namespace VilliInput.Touch.Engines
{
    public class XnaDerivedEngine : TouchEngine
    {

        public XnaDerivedEngine() : base(TouchEngines.XNA_derived) { }

        public TouchPanelCapabilities Capabilities { get; private set; }

        public TouchCollection CurrentState { get; private set; }

        public TouchEngines EnabledTouchEngine { get; private set; }

        public override void Setup()
        {
            Capabilities = TouchPanel.GetCapabilities();
            if (Capabilities.IsConnected)
            {
                TouchPanelConnected = true;
                MaxTouchPoints = Capabilities.MaximumTouchCount;

                CurrentState = new TouchCollection();
                Gestures = new Dictionary<GestureType, List<Gesture>>(Enum.GetNames(typeof(GestureType)).Length);

                TouchPanel.EnabledGestures = Villi.System.Settings.EnabledTouchGestures;
            }
        }

        public override void Update()
        {
            CurrentState = TouchPanel.GetState();

            // Update gestures...
            foreach (var gesture in Gestures)
            {
                gesture.Value.Clear();
            }

            var id = 0;
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                var type = TouchGestureMapping.XnaToInternal[gesture.GestureType];

                if (!Gestures.ContainsKey(type))
                {
                    Gestures.Add(type, new List<Gesture>());
                }

                Gestures[type].Add(new Gesture(id++, gesture.Position, gesture.Position2, gesture.Delta, gesture.Delta2, type, gesture.Timestamp));
            }
        }

    }
}
