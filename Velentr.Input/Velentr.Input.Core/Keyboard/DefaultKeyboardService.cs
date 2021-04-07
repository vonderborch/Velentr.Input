using System;
using System.Collections.Generic;

namespace Velentr.Input.Keyboard
{

    /// <summary>
    /// The default Keyboard service for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.Keyboard.KeyboardService" />
    public class DefaultKeyboardService : KeyboardService
    {

        /// <summary>
        /// The current keys pressed last consumed
        /// </summary>
        public ulong CurrentKeysPressedLastConsumed = ulong.MinValue;

        /// <summary>
        /// The keyboard lock last consumed
        /// </summary>
        public Dictionary<KeyboardLock, ulong> KeyboardLockLastConsumed = new Dictionary<KeyboardLock, ulong>(Enum.GetNames(typeof(KeyboardLock)).Length);

        /// <summary>
        /// The key last consumed
        /// </summary>
        public Dictionary<Key, ulong> KeyLastConsumed = new Dictionary<Key, ulong>(Enum.GetNames(typeof(Key)).Length);

        /// <summary>
        /// The keys pressed delta last consumed
        /// </summary>
        public ulong KeysPressedDeltaLastConsumed = ulong.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultKeyboardService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        public DefaultKeyboardService(InputManager inputManager) : base(inputManager)
        {
        }

        /// <summary>
        /// Gets a value indicating whether [current caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current caps lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool CurrentCapsLock => Engine.CurrentCapsLock;

        /// <summary>
        /// Gets a value indicating whether [previous caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous caps lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool PreviousCapsLock => Engine.PreviousCapsLock;

        /// <summary>
        /// Gets a value indicating whether [current number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current number lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool CurrentNumLock => Engine.CurrentNumLock;

        /// <summary>
        /// Gets a value indicating whether [previous number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous number lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool PreviousNumLock => Engine.PreviousNumLock;

        /// <summary>
        /// Sets up the input service.
        /// </summary>
        /// <param name="engine">The engine to setup the input service with.</param>
        protected override void SetupInternal(InputEngine engine)
        {
            Engine = (KeyboardEngine)engine;
        }

        /// <summary>
        /// Updates the input service.
        /// </summary>
        public override void Update()
        {
            Engine?.Update();
        }

        /// <summary>
        /// Consumes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        public override void ConsumeKey(Key key)
        {
            KeyLastConsumed[key] = Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is key consumed] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [is key consumed] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsKeyConsumed(Key key)
        {
            if (KeyLastConsumed.TryGetValue(key, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        /// <summary>
        /// Consumes the lock.
        /// </summary>
        /// <param name="lockType">Type of the lock.</param>
        public override void ConsumeLock(KeyboardLock lockType)
        {
            KeyboardLockLastConsumed[lockType] = Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is lock consumed] [the specified lock type].
        /// </summary>
        /// <param name="lockType">Type of the lock.</param>
        /// <returns>
        ///   <c>true</c> if [is lock consumed] [the specified lock type]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsLockConsumed(KeyboardLock lockType)
        {
            if (KeyboardLockLastConsumed.TryGetValue(lockType, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        /// <summary>
        /// Consumes the current keys pressed count.
        /// </summary>
        public override void ConsumeCurrentKeysPressedCount()
        {
            CurrentKeysPressedLastConsumed = Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is current keys pressed count consumed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is current keys pressed count consumed]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsCurrentKeysPressedCountConsumed()
        {
            return CurrentKeysPressedLastConsumed == Manager.CurrentFrame;
        }

        /// <summary>
        /// Consumes the keys pressed delta count.
        /// </summary>
        public override void ConsumeKeysPressedDeltaCount()
        {
            KeysPressedDeltaLastConsumed = Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is keys pressed delta consumed].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is keys pressed delta consumed]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsKeysPressedDeltaConsumed()
        {
            return KeysPressedDeltaLastConsumed == Manager.CurrentFrame;
        }

        /// <summary>
        /// Determines whether [is key pressed] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [is key pressed] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsKeyPressed(Key key)
        {
            return Engine.IsKeyPressed(key);
        }

        /// <summary>
        /// Determines whether [was key pressed] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [was key pressed] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasKeyPressed(Key key)
        {
            return Engine.WasKeyPressed(key);
        }

        /// <summary>
        /// Determines whether [is key released] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [is key released] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsKeyReleased(Key key)
        {
            return Engine.IsKeyReleased(key);
        }

        /// <summary>
        /// Determines whether [was key released] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [was key released] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasKeyReleased(Key key)
        {
            return Engine.WasKeyReleased(key);
        }

        /// <summary>
        /// Determines whether [is caps lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is caps lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsCapsLockEnabled()
        {
            return Engine.IsCapsLockEnabled();
        }

        /// <summary>
        /// Determines whether [was caps lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [was caps lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasCapsLockEnabled()
        {
            return Engine.WasCapsLockEnabled();
        }

        /// <summary>
        /// Determines whether [is number lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is number lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsNumLockEnabled()
        {
            return Engine.IsNumLockEnabled();
        }

        /// <summary>
        /// Determines whether [was number lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [was number lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasNumLockEnabled()
        {
            return Engine.WasNumLockEnabled();
        }

        /// <summary>
        /// Determines the number of keys currently pressed.
        /// </summary>
        /// <returns>
        /// the number of keys pressed.
        /// </returns>
        public override int CurrentKeysPressed()
        {
            return Engine.CurrentKeysPressed();
        }

        /// <summary>
        /// Determines the number of keys previously pressed.
        /// </summary>
        /// <returns>
        /// the number of keys pressed.
        /// </returns>
        public override int PreviousKeysPressed()
        {
            return Engine.PreviousKeysPressed();
        }

        /// <summary>
        /// Determines the delta between the amount of keys pressed.
        /// </summary>
        /// <returns>
        /// the delta between the amount of keys pressed.
        /// </returns>
        public override int KeysPressedCountDelta()
        {
            return Engine.KeysPressedCountDelta();
        }

        /// <summary>
        /// Gets the current pressed keys.
        /// </summary>
        /// <returns>
        /// A list of keys currently pressed.
        /// </returns>
        public override List<Key> GetCurrentPressedKeys()
        {
            return Engine.GetCurrentPressedKeys();
        }

        /// <summary>
        /// Gets the previous pressed keys.
        /// </summary>
        /// <returns>
        /// A list of keys previously pressed.
        /// </returns>
        public override List<Key> GetPreviousPressedKeys()
        {
            return Engine.GetPreviousPressedKeys();
        }

    }

}
