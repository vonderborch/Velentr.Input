using System;
using System.Collections.Generic;
using VilliInput.OLD.EventArguments;

namespace VilliInput.OLD
{
    public class VilliEvent
    {
        List<EventHandler<VilliEventArguments>> delegates = new List<EventHandler<VilliEventArguments>>();

        private event EventHandler<VilliEventArguments> internalEvent;

        public event EventHandler<VilliEventArguments> Event
        {
            add
            {
                internalEvent += value;
                delegates.Add(value);
            }

            remove
            {
                internalEvent -= value;
                delegates.Remove(value);
            }
        }

        public void Clear()
        {
            System.Collections.IList list = delegates;
            for (int i = 0; i < list.Count; i++)
            {
                EventHandler<VilliEventArguments> eh = (EventHandler<VilliEventArguments>)list[i];
                internalEvent -= eh;
            }
            delegates.Clear();
        }

        public void TriggerEvent(object sender, VilliEventArguments e)
        {
            internalEvent?.Invoke(sender, e);
        }

        public static VilliEvent operator +(VilliEvent left, EventHandler<VilliEventArguments> right)
        {
            left.internalEvent += right;
            left.delegates.Add(right);

            return left;
        }

        public static VilliEvent operator -(VilliEvent left, EventHandler<VilliEventArguments> right)
        {
            left.internalEvent -= right;
            left.delegates.Remove(right);

            return left;
        }
    }
}
