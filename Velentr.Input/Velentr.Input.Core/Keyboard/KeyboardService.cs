using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Velentr.Input.Enums;

namespace Velentr.Input.Keyboard
{

    public class KeyboardService : InputService
    {

        internal Dictionary<Key, Keys> KeyMapping = new Dictionary<Key, Keys>(Enum.GetNames(typeof(Key)).Length);

        public ulong CurrentKeysPressedLastConsumed = ulong.MinValue;

        public Dictionary<KeyboardLock, ulong> KeyboardLockLastConsumed = new Dictionary<KeyboardLock, ulong>(Enum.GetNames(typeof(KeyboardLock)).Length);

        public Dictionary<Key, ulong> KeyLastConsumed = new Dictionary<Key, ulong>(Enum.GetNames(typeof(Key)).Length);

        public ulong KeysPressedDeltaLastConsumed = ulong.MinValue;

        public KeyboardService(InputManager inputManager) : base(inputManager)
        {
            Source = InputSource.Keyboard;
        }

        public KeyboardState PreviousState { get; private set; }

        public KeyboardState CurrentState { get; private set; }

#if MONOGAME
#else
        private bool _currentCapsLock { get; set; }

        private bool _previousCapsLock { get; set; }

        private bool _currentNumLock { get; set; }

        private bool _previousNumLock { get; set; }
#endif

        public override void Setup()
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

        public void ConsumeKey(Key key)
        {
            KeyLastConsumed[key] = Manager.CurrentFrame;
        }

        public bool IsKeyConsumed(Key key)
        {
            if (KeyLastConsumed.TryGetValue(key, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        public void ConsumeLock(KeyboardLock lockType)
        {
            KeyboardLockLastConsumed[lockType] = Manager.CurrentFrame;
        }

        public bool IsLockConsumed(KeyboardLock lockType)
        {
            if (KeyboardLockLastConsumed.TryGetValue(lockType, out var frame))
            {
                return frame == Manager.CurrentFrame;
            }

            return false;
        }

        public void ConsumeCurrentKeysPressedCount()
        {
            CurrentKeysPressedLastConsumed = Manager.CurrentFrame;
        }

        public bool IsCurrentKeysPressedCountConsumed()
        {
            return CurrentKeysPressedLastConsumed == Manager.CurrentFrame;
        }

        public void ConsumeKeysPressedDeltaCount()
        {
            KeysPressedDeltaLastConsumed = Manager.CurrentFrame;
        }

        public bool IsKeysPressedDeltaConsumed()
        {
            return KeysPressedDeltaLastConsumed == Manager.CurrentFrame;
        }

        public bool IsKeyPressed(Key key)
        {
            return CurrentState.IsKeyDown(KeyMapping[key]);
        }

        public bool WasKeyPressed(Key key)
        {
            return PreviousState.IsKeyDown(KeyMapping[key]);
        }

        public bool IsKeyReleased(Key key)
        {
            return CurrentState.IsKeyUp(KeyMapping[key]);
        }

        public bool WasKeyReleased(Key key)
        {
            return PreviousState.IsKeyUp(KeyMapping[key]);
        }

        public bool IsCapsLockEnabled()
        {
#if MONOGAME
            return CurrentState.CapsLock;
#else
            return _currentCapsLock;
#endif
        }

        public bool WasCapsLockEnabled()
        {
#if MONOGAME
            return PreviousState.CapsLock;
#else
            return _previousCapsLock;
#endif
        }

        public bool IsNumLockEnabled()
        {
#if MONOGAME
            return CurrentState.NumLock;
#else
            return _currentNumLock;
#endif
        }

        public bool WasNumLockEnabled()
        {
#if MONOGAME
            return PreviousState.NumLock;
#else
            return _previousNumLock;
#endif
        }

        public int CurrentKeysPressed()
        {
#if MONOGAME
            return CurrentState.GetPressedKeyCount();
#else
            return CurrentState.GetPressedKeys().Length;
#endif
        }

        public int PreviousKeysPressed()
        {
#if MONOGAME
            return PreviousState.GetPressedKeyCount();
#else
            return PreviousState.GetPressedKeys().Length;
#endif
        }

        public int KeysPressedCountDelta()
        {
            return CurrentKeysPressed() - PreviousKeysPressed();
        }

        public List<Key> GetCurrentPressedKeys()
        {
            var rawKeys = CurrentState.GetPressedKeys();
            return rawKeys.Select(t => (Key) (int) t).ToList();
        }

        public List<Key> GetPreviousPressedKeys()
        {
            var rawKeys = PreviousState.GetPressedKeys();
            return rawKeys.Select(t => (Key) (int) t).ToList();
        }

    }

}
