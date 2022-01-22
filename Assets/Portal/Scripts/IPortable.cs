using UnityEngine;

/// <summary>
/// This interface is used to determine if an object can use portals and to update its porting state.
/// </summary>
public interface IPortable
{
    /// <summary>
    /// The different states an object can be in during teleporting
    /// </summary>
    public enum PortingState
    {
        Started,
        InProgress,
        Porting,
        Ending,
        NoPorting
    }

    /// <summary>
    /// the current PortingState
    /// </summary>
    public PortingState CurrentPortingState { get; set; }
    /// <summary>
    /// the portal from which teleporting starts
    /// </summary>
    public Transform PortingPortal {get; set; }
    /// <summary>
    /// reference to the PortingMovementPUN
    /// </summary>
    public PortingMovementPUN PortingMvmnt { get; }
}
