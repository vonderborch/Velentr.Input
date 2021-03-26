using VilliInput.OLD.GamePadInput;

namespace VilliInput.EventArguments
{
    public class GamePadButtonEventArguments : OLD.EventArguments.VilliEventArguments
    {
        public GamePadButton Button { get; internal set; }

        public int PlayerIndex { get; internal set; }
        
        public GamePadInputMode InputMode { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }
    }
}
