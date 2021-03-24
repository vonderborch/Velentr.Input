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
    }
}
