using VilliInput.Keyboard;

namespace VilliInput.EventArguments
{

    public class KeyboardButtonEventArguments : VilliEventArguments
    {

        public Key Key { get; internal set; }

        public int NumberOfKeysPressed { get; internal set; }

        public override VilliEventArguments Clone()
        {
            return (KeyboardButtonEventArguments) MemberwiseClone();
        }

    }

}
