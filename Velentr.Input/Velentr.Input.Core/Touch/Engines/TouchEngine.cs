using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Helpers;

namespace Velentr.Input.Touch.Engines
{

    public abstract class TouchEngine
    {

        protected Dictionary<int, ulong> GestureLastConsumed;

        protected Dictionary<GestureType, List<Gesture>> Gestures;

        protected TouchEngine(TouchEngines engine)
        {
            Engine = engine;
            GestureLastConsumed = new Dictionary<int, ulong>();
        }

        public TouchEngines Engine { get; }

        public bool TouchPanelConnected { get; protected set; }

        public int MaxTouchPoints { get; protected set; }

        public abstract void Setup();

        public abstract void Update();

        public void ConsumeGesture(int id)
        {
            GestureLastConsumed[id] = VelentrInput.CurrentFrame;
        }

        public bool IsGestureConsumed(int id)
        {
            if (GestureLastConsumed.TryGetValue(id, out var frame))
            {
                return frame == VelentrInput.CurrentFrame;
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
#if MONOGAME
                    var position = useRelativeCoordinates
                        ? Helper.ScalePointToChild(potentialGestures[i].Position.ToPoint(), parentBoundaries, boundaries)
                        : potentialGestures[i].Position.ToPoint();
#else
                    var gesturePosition = new Point((int)potentialGestures[i].Position.X, (int)potentialGestures[i].Position.Y);

                    var position = useRelativeCoordinates
                        ? Helper.ScalePointToChild(gesturePosition, parentBoundaries, boundaries)
                        : gesturePosition;
#endif

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
