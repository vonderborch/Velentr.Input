using VilliInput.KeyboardInput;

namespace VilliInput.EventArguments
{
    public class KeyboardButtonEventArguments : VilliEventArguments
    {
        public Key Key { get; internal set; }

        public int NumberOfKeysPressed { get; internal set; }
    }
}
