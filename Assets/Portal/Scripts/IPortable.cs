using UnityEngine;

public interface IPortable
{
    public enum PortingState
    {
        Started,
        InProgress,
        EndingPositive,
        EndingNegative,
        Ended
    }

    public PortingState CurrentPortingState { get; set; }
    public bool IsClone { get; set; }
    public Transform PortingPortal {get; set; }
    public PortingMovement PortingMovement { get; }

    public void Declonify(GameObject oldGameObject);
}
