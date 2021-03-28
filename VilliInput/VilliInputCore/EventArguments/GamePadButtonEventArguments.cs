using VilliInput.GamePad;

namespace VilliInput.EventArguments
{
    public class GamePadButtonEventArguments : VilliEventArguments
    {
        public GamePadButton Button { get; internal set; }

        public int PlayerIndex { get; internal set; }

        public GamePadInputMode InputMode { get; internal set; }
    }
}
