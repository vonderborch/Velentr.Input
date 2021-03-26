using VilliInput.OLD.GamePadInput;

namespace VilliInput.OLD.EventArguments
{
    public class GamePadButtonEventArguments : VilliEventArguments
    {
        public GamePadButton Button { get; internal set; }

        public int PlayerIndex { get; internal set; }
        
        public GamePadInputMode InputMode { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }
    }
}
