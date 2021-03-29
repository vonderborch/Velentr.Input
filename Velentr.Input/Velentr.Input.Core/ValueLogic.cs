using Microsoft.Xna.Framework;
using Velentr.Input.Enums;
using Velentr.Input.Helpers;

namespace Velentr.Input
{

    public struct ValueLogic
    {

        public Value Value { get; }

        public Comparison Comparator { get; }

        public ValueLogic(Value valueToCompare, Comparison comparator)
        {
            Value = valueToCompare;
            Comparator = comparator;
        }

        public bool Compare(Value value)
        {
            try
            {
                switch (Value.Type)
                {
                    case ValueType.Double:
                        return CompareHelper((double) Value.DoubleValue, (double) value.DoubleValue, Comparator);
                    case ValueType.Float:
                        return CompareHelper((float) Value.FloatValue, (float) value.FloatValue, Comparator);
                    case ValueType.Int:
                        return CompareHelper((int) Value.IntValue, (int) value.IntValue, Comparator);
                    case ValueType.Point:
                        return CompareHelper((Point) Value.PointValue, (Point) value.PointValue, Comparator);
                    case ValueType.UInt:
                        return CompareHelper((uint) Value.UIntValue, (uint) value.UIntValue, Comparator);
                    case ValueType.Vector2:
                        return CompareHelper((Vector2) Value.Vector2Value, (Vector2) value.Vector2Value, Comparator);
                }
            }
            catch { }

            return false;
        }

        private bool CompareHelper(int value, int valueToCompare, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThanX:
                case Comparison.GreaterThanY:
                case Comparison.GreaterThan:
                    return value > valueToCompare;
                case Comparison.GreaterThanOrEqualX:
                case Comparison.GreaterThanOrEqualY:
                case Comparison.GreaterThenOrEqual:
                    return value >= valueToCompare;
                case Comparison.Equal:
                    return value == valueToCompare;
                case Comparison.NotEquals:
                    return value != valueToCompare;
                case Comparison.LessThanX:
                case Comparison.LessThanY:
                case Comparison.LessThan:
                    return value < valueToCompare;
                case Comparison.LessThanOrEqualX:
                case Comparison.LessThanOrEqualY:
                case Comparison.LessThanOrEqual:
                    return value <= valueToCompare;
            }

            return false;
        }

        private bool CompareHelper(uint value, uint valueToCompare, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThanX:
                case Comparison.GreaterThanY:
                case Comparison.GreaterThan:
                    return value > valueToCompare;
                case Comparison.GreaterThanOrEqualX:
                case Comparison.GreaterThanOrEqualY:
                case Comparison.GreaterThenOrEqual:
                    return value >= valueToCompare;
                case Comparison.Equal:
                    return value == valueToCompare;
                case Comparison.NotEquals:
                    return value != valueToCompare;
                case Comparison.LessThanX:
                case Comparison.LessThanY:
                case Comparison.LessThan:
                    return value < valueToCompare;
                case Comparison.LessThanOrEqualX:
                case Comparison.LessThanOrEqualY:
                case Comparison.LessThanOrEqual:
                    return value <= valueToCompare;
            }

            return false;
        }

        private bool CompareHelper(float value, float valueToCompare, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThanX:
                case Comparison.GreaterThanY:
                case Comparison.GreaterThan:
                    return value > valueToCompare;
                case Comparison.GreaterThanOrEqualX:
                case Comparison.GreaterThanOrEqualY:
                case Comparison.GreaterThenOrEqual:
                    return value >= valueToCompare;
                case Comparison.Equal:
                    return Helper.ApproximatelyEqual(value, valueToCompare, VelentrInput.System.Settings.MaxFloatDifference);
                case Comparison.NotEquals:
                    return !Helper.ApproximatelyEqual(value, valueToCompare, VelentrInput.System.Settings.MaxFloatDifference);
                case Comparison.LessThanX:
                case Comparison.LessThanY:
                case Comparison.LessThan:
                    return value < valueToCompare;
                case Comparison.LessThanOrEqualX:
                case Comparison.LessThanOrEqualY:
                case Comparison.LessThanOrEqual:
                    return value <= valueToCompare;
            }

            return false;
        }

        private bool CompareHelper(double value, double valueToCompare, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThanX:
                case Comparison.GreaterThanY:
                case Comparison.GreaterThan:
                    return value > valueToCompare;
                case Comparison.GreaterThanOrEqualX:
                case Comparison.GreaterThanOrEqualY:
                case Comparison.GreaterThenOrEqual:
                    return value >= valueToCompare;
                case Comparison.Equal:
                    return Helper.ApproximatelyEqual(value, valueToCompare, VelentrInput.System.Settings.MaxDoubleDifference);
                case Comparison.NotEquals:
                    return !Helper.ApproximatelyEqual(value, valueToCompare, VelentrInput.System.Settings.MaxDoubleDifference);
                case Comparison.LessThanX:
                case Comparison.LessThanY:
                case Comparison.LessThan:
                    return value < valueToCompare;
                case Comparison.LessThanOrEqualX:
                case Comparison.LessThanOrEqualY:
                case Comparison.LessThanOrEqual:
                    return value <= valueToCompare;
            }

            return false;
        }

        private bool CompareHelper(Point value, Point valueToCompare, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThanX:
                    return value.X > valueToCompare.X;
                case Comparison.GreaterThanY:
                    return value.Y > valueToCompare.Y;
                case Comparison.GreaterThan:
                    return value.X > valueToCompare.X || value.Y > valueToCompare.Y;
                case Comparison.GreaterThanOrEqualX:
                    return value.X >= valueToCompare.X;
                case Comparison.GreaterThanOrEqualY:
                    return value.Y >= valueToCompare.Y;
                case Comparison.GreaterThenOrEqual:
                    return value.X >= valueToCompare.X || value.Y >= valueToCompare.Y;
                case Comparison.Equal:
                    return value == valueToCompare;
                case Comparison.NotEquals:
                    return value != valueToCompare;
                case Comparison.LessThanX:
                    return value.X < valueToCompare.X;
                case Comparison.LessThanY:
                    return value.Y < valueToCompare.Y;
                case Comparison.LessThan:
                    return value.X < valueToCompare.X || value.Y < valueToCompare.Y;
                case Comparison.LessThanOrEqualX:
                    return value.X <= valueToCompare.X;
                case Comparison.LessThanOrEqualY:
                    return value.Y <= valueToCompare.Y;
                case Comparison.LessThanOrEqual:
                    return value.X <= valueToCompare.X || value.Y <= valueToCompare.Y;
            }

            return false;
        }

        private bool CompareHelper(Vector2 value, Vector2 valueToCompare, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThanX:
                    return value.X > valueToCompare.X;
                case Comparison.GreaterThanY:
                    return value.Y > valueToCompare.Y;
                case Comparison.GreaterThan:
                    return value.X > valueToCompare.X || value.Y > valueToCompare.Y;
                case Comparison.GreaterThanOrEqualX:
                    return value.X >= valueToCompare.X;
                case Comparison.GreaterThanOrEqualY:
                    return value.Y >= valueToCompare.Y;
                case Comparison.GreaterThenOrEqual:
                    return value.X >= valueToCompare.X || value.Y >= valueToCompare.Y;
                case Comparison.Equal:
                    return Helper.ApproximatelyEqual(value, valueToCompare, VelentrInput.System.Settings.MaxFloatDifference);
                case Comparison.NotEquals:
                    return !Helper.ApproximatelyEqual(value, valueToCompare, VelentrInput.System.Settings.MaxFloatDifference);
                case Comparison.LessThanX:
                    return value.X < valueToCompare.X;
                case Comparison.LessThanY:
                    return value.Y < valueToCompare.Y;
                case Comparison.LessThan:
                    return value.X < valueToCompare.X || value.Y < valueToCompare.Y;
                case Comparison.LessThanOrEqualX:
                    return value.X <= valueToCompare.X;
                case Comparison.LessThanOrEqualY:
                    return value.Y <= valueToCompare.Y;
                case Comparison.LessThanOrEqual:
                    return value.X <= valueToCompare.X || value.Y <= valueToCompare.Y;
            }

            return false;
        }

    }

}
