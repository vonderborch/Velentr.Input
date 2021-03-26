using Microsoft.Xna.Framework;

namespace VilliInput.OLD
{
    public struct InputValue
    {
        public int? ValueInt { get; private set; }
        public uint? ValueUInt { get; private set; }
        public float? ValueFloat { get; private set; }
        public Point? ValuePoint { get; private set; }
        public Vector2? ValueVector2 { get; private set; }

        public InputValue(uint? valueUInt = null, int? valueInt = null, Point? valuePoint = null, Vector2? valueVector2 = null, float? valueFloat = null)
        {
            ValueUInt = valueUInt;
            ValueInt = valueInt;
            ValueFloat = valueFloat;
            ValuePoint = valuePoint;
            ValueVector2 = valueVector2;
        }
    }
}
