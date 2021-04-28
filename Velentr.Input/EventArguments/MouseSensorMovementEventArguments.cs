using Microsoft.Xna.Framework;
using Velentr.Input.Mouse;

namespace Velentr.Input.EventArguments
{

    public class MouseSensorMovementEventArguments : ConditionEventArguments
    {

        public MouseSensor Sensor { get; internal set; }

        public Rectangle? Boundaries { get; internal set; }

        public bool UseRelativeCoordinates { get; internal set; }

        public Point MouseCoordinates { get; internal set; }

        public Point RelativeMouseCoordinates { get; internal set; }

        public Value CurrentValue { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (MouseSensorMovementEventArguments) MemberwiseClone();
        }

    }

}
