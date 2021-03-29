using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;

namespace VilliInput.Touch
{
    public struct Gesture
    {
        public Gesture(int id, Vector2 position, Vector2 position2, Vector2 delta, Vector2 delta2, GestureType gestureType, TimeSpan timeStamp)
        {
            Id = id;
            Position = position;
            Position2 = position2;
            Delta = delta;
            Delta2 = delta2;
            GestureType = gestureType;
            TimeStamp = timeStamp;
        }

        public int Id { get; private set; }

        public Vector2 Position { get; private set; }

        public Vector2 Position2 { get; private set; }

        public Vector2 Delta { get; private set; }

        public Vector2 Delta2 { get; private set; }

        public GestureType GestureType { get; private set; }

        public TimeSpan TimeStamp { get; private set; }
    }
}
