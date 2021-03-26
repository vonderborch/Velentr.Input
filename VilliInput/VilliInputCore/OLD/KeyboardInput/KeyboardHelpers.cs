using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace VilliInput.OLD.KeyboardInput
{
    public static class KeyboardHelpers
    {
        internal static Dictionary<Key, Keys> KeyMapping = new Dictionary<Key, Keys>(Enum.GetNames(typeof(Key)).Length);

        public static bool IsKeyPressed(Key key)
        {
            return KeyboardService.CurrentState.IsKeyDown(KeyMapping[key]);
        }

        public static bool WasKeyPressed(Key key)
        {
            return KeyboardService.PreviousState.IsKeyDown(KeyMapping[key]);
        }

        public static bool IsKeyReleased(Key key)
        {
            return KeyboardService.CurrentState.IsKeyUp(KeyMapping[key]);
        }

        public static bool WasKeyReleased(Key key)
        {
            return KeyboardService.PreviousState.IsKeyUp(KeyMapping[key]);
        }

        public static bool IsCapsLockEnabled()
        {
            return KeyboardService.CurrentState.CapsLock;
        }

        public static bool WasCapsLockEnabled()
        {
            return KeyboardService.PreviousState.CapsLock;
        }

        public static bool IsNumLockEnabled()
        {
            return KeyboardService.CurrentState.NumLock;
        }

        public static bool WasNumLockEnabled()
        {
            return KeyboardService.PreviousState.NumLock;
        }

        public static int CurrentKeysPressed()
        {
            return KeyboardService.CurrentState.GetPressedKeyCount();
        }

        public static int PreviousKeysPressed()
        {
            return KeyboardService.PreviousState.GetPressedKeyCount();
        }

        public static List<Key> GetCurrentPressedKeys()
        {
            var rawKeys = KeyboardService.CurrentState.GetPressedKeys();
            return rawKeys.Select(t => (Key) ((int) t)).ToList();
        }

        public static List<Key> GetPreviousPressedKeys()
        {
            var rawKeys = KeyboardService.PreviousState.GetPressedKeys();
            return rawKeys.Select(t => (Key)((int)t)).ToList();
        }
    }
}
