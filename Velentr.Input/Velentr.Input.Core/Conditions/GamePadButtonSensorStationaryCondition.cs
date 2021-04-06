using Velentr.Input.Conditions.Internal;
using Velentr.Input.GamePad;

namespace Velentr.Input.Conditions
{
    /// <summary>
    /// An input condition that is valid when a sensor on a GamePad has been stationary.
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.Internal.GamePadSensorBooleanCondition" />
    public class GamePadButtonSensorStationaryCondition : GamePadSensorBooleanCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamePadButtonSensorStationaryCondition"/> class.
        /// </summary>
        /// <param name="manager">The input manager the condition is associated with.</param>
        /// <param name="sensor">The sensor.</param>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="inputMode">The input mode.</param>
        /// <param name="sensorValueMode">The sensor value mode.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for timeout.</param>
        public GamePadButtonSensorStationaryCondition(InputManager manager, GamePadSensor sensor, int playerIndex = 0, GamePadInputMode inputMode = GamePadInputMode.SingleGamePad, GamePadSensorValueMode sensorValueMode = GamePadSensorValueMode.SingleGamePad, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0, uint milliSecondsForTimeOut = 0) : base(manager, sensor, playerIndex, inputMode, sensorValueMode, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet, milliSecondsForTimeOut) { }

        /// <summary>
        /// Internals the current.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
        protected override bool InternalCurrent(int playerIndex)
        {
            return !Manager.GamePad.SensorMoved(Sensor, playerIndex);
        }

        /// <summary>
        /// Internals the previous.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns></returns>
        protected override bool InternalPrevious(int playerIndex)
        {
            return true;
        }

    }

}
