﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions;
using VilliInput.MouseInput;

namespace VilliInput.EventArguments
{
    public class AllConditionEventArguments : VilliEventArguments
    {

        public InputCondition[] Conditions { get; internal set; }

        public bool OrderMatters { get; internal set; }

        public List<VilliEventArguments> ConditionEventArguments { get; internal set; }
    }
}
