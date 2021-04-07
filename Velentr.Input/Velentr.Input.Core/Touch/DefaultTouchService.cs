using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;

namespace Velentr.Input.Touch
{

    public class DefaultTouchService : TouchService
    {

        public bool TouchPanelConnected { get; private set; }

        public int MaxTouchPoints { get; private set; }

        public TouchEngine Engine { get; private set; }

        public DefaultTouchService(InputManager manager) : base(manager)
        {
        }

        public override void Setup()
        {
            Engine = new XnaEngine(Manager);
            Engine.Setup();
            TouchPanelConnected = Engine.TouchPanelConnected;
            MaxTouchPoints = Engine.MaxTouchPoints;
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
