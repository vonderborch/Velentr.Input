using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.Touch.Engines;

namespace Velentr.Input.Touch
{

    public class TouchService : InputService
    {

        public bool TouchPanelConnected { get; private set; }

        public int MaxTouchPoints { get; private set; }

        public TouchEngine Engine { get; private set; }

        public TouchEngines EnabledTouchEngine { get; private set; }

        public TouchService(InputManager manager) : base(manager)
        {
            Source = InputSource.Touch;
        }

        public override void Setup()
        {
            switch (VelentrInput.System.Settings.TouchEngine)
            {
                case TouchEngines.Win7:
                    Engine = new Win7Engine();
                    break;
                case TouchEngines.XNA_derived:
                    Engine = new XnaDerivedEngine();
                    break;
            }

            Engine.Setup();
            TouchPanelConnected = Engine.TouchPanelConnected;
            MaxTouchPoints = Engine.MaxTouchPoints;
            EnabledTouchEngine = Engine.Engine;
        }

        public override void Update()
        {
            Engine.Update();
        }

        public void ConsumeGesture(int id)
        {
            Engine.ConsumeGesture(id);
        }

        public void ConsumeGesture(List<int> ids)
        {
            for (var i = 0; i < ids.Count; i++)
            {
                Engine.ConsumeGesture(ids[i]);
            }
        }

        public bool IsGestureConsumed(int id)
        {
            return Engine.IsGestureConsumed(id);
        }

        public List<Gesture> FetchValidGestures(GestureType type, Rectangle boundaries, bool useRelativeCoordinates, Rectangle parentBoundaries, bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return Engine.FetchValidGestures(type, boundaries, useRelativeCoordinates, parentBoundaries, allowedIfConsumed, milliSecondsForConditionMet);
        }

    }

}
