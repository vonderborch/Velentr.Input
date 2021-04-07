using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Velentr.Input.Helpers;

namespace Velentr.Input.Touch
{

    /// <summary>
    /// The default XNA-based Touch Engine for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.Touch.TouchEngine" />
    public class XnaTouchEngine : TouchEngine
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaTouchEngine"/> class.
        /// </summary>
        public XnaTouchEngine() : base() { }

        /// <summary>
        /// Gets the capabilities.
        /// </summary>
        /// <value>
        /// The capabilities.
        /// </value>
        public TouchPanelCapabilities Capabilities { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public TouchCollection CurrentState { get; private set; }

        /// <summary>
        /// Sets up the InputEngine
        /// </summary>
        protected override void SetupInternal()
        {
            Capabilities = TouchPanel.GetCapabilities();
            Gestures = new Dictionary<GestureType, List<Gesture>>(Enum.GetNames(typeof(GestureType)).Length);
            if (Capabilities.IsConnected)
            {
                TouchPanelConnected = true;
                MaxTouchPoints = Capabilities.MaximumTouchCount;

                CurrentState = new TouchCollection();
                TouchPanel.EnabledGestures = Constants.Settings.EnabledTouchGestures;
            }
        }

        /// <summary>
        /// Updates the InputEngine.
        /// </summary>
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

        /// <summary>
        /// Consumes the gesture.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public override void ConsumeGesture(int id)
        {
            GestureLastConsumed[id] = Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is gesture consumed] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is gesture consumed] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsGestureConsumed(int id)
        {
            if (GestureLastConsumed.TryGetValue(id, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        /// <summary>
        /// Fetches the valid gestures.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns></returns>
        public override List<Gesture> FetchValidGestures(GestureType type, Rectangle boundaries, bool useRelativeCoordinates, Rectangle parentBoundaries, bool allowedIfConsumed, uint milliSecondsForConditionMet)
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
                        ? Helpers.Helper.ScalePointToChild(gesturePosition, parentBoundaries, boundaries)
                        : gesturePosition;
#endif

                    if (Helpers.Helper.CoordinateInRectangle(position, boundaries))
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
