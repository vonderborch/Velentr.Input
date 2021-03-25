using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Xna.Framework;

namespace VilliInput
{
    public struct InputValue
    {
        public int? ValueInt { get; private set; }
        public uint? ValueUInt { get; private set; }
        public Point? ValuePoint { get; private set; }
        public Vector2? ValueVector2 { get; private set; }

        public InputValue(uint? valueUInt, int? valueInt, Point? valuePoint = null, Vector2? valueVector2 = null)
        {
            ValueUInt = valueUInt;
            ValueInt = valueInt;
            ValuePoint = valuePoint;
            ValueVector2 = valueVector2;
        }
    }
}
