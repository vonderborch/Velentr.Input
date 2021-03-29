using System;
using Microsoft.Xna.Framework;

namespace Velentr.Input.Touch
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

        public int Id { get; }

        public Vector2 Position { get; }

        public Vector2 Position2 { get; }

        public Vector2 Delta { get; }

        public Vector2 Delta2 { get; }

        public GestureType GestureType { get; }

        public TimeSpan TimeStamp { get; }

    }

}
