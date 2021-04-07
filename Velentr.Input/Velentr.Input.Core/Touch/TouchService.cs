using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Input.Enums;

namespace Velentr.Input.Touch
{

    /// <summary>
    /// Defines what methods must be available at a minimum to support Touch inputs
    /// </summary>
    /// <seealso cref="Velentr.Input.InputService" />
    public abstract class TouchService : InputService
    {

        /// <summary>
        /// Gets or sets a value indicating whether [touch panel connected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [touch panel connected]; otherwise, <c>false</c>.
        /// </value>
        public bool TouchPanelConnected { get; protected set; }

        /// <summary>
        /// Gets or sets the maximum touch points.
        /// </summary>
        /// <value>
        /// The maximum touch points.
        /// </value>
        public int MaxTouchPoints { get; protected set; }

        /// <summary>
        /// Gets or sets the engine.
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        public TouchEngine Engine { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchService"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        protected TouchService(InputManager manager) : base(manager)
        {
            Source = InputSource.Touch;
        }

        /// <summary>
        /// Consumes the gesture.
        /// </summary>
        /// <param name="id">The id to consume.</param>
        public abstract void ConsumeGesture(int id);

        /// <summary>
        /// Consumes the gestures.
        /// </summary>
        /// <param name="ids">The ids to consume.</param>
        public abstract void ConsumeGesture(List<int> ids);

        /// <summary>
        /// Determines whether [is gesture consumed] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is gesture consumed] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsGestureConsumed(int id);

        /// <summary>
        /// Fetches valid gestures for the GestureType and Boundaries, and other parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns>The list of gestures that meet the parameters.</returns>
        public abstract List<Gesture> FetchValidGestures(GestureType type, Rectangle boundaries, bool useRelativeCoordinates, Rectangle parentBoundaries, bool allowedIfConsumed, uint milliSecondsForConditionMet);

    }

}
