using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace Velentr.Input.Keyboard
{

    /// <summary>
    /// The default Keyboard engine for Velentr.Input
    /// </summary>
    /// <seealso cref="Velentr.Input.Keyboard.KeyboardEngine" />
    public class XnaKeyboardEngine : KeyboardEngine
    {

        /// <summary>
        /// The key mapping
        /// </summary>
        internal Dictionary<Key, Keys> KeyMapping = new Dictionary<Key, Keys>(Enum.GetNames(typeof(Key)).Length);

        /// <summary>
        /// The keys pressed delta last consumed
        /// </summary>
        public ulong KeysPressedDeltaLastConsumed = ulong.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="XnaKeyboardEngine"/> class.
        /// </summary>
        public XnaKeyboardEngine() : base()
        {

        }

#if MONOGAME
#else        
        /// <summary>
        /// Gets or sets a value indicating whether [current caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current caps lock]; otherwise, <c>false</c>.
        /// </value>
        private bool _currentCapsLock { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [previous caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous caps lock]; otherwise, <c>false</c>.
        /// </value>
        private bool _previousCapsLock { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [current number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current number lock]; otherwise, <c>false</c>.
        /// </value>
        private bool _currentNumLock { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [previous number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous number lock]; otherwise, <c>false</c>.
        /// </value>
        private bool _previousNumLock { get; set; }
#endif

        /// <summary>
        /// Gets the state of the previous.
        /// </summary>
        /// <value>
        /// The state of the previous.
        /// </value>
        public KeyboardState PreviousState { get; private set; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public KeyboardState CurrentState { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [current caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current caps lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool CurrentCapsLock {
            get
            {
#if MONOGAME
                return CurrentState.CapsLock;
#else
                return _currentCapsLock;
#endif
            }
        }

        /// <summary>
        /// Gets a value indicating whether [previous caps lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous caps lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool PreviousCapsLock
        {
            get
            {
#if MONOGAME
                return CurrentState.CapsLock;
#else
                return _previousCapsLock;
#endif
            }
        }

        /// <summary>
        /// Gets a value indicating whether [current number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [current number lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool CurrentNumLock
        {
            get
            {
#if MONOGAME
                return PreviousState.NumLock;
#else
                return _currentNumLock;
#endif
            }
        }

        /// <summary>
        /// Gets a value indicating whether [previous number lock].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [previous number lock]; otherwise, <c>false</c>.
        /// </value>
        public override bool PreviousNumLock
        {
            get
            {
#if MONOGAME
                return PreviousState.NumLock;
#else
                return _previousNumLock;
#endif
            }
        }

        /// <summary>
        /// Sets up the InputEngine
        /// </summary>
        protected override void SetupInternal()
        {
            PreviousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

#if MONOGAME
#else
            _previousCapsLock = Console.CapsLock;
            _currentCapsLock = Console.CapsLock;
            _previousNumLock = Console.NumberLock;
            _currentNumLock = Console.NumberLock;
#endif

            // Update the mapping to match XNA's (right now we've got parity. In the future, this might need to be changed to better handle other keyboards, etc.)
            foreach (var key in Enum.GetValues(typeof(Key)))
            {
                KeyMapping[(Key) key] = (Keys) (int) key;
            }
        }

        /// <summary>
        /// Updates the InputEngine.
        /// </summary>
        public override void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

#if MONOGAME
#else
            _previousCapsLock = _currentCapsLock;
            _currentCapsLock = Console.CapsLock;
            _previousNumLock = _currentNumLock;
            _currentNumLock = Console.NumberLock;
#endif
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
            return CurrentState.IsKeyDown(KeyMapping[key]);
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
            return PreviousState.IsKeyDown(KeyMapping[key]);
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
            return CurrentState.IsKeyUp(KeyMapping[key]);
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
            return PreviousState.IsKeyUp(KeyMapping[key]);
        }

        /// <summary>
        /// Determines whether [is caps lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is caps lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsCapsLockEnabled()
        {
#if MONOGAME
            return CurrentState.CapsLock;
#else
            return _currentCapsLock;
#endif
        }

        /// <summary>
        /// Determines whether [was caps lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [was caps lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasCapsLockEnabled()
        {
#if MONOGAME
            return PreviousState.CapsLock;
#else
            return _previousCapsLock;
#endif
        }

        /// <summary>
        /// Determines whether [is number lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is number lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsNumLockEnabled()
        {
#if MONOGAME
            return CurrentState.NumLock;
#else
            return _currentNumLock;
#endif
        }

        /// <summary>
        /// Determines whether [was number lock enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [was number lock enabled]; otherwise, <c>false</c>.
        /// </returns>
        public override bool WasNumLockEnabled()
        {
#if MONOGAME
            return PreviousState.NumLock;
#else
            return _previousNumLock;
#endif
        }

        /// <summary>
        /// Determines the number of keys currently pressed.
        /// </summary>
        /// <returns>
        /// the number of keys pressed.
        /// </returns>
        public override int CurrentKeysPressed()
        {
#if MONOGAME
            return CurrentState.GetPressedKeyCount();
#else
            return CurrentState.GetPressedKeys().Length;
#endif
        }

        /// <summary>
        /// Determines the number of keys previously pressed.
        /// </summary>
        /// <returns>
        /// the number of keys pressed.
        /// </returns>
        public override int PreviousKeysPressed()
        {
#if MONOGAME
            return PreviousState.GetPressedKeyCount();
#else
            return PreviousState.GetPressedKeys().Length;
#endif
        }

        /// <summary>
        /// Determines the delta between the amount of keys pressed.
        /// </summary>
        /// <returns>
        /// the delta between the amount of keys pressed.
        /// </returns>
        public override int KeysPressedCountDelta()
        {
            return CurrentKeysPressed() - PreviousKeysPressed();
        }

        /// <summary>
        /// Gets the current pressed keys.
        /// </summary>
        /// <returns>
        /// A list of keys currently pressed.
        /// </returns>
        public override List<Key> GetCurrentPressedKeys()
        {
            var rawKeys = CurrentState.GetPressedKeys();
            return rawKeys.Select(t => (Key) (int) t).ToList();
        }

        /// <summary>
        /// Gets the previous pressed keys.
        /// </summary>
        /// <returns>
        /// A list of keys previously pressed.
        /// </returns>
        public override List<Key> GetPreviousPressedKeys()
        {
            var rawKeys = PreviousState.GetPressedKeys();
            return rawKeys.Select(t => (Key) (int) t).ToList();
        }

    }

}
