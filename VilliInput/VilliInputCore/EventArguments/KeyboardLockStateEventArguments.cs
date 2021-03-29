using VilliInput.Keyboard;

namespace VilliInput.EventArguments
{

    public class KeyboardLockStateEventArguments : VilliEventArguments
    {

        public KeyboardLock LockType { get; internal set; }

        public int NumberOfKeysPressed { get; internal set; }

        public override VilliEventArguments Clone()
        {
            return (KeyboardLockStateEventArguments) MemberwiseClone();
        }

    }

}
