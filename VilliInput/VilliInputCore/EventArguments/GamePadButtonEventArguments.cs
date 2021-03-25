using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.GamePadInput;

namespace VilliInput.EventArguments
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
