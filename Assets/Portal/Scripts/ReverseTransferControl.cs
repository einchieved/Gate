using UnityEngine;
using static IPortable;

/// <summary>
/// Tells the object entering to switch back its place with its clone (if it meets the requirements)
/// </summary>
public class ReverseTransferControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (portable != null && portable.CurrentPortingState == PortingState.InProgress)
        {
            portable.CurrentPortingState = PortingState.Porting;
        }
    }
}
