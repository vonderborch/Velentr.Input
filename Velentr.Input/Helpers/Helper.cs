using System;
using Microsoft.Xna.Framework;

namespace Velentr.Input.Helpers
{

    public static class Helper
    {

        public static bool CoordinateInRectangle(Point point, Rectangle rectangle)
        {
            return rectangle.X <= point.X && point.X <= rectangle.Width && rectangle.Y <= point.Y && point.Y <= rectangle.Height;
        }

        public static uint ElapsedMilliSeconds(TimeSpan startTime, GameTime endTime)
        {
            return Convert.ToUInt32(Math.Abs((endTime.TotalGameTime - startTime).TotalMilliseconds));
        }

        public static Point ScalePointToChild(Point coordinates, Rectangle parentArea, Rectangle childArea)
        {
            var scale = new Vector2(parentArea.Width / (float) childArea.Width, parentArea.Height / (float) childArea.Height);
            var scaledOrigin = new Vector2(childArea.X * scale.X, childArea.Y * scale.Y);

#if MONOGAME
            return (coordinates.ToVector2() * scale - scaledOrigin).ToPoint();
#else
            return new Point((int)(coordinates.X * scale.X - scaledOrigin.X), (int)(coordinates.Y * scale.Y - scaledOrigin.Y));
#endif
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
