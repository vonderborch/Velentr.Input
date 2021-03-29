using System;
using System.Collections.Generic;
using Velentr.Input.EventArguments;

namespace Velentr.Input
{

    public class InputConditionEvent
    {

        internal List<EventHandler<ConditionEventArguments>> Delegates = new List<EventHandler<ConditionEventArguments>>();

        internal event EventHandler<ConditionEventArguments> InternalEvent;

        public event EventHandler<ConditionEventArguments> Event
        {
            add
            {
                InternalEvent += value;
                Delegates.Add(value);
            }

            remove
            {
                InternalEvent -= value;
                Delegates.Remove(value);
            }
        }

        public void Clear()
        {
            var list = Delegates;
            for (var i = 0; i < list.Count; i++)
            {
                InternalEvent -= list[i];
            }

            Delegates.Clear();
        }

        public void TriggerEvent(object sender, ConditionEventArguments e)
        {
            InternalEvent?.Invoke(sender, e);
        }

        public static InputConditionEvent operator +(InputConditionEvent left, EventHandler<ConditionEventArguments> right)
        {
            left.InternalEvent += right;
            left.Delegates.Add(right);

            return left;
        }

        public static InputConditionEvent operator -(InputConditionEvent left, EventHandler<ConditionEventArguments> right)
        {
            left.InternalEvent -= right;
            left.Delegates.Remove(right);

            return left;
        }

    }

}
