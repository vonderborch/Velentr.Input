using System;
using VilliInput.MouseInput;

namespace VilliInput.Conditions.Internal
{

    public abstract class MouseSensorCondition : InputCondition
    {
        public MouseSensor Sensor { get; private set; }

        protected MouseSensorCondition(MouseSensor sensor, bool windowMustBeActive = true) : base(InputSource.Mouse, windowMustBeActive)
        {
            Sensor = sensor;
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeSensor(Sensor);
        }
    }
}
