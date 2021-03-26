using VilliInput.OLD.KeyboardInput;

namespace VilliInput.OLD.EventArguments
{
    public class KeyboardEventArguments : VilliEventArguments
    {
        public Key Key { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }
    }
}
