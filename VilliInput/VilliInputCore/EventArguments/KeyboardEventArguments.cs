using VilliInput.OLD.KeyboardInput;

namespace VilliInput.EventArguments
{
    public class KeyboardEventArguments : OLD.EventArguments.VilliEventArguments
    {
        public Key Key { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }
    }
}
