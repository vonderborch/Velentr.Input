using System;
using System.Collections.Generic;
using VilliInput.EventArguments;

namespace VilliInput
{

    public class VilliEvent
    {

        internal List<EventHandler<VilliEventArguments>> Delegates = new List<EventHandler<VilliEventArguments>>();

        internal event EventHandler<VilliEventArguments> InternalEvent;

        public event EventHandler<VilliEventArguments> Event
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

        public void TriggerEvent(object sender, VilliEventArguments e)
        {
            InternalEvent?.Invoke(sender, e);
        }

        public static VilliEvent operator +(VilliEvent left, EventHandler<VilliEventArguments> right)
        {
            left.InternalEvent += right;
            left.Delegates.Add(right);

            return left;
        }

        public static VilliEvent operator -(VilliEvent left, EventHandler<VilliEventArguments> right)
        {
            left.InternalEvent -= right;
            left.Delegates.Remove(right);

            return left;
        }

    }

}
