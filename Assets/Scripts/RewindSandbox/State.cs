using UnityEngine;

namespace DefaultNamespace
{
    public class State
    {
        public State(Vector3 position, Vector3 velocity, Quaternion rotation)
        {
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
        }

        public Vector3 Position { get; }
        public Vector3 Velocity { get; }
        public Quaternion Rotation { get; }


        public override string ToString()
        {
            return "position: " + Position + " | velocity: " + Velocity + " | rotation: " + Rotation;
        }
    }
}