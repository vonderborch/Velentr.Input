using System.Collections.Generic;
using Velentr.Input.Enums;

namespace Velentr.Input.Keyboard
{

    /// <summary>
    /// Defines what methods must be available at a minimum to support Keyboard inputs
    /// </summary>
    /// <seealso cref="Velentr.Input.InputService" />
    public abstract class KeyboardService : InputService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        protected KeyboardService(InputManager inputManager) : base(inputManager)
        {
            Source = InputSource.Keyboard;
        }

        /// <summary>
        /// Gets or sets the engine.
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        protected KeyboardEngine Engine { get; set; }

        /// <summary>
        /// Gets a value indicating whether [current caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current caps lock]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CurrentCapsLock { get; }

        /// <summary>
        /// Gets a value indicating whether [previous caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous caps lock]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool PreviousCapsLock { get; }

        /// <summary>
        /// Gets a value indicating whether [current number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current number lock]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool CurrentNumLock { get; }

        /// <summary>
        /// Gets a value indicating whether [previous number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous number lock]; otherwise, <c>false</c>.
        /// </value>
        public abstract bool PreviousNumLock { get; }

        /// <summary>
        /// Consumes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        public abstract void ConsumeKey(Key key);

        /// <summary>
        /// Determines whether [is key consumed] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [is key consumed] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsKeyConsumed(Key key);

        /// <summary>
        /// Consumes the lock.
        /// </summary>
        /// <param name="lockType">Type of the lock.</param>
        public abstract void ConsumeLock(KeyboardLock lockType);

        /// <summary>
        /// Determines whether [is lock consumed] [the specified lock type].
        /// </summary>
        /// <param name="lockType">Type of the lock.</param>
        /// <returns>
        ///   <c>true</c> if [is lock consumed] [the specified lock type]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsLockConsumed(KeyboardLock lockType);

        /// <summary>
        /// Consumes the current keys pressed count.
        /// </summary>
        public abstract void ConsumeCurrentKeysPressedCount();

        /// <summary>
        /// Determines whether [is current keys pressed count consumed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is current keys pressed count consumed]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsCurrentKeysPressedCountConsumed();

        /// <summary>
        /// Consumes the keys pressed delta count.
        /// </summary>
        public abstract void ConsumeKeysPressedDeltaCount();

        /// <summary>
        /// Determines whether [is keys pressed delta consumed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is keys pressed delta consumed]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsKeysPressedDeltaConsumed();

        /// <summary>
        /// Determines whether [is key pressed] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [is key pressed] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsKeyPressed(Key key);

        /// <summary>
        /// Determines whether [was key pressed] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [was key pressed] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasKeyPressed(Key key);

        /// <summary>
        /// Determines whether [is key released] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [is key released] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsKeyReleased(Key key);

        /// <summary>
        /// Determines whether [was key released] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [was key released] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasKeyReleased(Key key);

        /// <summary>
        /// Determines whether [is caps lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is caps lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsCapsLockEnabled();

        /// <summary>
        /// Determines whether [was caps lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [was caps lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasCapsLockEnabled();

        /// <summary>
        /// Determines whether [is number lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is number lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsNumLockEnabled();

        /// <summary>
        /// Determines whether [was number lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [was number lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool WasNumLockEnabled();

        /// <summary>
        /// Determines the number of keys currently pressed.
        /// </summary>
        /// <returns>the number of keys pressed.</returns>
        public abstract int CurrentKeysPressed();

        /// <summary>
        /// Determines the number of keys previously pressed.
        /// </summary>
        /// <returns>the number of keys pressed.</returns>
        public abstract int PreviousKeysPressed();

        /// <summary>
        /// Determines the delta between the amount of keys pressed.
        /// </summary>
        /// <returns>the delta between the amount of keys pressed.</returns>
        public abstract int KeysPressedCountDelta();

        /// <summary>
        /// Gets the current pressed keys.
        /// </summary>
        /// <returns>A list of keys currently pressed.</returns>
        public abstract List<Key> GetCurrentPressedKeys();

        /// <summary>
        /// Gets the previous pressed keys.
        /// </summary>
        /// <returns>A list of keys previously pressed.</returns>
        public abstract List<Key> GetPreviousPressedKeys();

    }

}
