using Velentr.Input.Keyboard;

namespace Velentr.Input.EventArguments
{

    public class KeyboardLockStateEventArguments : ConditionEventArguments
    {

        public KeyboardLock LockType { get; internal set; }

        public int NumberOfKeysPressed { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (KeyboardLockStateEventArguments) MemberwiseClone();
        }

    }

}
