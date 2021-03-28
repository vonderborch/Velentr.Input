using Microsoft.Xna.Framework.Input;

namespace VilliInput.GamePad
{
    public struct VilliGamePad
    {
        public int PlayerIndex { get; set; }

        public GamePadState PreviousState { get; set; }

        public GamePadState CurrentState { get; set; }

        public GamePadCapabilities Capabilities { get; set; }

        public GamePadDeadZone DeadZone { get; set; }

        public bool IsConnected => Capabilities.IsConnected;

    }
}
