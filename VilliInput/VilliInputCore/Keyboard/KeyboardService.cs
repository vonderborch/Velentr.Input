using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using VilliInput.Enums;
using VilliInput.KeyboardInput;

namespace VilliInput.Keyboard
{
    public class KeyboardService : InputService
    {
        internal static Dictionary<Key, Keys> KeyMapping = new Dictionary<Key, Keys>(Enum.GetNames(typeof(Key)).Length);

        public Dictionary<Key, ulong> KeyLastConsumed = new Dictionary<Key, ulong>(Enum.GetNames(typeof(Key)).Length);

        public Dictionary<KeyboardLock, ulong> KeyboardLockLastConsumed = new Dictionary<KeyboardLock, ulong>(Enum.GetNames(typeof(KeyboardLock)).Length);

        public ulong CurrentKeysPressedLastConsumed = ulong.MinValue;

        public ulong KeysPressedDeltaLastConsumed = ulong.MinValue;

        public static KeyboardState PreviousState { get; private set; }
        public static KeyboardState CurrentState { get; private set; }

        public KeyboardService()
        {
            Source = InputSource.Keyboard;
        }

        public override void Setup()
        {
            PreviousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            // Update the mapping to match XNA's (right now we've got parity. In the future, this might need to be changed to better handle other keyboards, etc.)
            foreach (var key in Enum.GetValues(typeof(Key)))
            {
                KeyMapping[(Key)key] = (Keys)(int)key;
            }
        }

        public override void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        public void ConsumeKey(Key key)
        {
            KeyLastConsumed[key] = Villi.CurrentFrame;
        }

        public bool IsKeyConsumed(Key key)
        {
            if (KeyLastConsumed.TryGetValue(key, out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }

        public void ConsumeLock(KeyboardLock lockType)
        {
            KeyboardLockLastConsumed[lockType] = Villi.CurrentFrame;
        }

        public bool IsLockConsumed(KeyboardLock lockType)
        {
            if (KeyboardLockLastConsumed.TryGetValue(lockType, out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }

        public void ConsumeCurrentKeysPressedCount()
        {
            CurrentKeysPressedLastConsumed = Villi.CurrentFrame;
        }

        public bool IsCurrentKeysPressedCountConsumed()
        {
            return CurrentKeysPressedLastConsumed == Villi.CurrentFrame;
        }

        public void ConsumeKeysPressedDeltaCount()
        {
            KeysPressedDeltaLastConsumed = Villi.CurrentFrame;
        }

        public bool IsKeysPressedDeltaConsumed()
        {
            return KeysPressedDeltaLastConsumed == Villi.CurrentFrame;
        }

        public static bool IsKeyPressed(Key key)
        {
            return CurrentState.IsKeyDown(KeyMapping[key]);
        }

        public static bool WasKeyPressed(Key key)
        {
            return PreviousState.IsKeyDown(KeyMapping[key]);
        }

        public static bool IsKeyReleased(Key key)
        {
            return CurrentState.IsKeyUp(KeyMapping[key]);
        }

        public static bool WasKeyReleased(Key key)
        {
            return PreviousState.IsKeyUp(KeyMapping[key]);
        }

        public static bool IsCapsLockEnabled()
        {
            return CurrentState.CapsLock;
        }

        public static bool WasCapsLockEnabled()
        {
            return PreviousState.CapsLock;
        }

        public static bool IsNumLockEnabled()
        {
            return CurrentState.NumLock;
        }

        public static bool WasNumLockEnabled()
        {
            return PreviousState.NumLock;
        }

        public static int CurrentKeysPressed()
        {
            return CurrentState.GetPressedKeyCount();
        }

        public static int PreviousKeysPressed()
        {
            return PreviousState.GetPressedKeyCount();
        }

        public static int KeysPressedCountDelta()
        {
            return CurrentKeysPressed() - PreviousKeysPressed();
        }

        public static List<Key> GetCurrentPressedKeys()
        {
            var rawKeys = CurrentState.GetPressedKeys();
            return rawKeys.Select(t => (Key)((int)t)).ToList();
        }

        public static List<Key> GetPreviousPressedKeys()
        {
            var rawKeys = PreviousState.GetPressedKeys();
            return rawKeys.Select(t => (Key)((int)t)).ToList();
        }
    }
}
