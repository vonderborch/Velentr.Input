using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Xna.Framework;

namespace VilliInput
{
    public struct InputValueLogic
    {
        public InputValue ConditionalValue { get; private set; }

        public Comparison Comparator { get; private set; }

        public InputValueLogic(InputValue conditionalValue, Comparison comparator)
        {
            ConditionalValue = conditionalValue;
            Comparator = comparator;
        }
    }
}
