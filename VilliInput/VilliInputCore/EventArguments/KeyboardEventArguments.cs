using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.KeyboardInput;
using VilliInput.MouseInput;

namespace VilliInput.EventArguments
{
    public class KeyboardEventArguments : VilliEventArguments
    {
        public Key Key { get; internal set; }

        public uint SecondsForPressed { get; internal set; }

        public uint SecondsForReleased { get; internal set; }
    }
}
