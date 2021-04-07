using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Velentr.Input.Touch
{

    /// <summary>
    /// Defines the base methods and properties that are needed for Touch Input support.
    /// </summary>
    /// <seealso cref="Velentr.Input.InputEngine" />
    public abstract class TouchEngine : InputEngine
    {

        /// <summary>
        /// The gesture last consumed
        /// </summary>
        protected Dictionary<int, ulong> GestureLastConsumed;

        /// <summary>
        /// The gestures
        /// </summary>
        protected Dictionary<GestureType, List<Gesture>> Gestures;

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchEngine"/> class.
        /// </summary>
        protected TouchEngine()
        {
            GestureLastConsumed = new Dictionary<int, ulong>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [touch panel connected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [touch panel connected]; otherwise, <c>false</c>.
        /// </value>
        public bool TouchPanelConnected { get; set; }

        /// <summary>
        /// Gets or sets the maximum touch points.
        /// </summary>
        /// <value>
        /// The maximum touch points.
        /// </value>
        public int MaxTouchPoints { get; set; }

        /// <summary>
        /// Consumes the gesture.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public abstract void ConsumeGesture(int id);

        /// <summary>
        /// Determines whether [is gesture consumed] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is gesture consumed] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsGestureConsumed(int id);

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
        public abstract List<Gesture> FetchValidGestures(GestureType type, Rectangle boundaries, bool useRelativeCoordinates, Rectangle parentBoundaries, bool allowedIfConsumed, uint milliSecondsForConditionMet);

    }

}
