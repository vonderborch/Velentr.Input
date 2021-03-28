using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace VilliInput.OLD.KeyboardInput
{

    public class KeyboardService : InputService
    {
        public static Dictionary<Key, ulong> KeyConsumed = new Dictionary<Key, ulong>(Enum.GetNames(typeof(Key)).Length);

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
                KeyboardHelpers.KeyMapping[(Key) key] = (Keys) key;
            }
        }

        public override void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        public void ConsumeKey(Key key)
        {
            KeyConsumed[key] = Villi.CurrentFrame;
        }

        public bool IsKeyConsumed(Key key)
        {
            if (KeyConsumed.TryGetValue(key, out var frame))
            {
                return frame == Villi.CurrentFrame;
            }

            return false;
        }
    }
}
