using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using VilliInput.KeyboardInput;

namespace VilliInput.MouseInput
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
            PreviousState = Keyboard.GetState();
            CurrentState = Keyboard.GetState();

            // Update the mapping to match XNA's (right now we've got parity. In the future, this might need to be changed to better handle other keyboards, etc.)
            foreach (var key in Enum.GetValues(typeof(Key)))
            {
                KeyboardHelpers.KeyMapping[(Key) key] = (Keys) key;
            }
        }

        public override void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
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
