using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VilliInput.Helpers;

namespace VilliInput.Touch.Engines
{
    public abstract class TouchEngine
    {
        protected Dictionary<GestureType, List<Gesture>> Gestures;

        protected Dictionary<int, ulong> GestureLastConsumed;

        public TouchEngines Engine { get; private set; }

        public bool TouchPanelConnected { get; protected set; }

        public int MaxTouchPoints { get; protected set; }

        protected TouchEngine(TouchEngines engine)
        {
            Engine = engine;
            GestureLastConsumed = new Dictionary<int, ulong>();
        }

        public abstract void Setup();

        public abstract void Update();

        public void ConsumeGesture(int id)
        {
            GestureLastConsumed[id] = Villi.CurrentFrame;
        }

        public bool IsGestureConsumed(int id)
        {
            if (GestureLastConsumed.TryGetValue(id, out var frame))
            {
                return frame == Villi.CurrentFrame;
            }
            return false;
        }

        public List<Gesture> FetchValidGestures(GestureType type, Rectangle boundaries, bool useRelativeCoordinates, Rectangle parentBoundaries, bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            var validGestures = new List<Gesture>();
            if (Gestures.TryGetValue(type, out var potentialGestures))
            {
                for (var i = 0; i < potentialGestures.Count; i++)
                {
                    var position = useRelativeCoordinates
                        ? Helper.ScalePointToChild(potentialGestures[i].Position.ToPoint(), parentBoundaries, boundaries)
                        : potentialGestures[i].Position.ToPoint();

                    if (Helper.CoordinateInRectangle(position, boundaries))
                    {
                        if ((allowedIfConsumed || !IsGestureConsumed(potentialGestures[i].Id)) && potentialGestures[i].TimeStamp.TotalMilliseconds >= milliSecondsForConditionMet)
                        {
                            validGestures.Add(potentialGestures[i]);
                        }
                    }
                }
            }

            return validGestures;

        }
    }
}
