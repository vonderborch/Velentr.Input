using System.Collections.Generic;
using XnaGestureType = Microsoft.Xna.Framework.Input.Touch.GestureType;

namespace Velentr.Input.Touch
{

    public static class TouchGestureMapping
    {

        public static Dictionary<GestureType, XnaGestureType> InternalToXna = new Dictionary<GestureType, XnaGestureType>
        {
            {GestureType.None, XnaGestureType.None},
            {GestureType.Tap, XnaGestureType.Tap},
            {GestureType.DragComplete, XnaGestureType.DragComplete},
            {GestureType.Flick, XnaGestureType.Flick},
            {GestureType.FreeDrag, XnaGestureType.FreeDrag},
            {GestureType.Hold, XnaGestureType.Hold},
            {GestureType.HorizontalDrag, XnaGestureType.HorizontalDrag},
            {GestureType.Pinch, XnaGestureType.Pinch},
            {GestureType.PinchComplete, XnaGestureType.PinchComplete},
            {GestureType.DoubleTap, XnaGestureType.DoubleTap},
            {GestureType.VerticalDrag, XnaGestureType.VerticalDrag}
        };

        public static Dictionary<XnaGestureType, GestureType> XnaToInternal = new Dictionary<XnaGestureType, GestureType>
        {
            {XnaGestureType.None, GestureType.None},
            {XnaGestureType.Tap, GestureType.Tap},
            {XnaGestureType.DragComplete, GestureType.DragComplete},
            {XnaGestureType.Flick, GestureType.Flick},
            {XnaGestureType.FreeDrag, GestureType.FreeDrag},
            {XnaGestureType.Hold, GestureType.Hold},
            {XnaGestureType.HorizontalDrag, GestureType.HorizontalDrag},
            {XnaGestureType.Pinch, GestureType.Pinch},
            {XnaGestureType.PinchComplete, GestureType.PinchComplete},
            {XnaGestureType.DoubleTap, GestureType.DoubleTap},
            {XnaGestureType.VerticalDrag, GestureType.VerticalDrag}
        };

    }

}
