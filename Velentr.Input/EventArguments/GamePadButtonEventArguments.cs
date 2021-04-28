using Velentr.Input.GamePad;

namespace Velentr.Input.EventArguments
{

    public class GamePadButtonEventArguments : ConditionEventArguments
    {

        public GamePadButton Button { get; internal set; }

        public int PlayerIndex { get; internal set; }

        public GamePadInputMode InputMode { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (GamePadButtonEventArguments) MemberwiseClone();
        }

    }

}
