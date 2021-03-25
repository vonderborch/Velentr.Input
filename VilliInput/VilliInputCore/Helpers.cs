using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace VilliInput
{
    public static class Helpers
    {

        public static bool CoordinateInRectangle(Point point, Rectangle rectangle)
        {
            return rectangle.X <= point.X && point.X <= rectangle.Width && rectangle.Y <= point.Y && point.Y <= rectangle.Height;
        }

        public static uint ElapsedSeconds(GameTime startTime, GameTime endTime)
        {
            return Convert.ToUInt32((endTime.TotalGameTime - startTime.TotalGameTime).TotalSeconds);
        }

        public static Point ScalePointToChild(Point coordinates, Rectangle parentArea, Rectangle childArea)
        {
            var scale = new Vector2(parentArea.Width / (float) childArea.Width, parentArea.Height / (float) childArea.Height);
            var scaledOrigin = new Vector2(childArea.X * scale.X, childArea.Y * scale.Y);

            return (coordinates.ToVector2() * scale - scaledOrigin).ToPoint();
        }

        public static bool ValidValueComparator(InputValue value, InputValueLogic comparator)
        {
            try
            {
                if (value.ValueUInt != null)
                {
                    return ValidValueComparatorHelper((uint) value.ValueUInt, (uint) comparator.ConditionalValue.ValueUInt, comparator.Comparator);
                }

                if (value.ValueInt != null)
                {
                    return ValidValueComparatorHelper((int)value.ValueInt, (int)comparator.ConditionalValue.ValueInt, comparator.Comparator);
                }

                if (value.ValuePoint != null)
                {
                    return ValidValueComparatorHelper((Point) value.ValuePoint, (Point) comparator.ConditionalValue.ValuePoint, comparator.Comparator);
                }

                if (value.ValueVector2 != null)
                {
                    return ValidValueComparatorHelper((Vector2)value.ValueVector2, (Vector2)comparator.ConditionalValue.ValueVector2, comparator.Comparator);
                }

                if (value.ValueVector2 != null)
                {
                    return ValidValueComparatorHelper((Vector2)value.ValueVector2, (Vector2)comparator.ConditionalValue.ValueVector2, comparator.Comparator);
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        private static bool ValidValueComparatorHelper(uint value, uint comparedValue, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThan:
                    return value > comparedValue;
                case Comparison.GreaterThenOrEqual:
                    return value >= comparedValue;
                case Comparison.Equal:
                    return value == comparedValue;
                case Comparison.NotEquals:
                    return value != comparedValue;
                case Comparison.LessThan:
                    return value < comparedValue;
                case Comparison.LessThanOrEqual:
                    return value <= comparedValue;
            }

            return false;
        }

        private static bool ValidValueComparatorHelper(int value, int comparedValue, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThan:
                    return value > comparedValue;
                case Comparison.GreaterThenOrEqual:
                    return value >= comparedValue;
                case Comparison.Equal:
                    return value == comparedValue;
                case Comparison.NotEquals:
                    return value != comparedValue;
                case Comparison.LessThan:
                    return value < comparedValue;
                case Comparison.LessThanOrEqual:
                    return value <= comparedValue;
            }

            return false;
        }

        private static bool ValidValueComparatorHelper(Point value, Point comparedValue, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThan:
                    return value.X > comparedValue.X || value.Y > comparedValue.Y;
                case Comparison.GreaterThenOrEqual:
                    return value.X >= comparedValue.X || value.Y >= comparedValue.Y;
                case Comparison.Equal:
                    return value == comparedValue;
                case Comparison.NotEquals:
                    return value != comparedValue;
                case Comparison.LessThan:
                    return value.X < comparedValue.X || value.Y < comparedValue.Y;
                case Comparison.LessThanOrEqual:
                    return value.X <= comparedValue.X || value.Y <= comparedValue.Y;
            }

            return false;
        }

        private static bool ValidValueComparatorHelper(Vector2 value, Vector2 comparedValue, Comparison comparator)
        {
            switch (comparator)
            {
                case Comparison.GreaterThan:
                    return value.X > comparedValue.X || value.Y > comparedValue.Y;
                case Comparison.GreaterThenOrEqual:
                    return value.X >= comparedValue.X || value.Y >= comparedValue.Y;
                case Comparison.Equal:
                    return value == comparedValue;
                case Comparison.NotEquals:
                    return value != comparedValue;
                case Comparison.LessThan:
                    return value.X < comparedValue.X || value.Y < comparedValue.Y;
                case Comparison.LessThanOrEqual:
                    return value.X <= comparedValue.X || value.Y <= comparedValue.Y;
            }

            return false;
        }
    }
}
