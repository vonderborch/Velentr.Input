using System;
using Microsoft.Xna.Framework;

namespace VilliInput.Helpers
{
    public static class Helper
    {

        public static bool CoordinateInRectangle(Point point, Rectangle rectangle)
        {
            return rectangle.X <= point.X && point.X <= rectangle.Width && rectangle.Y <= point.Y && point.Y <= rectangle.Height;
        }

        public static uint ElapsedSeconds(GameTime startTime, GameTime endTime)
        {
            return Convert.ToUInt32((endTime.TotalGameTime - startTime.TotalGameTime).TotalSeconds);
        }

        public static uint ElapsedMilliSeconds(GameTime startTime, GameTime endTime)
        {
            return Convert.ToUInt32((endTime.TotalGameTime - startTime.TotalGameTime).TotalMilliseconds);
        }

        public static Point ScalePointToChild(Point coordinates, Rectangle parentArea, Rectangle childArea)
        {
            var scale = new Vector2(parentArea.Width / (float)childArea.Width, parentArea.Height / (float)childArea.Height);
            var scaledOrigin = new Vector2(childArea.X * scale.X, childArea.Y * scale.Y);

            return (coordinates.ToVector2() * scale - scaledOrigin).ToPoint();
        }

        public static bool ApproximatelyEqual(float value1, float value2, float maxDifference)
        {
            return Math.Abs(value1 - value2) <= maxDifference;
        }

        public static bool ApproximatelyEqual(Vector2 value1, Vector2 value2, float maxDifference)
        {
            return Math.Abs(Vector2.Distance(value1, value2)) <= maxDifference;
        }

        public static bool ApproximatelyEqual(double value1, double value2, double maxDifference)
        {
            return Math.Abs(value1 - value2) <= maxDifference;
        }
    }
}
