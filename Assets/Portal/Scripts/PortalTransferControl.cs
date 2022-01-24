using UnityEngine;
using static IPortable;

/// <summary>
/// Tells the object entering to switch place with its clone (if it meets the requirements)
/// </summary>
public class PortalTransferControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IPortable portable = other.GetComponent<IPortable>();
        if (portable != null && CurrentStateOK(portable))
        {
            portable.CurrentPortingState = PortingState.Porting;
        }
    }

    private bool CurrentStateOK(IPortable testSubject)
    {
        return testSubject.CurrentPortingState == PortingState.Started ||
            testSubject.CurrentPortingState == PortingState.InProgress;
    }
}
