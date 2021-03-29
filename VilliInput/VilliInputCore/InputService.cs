﻿using VilliInput.Enums;

namespace VilliInput
{

    public abstract class InputService
    {

        public InputSource Source { get; protected set; }

        public abstract void Setup();

        public abstract void Update();

    }

}
