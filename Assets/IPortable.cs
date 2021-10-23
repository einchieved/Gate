using UnityEngine;

interface IPortable
{
    public bool IsPorting { get; set; }
    public Vector3 PortingDirection { get; set; }
    public Vector3 PortingFromDirection { get; set; }
}
