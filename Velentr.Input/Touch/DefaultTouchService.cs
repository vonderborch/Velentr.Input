using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Velentr.Input.Touch
{

    /// <summary>
    /// The default Touch Service implementation for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.Touch.TouchService" />
    public class DefaultTouchService : TouchService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTouchService"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public DefaultTouchService(InputManager manager) : base(manager)
        {
        }

        /// <summary>
        /// Sets up the input service.
        /// </summary>
        /// <param name="engine">The engine to setup the input service with.</param>
        protected override void SetupInternal(InputEngine engine)
        {
            Engine = (TouchEngine)engine;
            TouchPanelConnected = Engine.TouchPanelConnected;
            MaxTouchPoints = Engine.MaxTouchPoints;
        }

        /// <summary>
        /// Updates the input service.
        /// </summary>
        public override void Update()
        {
            Engine?.Update();
        }

        /// <summary>
        /// Consumes the gesture.
        /// </summary>
        /// <param name="id">The id to consume.</param>
        public override void ConsumeGesture(int id)
        {
            Engine.ConsumeGesture(id);
        }

        /// <summary>
        /// Consumes the gestures.
        /// </summary>
        /// <param name="ids">The ids to consume.</param>
        public override void ConsumeGesture(List<int> ids)
        {
            for (var i = 0; i < ids.Count; i++)
            {
                Engine.ConsumeGesture(ids[i]);
            }
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
            return Engine.IsGestureConsumed(id);
        }

        /// <summary>
        /// Fetches valid gestures for the GestureType and Boundaries, and other parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="boundaries">The boundaries.</param>
        /// <param name="useRelativeCoordinates">if set to <c>true</c> [use relative coordinates].</param>
        /// <param name="parentBoundaries">The parent boundaries.</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns>
        /// The list of gestures that meet the parameters.
        /// </returns>
        public override List<Gesture> FetchValidGestures(GestureType type, Rectangle boundaries, bool useRelativeCoordinates, Rectangle parentBoundaries, bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return Engine.FetchValidGestures(type, boundaries, useRelativeCoordinates, parentBoundaries, allowedIfConsumed, milliSecondsForConditionMet);
        }

    }

}
