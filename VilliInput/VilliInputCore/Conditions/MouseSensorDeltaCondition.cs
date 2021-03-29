﻿using System;
using Microsoft.Xna.Framework;
using VilliInput.Conditions.Internal;
using VilliInput.Enums;
using VilliInput.EventArguments;
using VilliInput.Helpers;
using VilliInput.Mouse;
using ValueType = VilliInput.Enums.ValueType;

namespace VilliInput.Conditions
{

    public class MouseSensorDeltaCondition : LogicCondition
    {

        private readonly Rectangle? _parentBoundaries;

        public MouseSensorDeltaCondition(MouseSensor sensor, ValueLogic logicValue, Rectangle? boundaries = null, bool useRelativeCoordinates = false, Rectangle? parentBoundaries = null, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForConditionMet = 0) : base(InputSource.Mouse, logicValue, windowMustBeActive, consumable, allowedIfConsumed, milliSecondsForConditionMet)
        {
            Sensor = sensor;

            Boundaries = boundaries;
            UseRelativeCoordinates = useRelativeCoordinates;
            _parentBoundaries = parentBoundaries;

            logicValue.Value.Validate();
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    if (logicValue.Value.Type != ValueType.Int)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.HorizontalScrollWheel, you must use a ValueType.Int!");
                    }

                    break;
                case MouseSensor.Pointer:
                    if (logicValue.Value.Type != ValueType.Point)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.Pointer, you must use a ValueType.Point!");
                    }

                    break;
                case MouseSensor.ScrollWheels:
                    if (logicValue.Value.Type != ValueType.Point)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.ScrollWheels, you must use a ValueType.Point!");
                    }

                    break;
                case MouseSensor.VerticalScrollWheel:
                    if (logicValue.Value.Type != ValueType.Int)
                    {
                        throw new Exception("logicValue contains an invalid type for MouseSensor.VerticalScrollWheel, you must use a ValueType.Int!");
                    }

                    break;
            }
        }

        public MouseSensor Sensor { get; }

        public Rectangle? Boundaries { get; }

        public bool UseRelativeCoordinates { get; }

        public Rectangle? ParentBoundaries => _parentBoundaries ?? Villi.Window.ClientBounds;

        protected override Value InternalGetValue()
        {
            switch (Sensor)
            {
                case MouseSensor.HorizontalScrollWheel:
                    return new Value(ValueType.Int, valueInt: MouseService.HorizontalScrollDelta);
                case MouseSensor.Pointer:
                    return new Value(ValueType.Int, valuePoint: MouseService.CursorPositionDelta);
                case MouseSensor.ScrollWheels:
                    return new Value(ValueType.Int, valuePoint: MouseService.ScrollDelta);
                case MouseSensor.VerticalScrollWheel:
                    return new Value(ValueType.Int, valueInt: MouseService.VerticalScrollDelta);
            }

            return new Value(ValueType.None);
        }

        public override void Consume()
        {
            Villi.System.Mouse.ConsumeSensor(Sensor);
        }

        public override bool IsConsumed()
        {
            return Villi.System.Mouse.IsSensorConsumed(Sensor);
        }

        public override VilliEventArguments GetArguments()
        {
            return new MouseSensorMovementEventArguments
            {
                Boundaries = Boundaries,
                Sensor = Sensor,
                Condition = this,
                InputSource = InputSource,
                MouseCoordinates = MouseService.CurrentCursorPosition,
                RelativeMouseCoordinates = Helper.ScalePointToChild(MouseService.CurrentCursorPosition, ParentBoundaries ?? Villi.Window.ClientBounds, Boundaries ?? Villi.Window.ClientBounds),
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                UseRelativeCoordinates = UseRelativeCoordinates,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime),
                WindowMustBeActive = WindowMustBeActive,
                CurrentValue = InternalGetValue()
            };
        }

        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (!WindowMustBeActive || Villi.IsWindowActive && MouseService.IsMouseInWindow)
                   && (allowedIfConsumed || IsConsumed())
                   && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Villi.CurrentTime) >= milliSecondsForConditionMet)
                   && (Boundaries == null || MouseService.CursorInBounds((Rectangle) Boundaries, UseRelativeCoordinates, ParentBoundaries));
        }

    }

}
