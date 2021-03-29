using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input.Touch;

namespace VilliInput.Touch.Engines
{

    public class XnaDerivedEngine : TouchEngine
    {

        public XnaDerivedEngine() : base(TouchEngines.XNA_derived) { }

        public TouchPanelCapabilities Capabilities { get; private set; }

        public TouchCollection CurrentState { get; private set; }

        public override void Setup()
        {
            Capabilities = TouchPanel.GetCapabilities();
            Gestures = new Dictionary<GestureType, List<Gesture>>(Enum.GetNames(typeof(GestureType)).Length);
            if (Capabilities.IsConnected)
            {
                TouchPanelConnected = true;
                MaxTouchPoints = Capabilities.MaximumTouchCount;

                CurrentState = new TouchCollection();
                TouchPanel.EnabledGestures = Villi.System.Settings.EnabledTouchGestures;
            }
        }

        public override void Update()
        {
            if (TouchPanelConnected)
            {
                CurrentState = TouchPanel.GetState();

                // Update gestures...
                if (Gestures.Count == 0)
                {
                    Gestures = new Dictionary<GestureType, List<Gesture>>(Enum.GetNames(typeof(GestureType)).Length);
                }
                else
                {
                    foreach (var gesture in Gestures)
                    {
                        gesture.Value.Clear();
                    }
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

}
