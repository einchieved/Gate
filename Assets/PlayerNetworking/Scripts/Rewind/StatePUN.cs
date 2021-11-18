using UnityEngine;


public class StatePUN
{
    public StatePUN(Vector3 position, Vector3 velocity, Quaternion rotation)
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

    public bool Equals(StatePUN other)
    {
        return Position.x.Equals(other.Position.x) &&  Position.y.Equals(other.Position.y) && Position.z.Equals(other.Position.z)  
               && Velocity.x.Equals(other.Velocity.x) && Velocity.y.Equals(other.Velocity.y) && Velocity.z.Equals(other.Velocity.z)
               && Rotation.x.Equals(other.Rotation.x) && Rotation.y.Equals(other.Rotation.y) && Rotation.z.Equals(other.Rotation.z);
    }
}
