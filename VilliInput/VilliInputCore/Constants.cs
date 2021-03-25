using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace VilliInput
{
    public sealed class Constants
    {
        static Constants() { }
        private Constants() { }

        public static Constants Instance { get; } = new Constants();

        public GamePadDeadZone DefaultGamePadDeadZone { get; set; } = GamePadDeadZone.None;

        public int SecondsBetweenGamePadConnectionCheck { get; set; } = 15;

        public const string PlayerIndexExceptionMessage = "playerIndex must be 0 or greater, and less than the maximum supported gamepads on the system";

        public const string PlayerIndexExceptionMessageButtonState = "playerIndex must be less than the maximum supported gamepads on the system";

    }
}
