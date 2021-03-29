using System;
using Microsoft.Xna.Framework;
using ValueType = VilliInput.Enums.ValueType;

namespace VilliInput
{

    public struct Value
    {

        public float? FloatValue { get; }

        public int? IntValue { get; }

        public uint? UIntValue { get; }

        public double? DoubleValue { get; }

        public Point? PointValue { get; }

        public Vector2? Vector2Value { get; }

        public ValueType Type { get; }

        public Value(ValueType type, uint? valueUInt = null, int? valueInt = null, Point? valuePoint = null, Vector2? valueVector2 = null, float? valueFloat = null, double? valueDouble = null)
        {
            UIntValue = valueUInt;
            IntValue = valueInt;
            FloatValue = valueFloat;
            PointValue = valuePoint;
            Vector2Value = valueVector2;
            DoubleValue = valueDouble;

            Type = type;
        }

        public void Validate()
        {
            switch (Type)
            {
                case ValueType.None:
                    throw new Exception("ValueType.None is not a valid type!");
                case ValueType.Double:
                    if (DoubleValue == null)
                    {
                        throw new Exception("DoubleValue has an invalid value for a Value of type ValueType.Double!");
                    }

                    break;
                case ValueType.Vector2:
                    if (Vector2Value == null)
                    {
                        throw new Exception("Vector2Value has an invalid value for a Value of type ValueType.Vector2!");
                    }

                    break;
                case ValueType.Float:
                    if (FloatValue == null)
                    {
                        throw new Exception("FloatValue has an invalid value for a Value of type ValueType.Float!");
                    }

                    break;
                case ValueType.Point:
                    if (PointValue == null)
                    {
                        throw new Exception("PointValue has an invalid value for a Value of type ValueType.Point!");
                    }

                    break;
                case ValueType.UInt:
                    if (UIntValue == null)
                    {
                        throw new Exception("UIntValue has an invalid value for a Value of type ValueType.UInt!");
                    }

                    break;
                case ValueType.Int:
                    if (IntValue == null)
                    {
                        throw new Exception("IntValue has an invalid value for a Value of type ValueType.Int!");
                    }

                    break;
            }
        }

    }

}
