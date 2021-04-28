using Velentr.Input.Keyboard;

namespace Velentr.Input.EventArguments
{

    public class KeyboardButtonEventArguments : ConditionEventArguments
    {

        public Key Key { get; internal set; }

        public int NumberOfKeysPressed { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (KeyboardButtonEventArguments) MemberwiseClone();
        }

    }

}
