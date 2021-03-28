using VilliInput.Keyboard;
using VilliInput.KeyboardInput;

namespace VilliInput.EventArguments
{
    public class KeyboardLockStateEventArguments : VilliEventArguments
    {
        public KeyboardLock LockType { get; internal set; }

        public int NumberOfKeysPressed { get; internal set; }
    }
}
