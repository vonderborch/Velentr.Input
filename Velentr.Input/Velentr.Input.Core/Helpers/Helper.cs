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

        public static uint ElapsedSeconds(GameTime startTime, GameTime endTime)
        {
            return Convert.ToUInt32((endTime.TotalGameTime - startTime.TotalGameTime).TotalSeconds);
        }

        public static uint ElapsedMilliSeconds(GameTime startTime, GameTime endTime)
        {
            if (startTime == null && endTime == null)
            {
                throw new ArgumentException("At least one time must not be null!");
            }
            if (startTime == null)
            {
                return Convert.ToUInt32(endTime.TotalGameTime.TotalMilliseconds);
            }
            if (endTime == null)
            {
                return Convert.ToUInt32(startTime.TotalGameTime.TotalMilliseconds);
            }

            var start = startTime;
            var end = endTime;
            if (start.TotalGameTime > end.TotalGameTime)
            {
                start = endTime;
                end = startTime;
            }

            return Convert.ToUInt32((end.TotalGameTime - start.TotalGameTime).TotalMilliseconds);
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
