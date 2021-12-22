using UnityEngine;

public interface IPortable
{
    public enum PortingState
    {
        Started,
        InProgress,
        Porting,
        Ending,
        NoPorting
    }

    public PortingState CurrentPortingState { get; set; }
    public Transform PortingPortal {get; set; }
    public PortingMovement PortingMvmnt { get; }
}
